using System;
using System.Collections.Generic;
using System.Reflection;
using Mono.Cecil;
using Nule.Packet;

namespace Nule.Weaver
{
    public static class Definitions
    {
        public static Assembly CsAssembly => Assembly.GetExecutingAssembly();
        public static Type RpcAttribute => typeof(RpcAttribute);
        
        public static Dictionary<string, MethodDefinition> RpcMethodsDefinitions { get; internal set; } = new();
        public static Dictionary<int, NetworkBehaviour> NetworkObjectInstances { get; internal set; } = new();
    }
}


