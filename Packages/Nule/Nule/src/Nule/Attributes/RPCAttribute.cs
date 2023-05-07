using System;

namespace Nule.Weaver.Attributes
{
    public class RpcAttribute : Attribute
    {
        public uint MethodID { get; internal set; }
    }
}