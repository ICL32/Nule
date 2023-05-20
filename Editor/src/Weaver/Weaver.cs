/*
using System.IO;
using System.Reflection;
using Mono.Cecil;
using Mono.Collections.Generic;
using UnityEngine;

namespace Nule.Weaver
{
   internal static  class Weaver
   {

      //Method will be called when weaving has begun
      internal static void EntryPoint(ModuleDefinition module)
      {
         WeaveDebugger.Log($"WeavingILPostProcessor");

         if (module != null)
         {
            Collection<TypeDefinition> types = module.Types;
            TypeFiltering(types);  
         }
      }

      internal static void TypeFiltering(Collection<TypeDefinition> types)
      {
         WeaveDebugger.Log("Type Filtering");
         if (types == null)
         {
            WeaveDebugger.Log("Type Collection is null");
            return;
         }
         if (types.Count == 0)
         {
            WeaveDebugger.Log("Type Collection is empty");
            return;
         }

         WeaveDebugger.Log($"Type Collection has {types.Count} types");

         for (int i = 0; i < types.Count; i++)
         {
            var currentType = types[i];
            string baseTypeName;

            if (currentType.BaseType != null)
            {
               baseTypeName = types[i].BaseType.Name;
               WeaveDebugger.Log($"Base type names: {baseTypeName}"); 
               
               if (baseTypeName == nameof(NetworkBehaviour))
               {
                  MethodFiltering(currentType.Methods);
               }
               
            }
            
         }
      }

      internal static void MethodFiltering(Collection<MethodDefinition> methods)
      {
         
      }
      
   }
}
*/
