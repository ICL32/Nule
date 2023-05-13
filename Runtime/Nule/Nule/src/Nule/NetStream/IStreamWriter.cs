using System;
using System.Net.Sockets;
using Nule.Packet;

namespace Nule.NetStream
{
    //Handles writing to a stream optimized
    public interface IStreamWriter<T> where T : IPacket
    {
        public void WriteToStream(NetworkStream destination);

        public T DeserializeToPacket(Span<byte> data);
    }
}