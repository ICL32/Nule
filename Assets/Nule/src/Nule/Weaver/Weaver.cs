using System.IO;
using System.Reflection;
using Mono.Cecil;
using Mono.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nule.Weaver
{
   [InitializeOnLoad]
   internal static partial class Weaver
   {
      private static AssemblyDefinition _userCode;
      private static DefaultAssemblyResolver _assemblyResolver = new DefaultAssemblyResolver();
      
      
      private static void EntryPoint()
      {
         PopulateSearchDictionaries();
         _userCode = AssemblyDefinition.ReadAssembly(
            Definitions.CsAssembly.Location,
            new ReaderParameters
            {
               AssemblyResolver = _assemblyResolver
            });
         Collection<TypeDefinition> userTypes = _userCode.MainModule.Types;
         foreach (TypeDefinition type in userTypes)
         {
            if (type.BaseType != null && type.BaseType.FullName == Definitions.NetworkBehaviour.FullName)
            {
               TypeWeaver(type.Methods);
            }
         }
      }

      private static void TypeWeaver(in Collection<MethodDefinition> methods)
      {
         Collection<TypeDefinition> userTypes = _userCode.MainModule.Types;
         foreach (TypeDefinition type in userTypes)
         {
            if (type.BaseType != null && type.BaseType.FullName == Definitions.NetworkBehaviour.FullName)
            {
               foreach (MethodDefinition method in methods)
               {
                  if (method.HasCustomAttributes)
                  {
                     MethodWeaver(method);
                  }
               }
            }
         }
      }

      private static void MethodWeaver(MethodDefinition method)
      {
         Debug.Log($"Checking method {method.Name} for RpcAttribute");
         foreach (CustomAttribute attribute in method.CustomAttributes)
         {
            if (attribute.AttributeType.FullName == Definitions.RpcAttribute.FullName)
            {
               method.RpcWeave();
            }
         }
      }

      private static void PopulateSearchDictionaries()
      {
         string[] assemblies = Definitions.UnityAssemblies;
         string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
         _assemblyResolver.AddSearchDirectory(executingDir);
         for (int i = 0; i < assemblies.Length; i++)
         {
            string assemblyPath = assemblies[i];
            string assemblyFullPath = Path.Combine(executingDir, assemblyPath);
            _assemblyResolver.AddSearchDirectory(Path.GetDirectoryName(assemblyFullPath));
            Debug.Log(assemblyFullPath);
         }
      }
      
   }
}
