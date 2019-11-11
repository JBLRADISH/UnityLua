using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Callbacks;
using UnityEngine;

public static class ILInjector
{
    private const string AssemblyPath = "./Library/ScriptAssemblies/Assembly-CSharp.dll";

    [PostProcessScene]
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
        DelegateHelperGenerator(assemblyDefinition.MainModule, methodDefinitions);
        assemblyDefinition.Write(AssemblyPath, new WriterParameters { WriteSymbols = true });
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

    private static void DelegateHelperGenerator(ModuleDefinition mainModule, List<MethodDefinition> methodDefinitions)
    {
        List<MethodDefinition> res = new List<MethodDefinition>();
        AddMethodDefinition(res, methodDefinitions);
        TypeDefinition delegateHelper = new TypeDefinition(null, "DelegateHelperTest", TypeAttributes.Public, mainModule.ImportReference(typeof(object)));
        mainModule.Types.Add(delegateHelper);
        FieldDefinition reference = new FieldDefinition("reference", FieldAttributes.Private, mainModule.ImportReference(typeof(int)));
        delegateHelper.Fields.Add(reference);
        for (int i = 0; i < res.Count; i++)
        {
            MethodDefinition invoke = new MethodDefinition(GetInvokeMethodName(res[i]), MethodAttributes.Public, res[i].ReturnType);
            for (int j = 0; j < res[i].Parameters.Count; j++)
            {
                invoke.Parameters.Add(res[i].Parameters[j]);
            }
            delegateHelper.Methods.Add(invoke);
            Invoke_IL(invoke);
        }
    }

    private static void Invoke_IL(MethodDefinition invoke)
    {
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