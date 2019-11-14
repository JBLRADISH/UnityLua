using AOT;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class ILInjector
{
    private const string AssemblyPath = "./Library/ScriptAssemblies/Assembly-CSharp.dll";

    [MenuItem("Tools/ILInjector")]
    public static void DoInjector()
    {
        ReaderParameters readerParameters = new ReaderParameters { ReadSymbols = true };
        AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(AssemblyPath, readerParameters);
        if (assemblyDefinition == null)
        {
            Debug.LogError(string.Format("Inject Failed: {0}", AssemblyPath));
            return;
        }
        List<MethodDefinition> methodDefinitions = new List<MethodDefinition>();
        foreach (TypeDefinition typeDefinition in assemblyDefinition.MainModule.Types)
        {
            foreach (MethodDefinition methodDefinition in typeDefinition.Methods)
            {
                if (ContainsHotFixAttribute(methodDefinition))
                {
                    methodDefinitions.Add(methodDefinition);
                }
            }
        }
        TypeDefinition delegateHelper = DelegateHelperGenerator(assemblyDefinition.MainModule, methodDefinitions);
        foreach (MethodDefinition methodDefinition in methodDefinitions)
        {
            FieldDefinition hotfix = new FieldDefinition(methodDefinition.Name + "HotFix", FieldAttributes.Public | FieldAttributes.Static, delegateHelper);
            methodDefinition.DeclaringType.Fields.Add(hotfix);
            HotFix_IL(methodDefinition, delegateHelper);
        }

        TypeDefinition luaCallback = assemblyDefinition.MainModule.Types.First(c => c.FullName == typeof(LuaCallback).FullName);
        List<byte> blob = new List<byte> { 1, 0, 24 };
        blob.AddRange(Encoding.Default.GetBytes("LuaCallback+LuaCFunction").ToList());
        blob.AddRange(new List<byte> { 0, 0 });
        foreach (MethodDefinition methodDefinition in methodDefinitions)
        {
            MethodDefinition method = new MethodDefinition("set_" + methodDefinition.Name + "HotFix", MethodAttributes.Public | MethodAttributes.Static | MethodAttributes.HideBySig, assemblyDefinition.MainModule.ImportReference(typeof(int)));
            method.Parameters.Add(new ParameterDefinition(assemblyDefinition.MainModule.ImportReference(typeof(IntPtr))));
            method.CustomAttributes.Add(new CustomAttribute(assemblyDefinition.MainModule.ImportReference(typeof(MonoPInvokeCallbackAttribute).GetConstructor(new Type[] { typeof(Type) })), blob.ToArray()));
            luaCallback.Methods.Add(method);
            LuaCallback_IL(method, methodDefinition, delegateHelper);
        }

        Register_IL(luaCallback, methodDefinitions);

        assemblyDefinition.Write(AssemblyPath, new WriterParameters { WriteSymbols = true });
    }

    private static void Register_IL(TypeDefinition luaCallback, List<MethodDefinition> hotfixs)
    {
        MethodDefinition register = luaCallback.Methods.First(method => method.Name == "Register");
        ILProcessor ilProcessor = register.Body.GetILProcessor();
        Instruction first = ilProcessor.Body.Instructions.First();
        Dictionary<TypeDefinition, List<MethodDefinition>> res = GetHotFixTypeDefinitions(hotfixs);
        foreach (var item in res)
        {
            ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldtoken, item.Key));
            ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Call, luaCallback.Module.ImportReference(typeof(Type).GetMethod("GetTypeFromHandle", new[] { typeof(RuntimeTypeHandle) }))));
            ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldnull));
            ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Call, luaCallback.Methods.First(method => method.Name == "BeginClass")));
            foreach (var hotfix in item.Value)
            {
                ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldstr, hotfix.Name + "HotFix"));
                ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldnull));
                ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldnull));
                ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldftn, luaCallback.Methods.First(method => method.Name == "set_" + hotfix.Name + "HotFix")));
                ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Newobj, luaCallback.Module.ImportReference(typeof(LuaCallback.LuaCFunction).GetConstructor(new Type[] { typeof(object), typeof(IntPtr) }))));
                ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Call, luaCallback.Methods.First(method => method.Name == "RegisterVar")));
            }
            ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Call, luaCallback.Methods.First(method => method.Name == "EndClass")));
        }
    }

    private static Dictionary<TypeDefinition, List<MethodDefinition>> GetHotFixTypeDefinitions(List<MethodDefinition> hotfixs)
    {
        Dictionary<TypeDefinition, List<MethodDefinition>> res = new Dictionary<TypeDefinition, List<MethodDefinition>>();
        for (int i = 0; i < hotfixs.Count; i++)
        {
            if (!res.ContainsKey(hotfixs[i].DeclaringType))
            {
                res[hotfixs[i].DeclaringType] = new List<MethodDefinition>();
            }
            res[hotfixs[i].DeclaringType].Add(hotfixs[i]);
        }
        return res;
    }

    private static void LuaCallback_IL(MethodDefinition luaCallback, MethodDefinition hotfix, TypeDefinition delegateHelper)
    {
        luaCallback.Body.InitLocals = true;
        ILProcessor ilProcessor = luaCallback.Body.GetILProcessor();
        Instruction ldc_i4_0 = ilProcessor.Create(OpCodes.Ldc_I4_0);
        Instruction ldloc_1 = ilProcessor.Create(OpCodes.Ldloc_1);
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Nop));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_0));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldc_I4_M1));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, luaCallback.Module.ImportReference(typeof(LuaAPI).GetMethod("IsLuaFunction", new[] { typeof(IntPtr), typeof(int) }))));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Brfalse, ldc_i4_0));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Nop));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_0));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, luaCallback.Module.ImportReference(typeof(LuaAPI).GetMethod("ToLuaFunction", new[] { typeof(IntPtr) }))));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Stloc_0));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldloc_0));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Newobj, delegateHelper.Methods.First(method => method.Name == ".ctor")));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Stsfld, hotfix.DeclaringType.Fields.First(field => field.Name == hotfix.Name + "HotFix")));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Nop));
        luaCallback.Body.Instructions.Add(ldc_i4_0);
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Stloc_1));
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Br, ldloc_1));
        luaCallback.Body.Instructions.Add(ldloc_1);
        luaCallback.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ret));
        CalcOffset(luaCallback);
    }

    private static void HotFix_IL(MethodDefinition hotfix, TypeDefinition delegateHelper)
    {
        ILProcessor ilProcessor = hotfix.Body.GetILProcessor();
        Instruction first = ilProcessor.Body.Instructions.First();
        Instruction ldloc_0 = ilProcessor.Body.Instructions.Last(instruction => instruction.OpCode == OpCodes.Ldloc_0);
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Nop));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldsfld, hotfix.DeclaringType.Fields.First(field => field.Name == hotfix.Name + "HotFix")));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Brfalse, first));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Nop));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldsfld, hotfix.DeclaringType.Fields.First(field => field.Name == hotfix.Name + "HotFix")));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldarg_1));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldarg_2));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Callvirt, delegateHelper.Methods.First(method => method.Name == GetInvokeMethodName(hotfix))));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Stloc_0));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Br, ldloc_0));
        CalcOffset(hotfix);
    }

    private static bool ContainsHotFixAttribute(MethodDefinition methodDefinition)
    {
        string fullName = typeof(HotFixAttribute).FullName;
        for (int i = 0; i < methodDefinition.CustomAttributes.Count; i++)
        {
            if (methodDefinition.CustomAttributes[i].AttributeType.FullName == fullName)
            {
                return true;
            }
        }
        return false;
    }

    private static TypeDefinition DelegateHelperGenerator(ModuleDefinition mainModule, List<MethodDefinition> methodDefinitions)
    {
        List<MethodDefinition> res = new List<MethodDefinition>();
        AddMethodDefinition(res, methodDefinitions);
        TypeDefinition delegateHelper = new TypeDefinition(null, "DelegateHelper", TypeAttributes.Public | TypeAttributes.BeforeFieldInit, mainModule.ImportReference(typeof(object)));
        mainModule.Types.Add(delegateHelper);
        FieldDefinition reference = new FieldDefinition("reference", FieldAttributes.Private, mainModule.ImportReference(typeof(int)));
        delegateHelper.Fields.Add(reference);
        MethodDefinition ctor = new MethodDefinition(".ctor", MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, mainModule.ImportReference(typeof(void)));
        ctor.Parameters.Add(new ParameterDefinition(mainModule.ImportReference(typeof(int))));
        delegateHelper.Methods.Add(ctor);
        DelegateHelper_Ctor_IL(ctor);
        for (int i = 0; i < res.Count; i++)
        {
            MethodDefinition invoke = new MethodDefinition(GetInvokeMethodName(res[i]), MethodAttributes.Public | MethodAttributes.HideBySig, res[i].ReturnType);
            for (int j = 0; j < res[i].Parameters.Count; j++)
            {
                invoke.Parameters.Add(res[i].Parameters[j]);
            }
            delegateHelper.Methods.Add(invoke);
            DelegateHelper_Invoke_IL(invoke);
        }
        return delegateHelper;
    }

    private static void DelegateHelper_Ctor_IL(MethodDefinition ctor)
    {
        ctor.Body.InitLocals = true;
        ILProcessor ilProcessor = ctor.Body.GetILProcessor();
        ctor.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_0));
        ctor.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, ctor.Module.ImportReference(typeof(object).GetConstructor(new Type[] { }))));
        ctor.Body.Instructions.Add(ilProcessor.Create(OpCodes.Nop));
        ctor.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_0));
        ctor.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_1));
        ctor.Body.Instructions.Add(ilProcessor.Create(OpCodes.Stfld, ctor.DeclaringType.Fields.First(field => field.Name == "reference")));
        ctor.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ret));
        CalcOffset(ctor);
    }

    private static void DelegateHelper_Invoke_IL(MethodDefinition invoke)
    {
        invoke.Body.InitLocals = true;
        ILProcessor ilProcessor = invoke.Body.GetILProcessor();
        Instruction getL = ilProcessor.Create(OpCodes.Call, invoke.Module.ImportReference(typeof(LuaEnv).GetMethod("get_L")));
        Instruction ldloc_0 = ilProcessor.Create(OpCodes.Ldloc_0);
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Nop));
        invoke.Body.Instructions.Add(getL);
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_0));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldfld, invoke.DeclaringType.Fields.First(field => field.Name == "reference")));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, invoke.Module.ImportReference(typeof(LuaAPI).GetMethod("PushLuaFunction", new[] { typeof(IntPtr), typeof(int) }))));
        invoke.Body.Instructions.Add(getL);
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_1));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Conv_R8));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, invoke.Module.ImportReference(typeof(LuaCallback).GetMethod("PushNumber", new[] { typeof(IntPtr), typeof(double) }))));
        invoke.Body.Instructions.Add(getL);
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_2));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Conv_R8));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, invoke.Module.ImportReference(typeof(LuaCallback).GetMethod("PushNumber", new[] { typeof(IntPtr), typeof(double) }))));
        invoke.Body.Instructions.Add(getL);
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldc_I4_2));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldc_I4_1));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, invoke.Module.ImportReference(typeof(LuaAPI).GetMethod("CallLuaFunction", new[] { typeof(IntPtr), typeof(int), typeof(int) }))));
        invoke.Body.Instructions.Add(getL);
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldc_I4_M1));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, invoke.Module.ImportReference(typeof(LuaAPI).GetMethod("ToNumber", new[] { typeof(IntPtr), typeof(int) }))));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Conv_I4));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Stloc_0));
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Br, ldloc_0));
        invoke.Body.Instructions.Add(ldloc_0);
        invoke.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ret));
        CalcOffset(invoke);
    }

    private static void CalcOffset(MethodDefinition methodDefinition)
    {
        int offset = 0;
        for (int i = 0; i < methodDefinition.Body.Instructions.Count; i++)
        {
            methodDefinition.Body.Instructions[i].Offset = offset;
            offset += methodDefinition.Body.Instructions[i].GetSize();
        }
    }

    private static string GetInvokeMethodName(MethodDefinition methodDefinition)
    {
        string res = "Invoke";
        for (int i = 0; i < methodDefinition.Parameters.Count; i++)
        {
            res += "_" + methodDefinition.Parameters[i].ParameterType.Name;
        }
        res += "_" + methodDefinition.ReturnType.Name;
        return res;
    }

    private static void AddMethodDefinition(List<MethodDefinition> res, List<MethodDefinition> methodDefinitions)
    {
        for (int i = 0; i < methodDefinitions.Count; i++)
        {
            AddMethodDefinition(res, methodDefinitions[i]);
        }
    }

    private static void AddMethodDefinition(List<MethodDefinition> res, MethodDefinition methodDefinition)
    {
        for (int i = 0; i < res.Count; i++)
        {
            if (CheckMethodDefinitionEqual(res[i], methodDefinition))
            {
                return;
            }
        }
        res.Add(methodDefinition);
    }

    private static bool CheckMethodDefinitionEqual(MethodDefinition methodDefinition1, MethodDefinition methodDefinition2)
    {
        if (methodDefinition1.ReturnType != methodDefinition2.ReturnType)
        {
            return false;
        }
        if (methodDefinition1.Parameters.Count != methodDefinition2.Parameters.Count)
        {
            return false;
        }
        for (int i = 0; i < methodDefinition1.Parameters.Count; i++)
        {
            if (methodDefinition1.Parameters[i].ParameterType != methodDefinition2.Parameters[i].ParameterType)
            {
                return false;
            }
        }
        return true;
    }
}