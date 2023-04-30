using System.Net;
using Nule.Transport;

namespace Nule.NuleTransport
{
    public class NuleTransport : Transport.Transport
    {
        public override bool TryConnectAsync()
        {
            if (State != NetworkStates.Offline)
            {
                return false;
            }
            ServerIp = IPAddress.Parse(IpAddress);
            EndPoint = new IPEndPoint(ServerIp, ServerPort);

            return false;
        }

        public override bool TrySend()
        {
            throw new System.NotImplementedException();
        }

        public override void StartHosting()
        {
            throw new System.NotImplementedException();
        }

        public override void StopHosting()
        {
            throw new System.NotImplementedException();
        }

        public override void RecieveAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}