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
        protected IPAddress ServerAddress { get;}
        
        protected int ServerPort { get;}

        protected TcpClient Client { get; set; }

        protected TcpListener Server { get; set; }
        
        public bool KeepListening { get; set; } = true;
        

        public NetworkStates State { get; set; } = NetworkStates.Offline;
        

        public Transport(IPAddress address, int port)
        {
            ServerAddress = address;
            ServerPort = port;
        }

        public abstract bool StartHosting();
        public abstract bool StopHosting();
        public abstract Task<bool> TryConnectAsync();
        public abstract Task<bool> TrySend(byte[] data);
        public abstract Task<byte[]> RecieveAsync();
        public abstract Task ListenForConnectionsAsync();
        
    }
}