using System;
using Nule.NuleClient;

namespace Nule.Transport
{
    public abstract class Transport
    {
        private Client _client;
        public NetworkStates State { get; set; } = NetworkStates.Offline;
        public event EventHandler<ClientIDEventArgs> ClientConnected;
        public event EventHandler<ClientIDEventArgs> ClientDisconnected;

        public abstract bool TryConnect();
        public abstract bool TrySend();
        public abstract bool TryReceiveRpc();
        public abstract void Disconnect();
    }
}