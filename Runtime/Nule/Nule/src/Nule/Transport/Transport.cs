using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Nule.NuleClient;
using UnityEngine;

namespace Nule.Transport
{
    public abstract class Transport
    {
        protected byte[] Buffer { get; } = new byte[1024];
        
        protected int ServerPort { get;  set; }

        protected internal TcpClient Client { get; set; }

        protected TcpListener Server { get; set; }
        
        public NetworkStates State { get; protected set; } = NetworkStates.Offline;
        
        
        public abstract bool TryStartHosting();
        public abstract bool TryStopHosting();
        public abstract Task<bool> TryConnectAsync(IPAddress address);
        public abstract Task<bool> TryServerSend(List<TcpClient> activeClients, byte[] buffer);
        public abstract Task<bool> TryClientSend(byte[] buffer);
        public abstract Task<bool> TryReceiveAsync(NetworkStream stream, Memory<byte> buffer);
        public abstract Task<TcpClient> ListenForConnectionsAsync();
        
    }
}