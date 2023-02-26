using System.Collections.Generic;
using Mono.Cecil;

namespace Nule.Weaver
{
    public static class Definitions
    {
        public static string CsAssemblyName => "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null";

        public static Dictionary<string, MethodDefinition> RpcMethods { get; internal set; } = new();
        public static Dictionary<int, NetworkBehaviour> NetworkObjectInstances { get; internal set; } = new();
    }
}


