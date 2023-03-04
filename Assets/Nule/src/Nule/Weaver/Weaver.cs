using Mono.Cecil;
using UnityEditor;
using UnityEngine;

namespace Nule.Weaver
{
   internal static partial class Weaver
   {
      private static TypeCache.TypeCollection _networkBehaviourTypes = UnityEditor.TypeCache.GetTypesDerivedFrom<NetworkBehaviour>();
      private static AssemblyDefinition _userCode = AssemblyDefinition.ReadAssembly(Definitions.CsAssembly.Location);

      //WeaveLoop handles going through types
      [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
      private static void MainLoop()
      {
         foreach (TypeDefinition type in _userCode.MainModule.Types)
         {
            Debug.Log(type.Name);
         }

      }

   }
}
