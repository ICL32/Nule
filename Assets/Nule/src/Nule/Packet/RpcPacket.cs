﻿using Nule.NuleClient;

namespace Nule.Packet
{
    public struct RpcPacket
    {
        //public PacketTypes Type { get; } = PacketTypes.Rpc;
        //Net ID helps us get the monobehavior that method was called in. Then from there we can navigate to find the method called
        public NetId Id { get; }
        
        
    }
}