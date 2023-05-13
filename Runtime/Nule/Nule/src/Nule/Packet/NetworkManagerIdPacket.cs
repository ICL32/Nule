namespace Nule.Packet
{
    public struct NetworkManagerIdPacket
    {
        public PacketTypes packetType { get; set; }
        public uint IdAssign { get; set; }

        public NetworkManagerIdPacket(uint id, PacketTypes type = PacketTypes.NetworkManagerId)
        {
            packetType = type;
            IdAssign = id;
        }
    }
}