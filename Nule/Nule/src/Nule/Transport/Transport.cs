using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Nule.NuleClient;
using UnityEngine;

namespace Nule.Transport
{
    public abstract class Transport
    {
        protected IPAddress ServerAddress { get; set; }
        
        protected int ServerPort { get;  set; }

        protected TcpClient Client { get; set; }

        protected TcpListener Server { get; set; }
        
        public bool KeepListening { get; set; } = true;
        

        public NetworkStates State { get; set; } = NetworkStates.Offline;
        
        
        

        public abstract bool TryStartHosting();
        public abstract bool TryStopHosting();
        public abstract Task<bool> TryConnectAsync();
        public abstract Task<bool> TrySend(byte[] data);
        public abstract Task<byte[]> ReceiveAsync();
        public abstract Task ListenForConnectionsAsync();
        
    }
}