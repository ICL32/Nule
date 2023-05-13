using System.Net.Sockets;

namespace Nule.NuleClient
{
    public class NuleClient
    {
        public TcpClient ClientTCP { get; private set; }
        public NetworkStream ClientStream { get; private set; }

        public NuleClient(TcpClient clientTCP)
        {
            ClientTCP = clientTCP;
            ClientStream = clientTCP.GetStream();
        }
    }
}