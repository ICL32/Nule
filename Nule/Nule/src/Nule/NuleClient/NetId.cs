using System;
using UnityEngine;

namespace Nule.NuleClient
{
    public struct NetId : IEquatable<NetId>
    {
        //Network ID is the Scene Object ID assigned by the host
        public uint NetworkId {get;}
        //Object ID is the GUID in the Scene
        public uint ObjectId {get;}

        public NetId(uint networkId, uint objectId)
        {
            NetworkId = networkId;
            ObjectId = objectId;
        }

        public bool Equals(NetId other)
        {
            return NetworkId == other.NetworkId && ObjectId == other.ObjectId;
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(NetworkId, ObjectId);
        }
    }
}