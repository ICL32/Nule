using System;
using Nule.NuleClient;

namespace Nule.Transport
{
    internal abstract class Transport
    {
        private Client _client;
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;
        public event EventHandler<ClientConnectedEventArgs> ClientDisconnected;
    }
}