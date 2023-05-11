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
        protected IPAddress ServerAddress { get; set; }
        
        protected int ServerPort { get;  set; }

        protected TcpClient Client { get; set; }

        protected TcpListener Server { get; set; }
        protected List<TcpClient> ClientsList { get; set; }
        
        public bool KeepListening { get; set; } = true;
        

        public NetworkStates State { get; set; } = NetworkStates.Offline;
        
        
        

        public abstract bool TryStartHosting();
        public abstract bool TryStopHosting();
        public abstract Task<bool> TryConnectAsync();
        public abstract Task<bool> TrySend(byte[] data, int length);
        public abstract Task<byte[]> ReceiveAsync();
        public abstract Task ListenForConnectionsAsync();
        
    }
}