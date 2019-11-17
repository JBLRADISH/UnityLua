using AOT;
using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
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

        TypeDefinition delegateHelper = assemblyDefinition.MainModule.Types.First(type => type.FullName == typeof(DelegateHelper).FullName);
        foreach (MethodDefinition methodDefinition in methodDefinitions)
        {
            FieldDefinition hotfix = new FieldDefinition(methodDefinition.Name + "HotFix", FieldAttributes.Public | FieldAttributes.Static, delegateHelper);
            methodDefinition.DeclaringType.Fields.Add(hotfix);
            HotFix_IL(methodDefinition, delegateHelper);
        }

        foreach (MethodDefinition methodDefinition in methodDefinitions)
        {
            TypeDefinition wrapClass = assemblyDefinition.MainModule.Types.First(type => type.FullName == methodDefinition.DeclaringType.FullName + "Wrap");
            MethodDefinition wrap = wrapClass.Methods.First(method => method.Name == "set_" + methodDefinition.Name + "HotFix");
            Wrap_IL(wrap, methodDefinition, delegateHelper);
        }

        assemblyDefinition.Write(AssemblyPath, new WriterParameters { WriteSymbols = true });

        Debug.Log("IL Injector Success!");
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

    private static void Wrap_IL(MethodDefinition wrap, MethodDefinition hotfix, TypeDefinition delegateHelper)
    {
        wrap.Body.InitLocals = true;
        wrap.Body.Variables.Clear();
        wrap.Body.Variables.Add(new VariableDefinition(wrap.Module.ImportReference(typeof(int))));
        wrap.Body.Variables.Add(new VariableDefinition(wrap.Module.ImportReference(typeof(int))));
        ILProcessor ilProcessor = wrap.Body.GetILProcessor();
        Instruction ldc_i4_0 = ilProcessor.Create(OpCodes.Ldc_I4_0);
        Instruction ldloc_1 = ilProcessor.Create(OpCodes.Ldloc_1);
        wrap.Body.Instructions.Clear();
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Nop));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_0));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldc_I4_M1));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, wrap.Module.ImportReference(typeof(LuaAPI).GetMethod("IsLuaFunction", new[] { typeof(IntPtr), typeof(int) }))));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Brfalse, ldc_i4_0));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Nop));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldarg_0));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Call, wrap.Module.ImportReference(typeof(LuaAPI).GetMethod("ToLuaFunction", new[] { typeof(IntPtr) }))));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Stloc_0));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ldloc_0));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Newobj, delegateHelper.Methods.First(method => method.Name == ".ctor")));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Stsfld, hotfix.DeclaringType.Fields.First(field => field.Name == hotfix.Name + "HotFix")));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Nop));
        wrap.Body.Instructions.Add(ldc_i4_0);
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Stloc_1));
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Br, ldloc_1));
        wrap.Body.Instructions.Add(ldloc_1);
        wrap.Body.Instructions.Add(ilProcessor.Create(OpCodes.Ret));
        CalcOffset(wrap);
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
        PushArg(hotfix, first);
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Callvirt, delegateHelper.Methods.First(method => method.Name == GetInvokeMethodName(hotfix))));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Stloc_0));
        ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Br, ldloc_0));
        CalcOffset(hotfix);
    }

    private static OpCode[] ldarg = new OpCode[] { OpCodes.Ldarg_0, OpCodes.Ldarg_1, OpCodes.Ldarg_2, OpCodes.Ldarg_3 };

    private static void PushArg(MethodDefinition hotfix, Instruction first)
    {
        ILProcessor ilProcessor = hotfix.Body.GetILProcessor();
        int count = hotfix.Parameters.Count;
        if (hotfix.IsStatic)
        {
            for (int i = 0; i < Mathf.Min(ldarg.Length, count); i++)
            {
                ilProcessor.InsertBefore(first, ilProcessor.Create(ldarg[i]));
            }
            for (int i = ldarg.Length; i < count; i++)
            {
                ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldarg_S, i));
            }
        }
        else
        {
            ilProcessor.InsertBefore(first, ilProcessor.Create(ldarg[0]));
            for (int i = 1; i < Mathf.Min(ldarg.Length, count + 1); i++)
            {
                ilProcessor.InsertBefore(first, ilProcessor.Create(ldarg[i]));
            }
            for (int i = ldarg.Length; i < count + 1; i++)
            {
                ilProcessor.InsertBefore(first, ilProcessor.Create(OpCodes.Ldarg_S, i));
            }
        }
    }

    private static string GetInvokeMethodName(MethodDefinition methodDefinition)
    {
        string res = "Invoke";
        if (!methodDefinition.IsStatic)
        {
            res += "_" + methodDefinition.DeclaringType.Name;
        }
        for (int i = 0; i < methodDefinition.Parameters.Count; i++)
        {
            res += "_" + methodDefinition.Parameters[i].ParameterType.Name;
        }
        res += "_" + methodDefinition.ReturnType.Name;
        return res;
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
}