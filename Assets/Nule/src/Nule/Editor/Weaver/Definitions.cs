using System;
using System.Collections.Generic;
using System.IO;
using Mono.Cecil;
using Nule.Packet;
using Nule.Weaver.Attributes;
using UnityEditor;
using UnityEditor.Compilation;
using Unity.CompilationPipeline.Common.ILPostProcessing;

namespace Nule.Weaver
{
    internal static class Definitions
    {
        public static Dictionary<string, MethodDefinition> _rpcMethodsDefinitions { get; internal set; }

        public static Dictionary<int, NetworkBehaviour> _networkObjectInstances { get; internal set; }
        //CsAssembly is the dll generated by Unity with all User's types.
        public static string[] UnityAssembliesFullPath => new[]
        {
            Path.GetFullPath(@"..\..\Editor\Data\Managed\UnityEngine.dll"),
            Path.GetFullPath(@"..\..\Editor\Data\Managed\UnityEngine\UnityEngine.CoreModule.dll")
            
        };
        
        public static Type RpcAttribute => typeof(RpcAttribute);
        public static Type NetworkBehaviour => typeof(NetworkBehaviour);

        internal static Dictionary<string, MethodDefinition> RpcMethodsDefinitions => _rpcMethodsDefinitions;
        internal static Dictionary<int, NetworkBehaviour> NetworkObjectInstances => _networkObjectInstances;
    }
}
