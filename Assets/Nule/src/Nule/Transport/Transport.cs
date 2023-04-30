using System;
using System.Net;
using System.Net.Sockets;
using Nule.NuleClient;
using UnityEngine;

namespace Nule.Transport
{
    public abstract class Transport
    {
        protected string IpAddress { get;  set;}
        protected int ServerPort { get;  set;}

        protected IPAddress ServerIp { get; set;}
        protected IPEndPoint EndPoint { get; set;}
        
        protected TcpClient TcpClient { get; set; }
        
        protected TcpListener TcpListener { get; set; }

        public NetworkStates State { get; set;} = NetworkStates.Offline;
        public event Action<int> ClientConnected;
        public event Action<int> ClientDisconnected;

        public abstract bool TryConnectAsync();
        public abstract bool TrySend();
        public abstract void StartHosting();
        public abstract void StopHosting();
        public abstract void RecieveAsync();
        public delegate void OnDisconnect();
        public delegate void OnConnected();
    }
}