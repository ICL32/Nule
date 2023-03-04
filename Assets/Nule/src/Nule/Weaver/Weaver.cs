using Mono.Cecil;
using Mono.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Nule.Weaver
{
   internal static partial class Weaver
   {
      private static TypeCache.TypeCollection _networkBehaviourTypes = UnityEditor.TypeCache.GetTypesDerivedFrom<NetworkBehaviour>();
      private static AssemblyDefinition _userCode = AssemblyDefinition.ReadAssembly(Definitions.CsAssembly.Location);

      
      
      
      //TODO: Create other methods that MainLoop calls to reduce logic done in one method.
      //WeaveLoop handles going through types
      [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
      internal static void MainLoop()
      {
         Collection<TypeDefinition> userTypes = _userCode.MainModule.Types;
         foreach (TypeDefinition type in _userCode.MainModule.Types)
         {
            
            if (type.BaseType != null && type.BaseType.FullName == Definitions.NetworkBehaviour.FullName)
            {
               foreach (MethodDefinition method in type.Methods)
               {
                  
                  //Check if they have attributes
                  if (method.HasCustomAttributes)
                  {
                     foreach (CustomAttribute attribute in method.CustomAttributes)
                     {
                        if (attribute.AttributeType.FullName == Definitions.RpcAttribute.FullName)
                        {
                           Definitions.RpcMethodsDefinitions.Add(method.Name, method);
                        }
                     }
                  }
               }
            }
         }

      }

   }
}
