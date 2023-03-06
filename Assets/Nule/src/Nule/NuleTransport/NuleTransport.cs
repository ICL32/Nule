using Nule.Transport;

namespace Nule.NuleTransport
{
    public class NuleTransport : Transport.Transport
    {
        public override bool TryConnect()
        {
            throw new System.NotImplementedException();
        }

        public override bool TrySend()
        {
            throw new System.NotImplementedException();
        }

        public override bool TryReceiveRpc()
        {
            throw new System.NotImplementedException();
        }

        public override void Disconnect()
        {
            throw new System.NotImplementedException();
        }
    }
}