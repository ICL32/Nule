using System;
using System.Buffers.Binary;
using System.Net.Sockets;
using Nule.NetStream;
using Unity.Collections.LowLevel.Unsafe;

namespace Nule.Packet
{
    public struct NetworkManagerIdPacket : IPacket, IStreamWriter<NetworkManagerIdPacket>
    {
        public PacketTypes packetType { get; set; }
        public uint IdAssign { get; set; }

        public NetworkManagerIdPacket(uint id, PacketTypes type = PacketTypes.NetworkManagerId)
        {
            packetType = type;
            IdAssign = id;
        }

        public void WriteToStream(NetworkStream destination)
        {
            if (!destination.CanWrite)
            {
                throw new Exception("Cannot write to the provided stream.");
            }

            Span<byte> packetBytes = new byte[UnsafeUtility.SizeOf<NetworkManagerIdPacket>()];

            // Use Serializer to write the whole struct to the byte array
            if (!Serializer.TrySerialize(ref this, packetBytes))
            {
                throw new Exception("Failed to serialize data.");
            }

            // Write the bytes to the stream
            destination.Write(packetBytes.ToArray(), 0, packetBytes.Length);
        }

        public NetworkManagerIdPacket DeserializeToPacket(Span<byte> data)
        {
            NetworkManagerIdPacket packet;
            Serializer.TryDeserialize(data, out packet);
            return packet;
        }
    }
}