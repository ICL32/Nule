using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Nule.Transport;
using UnityEngine;

namespace Nule.NuleTransport
{
    public class NuleTransport : Transport.Transport
    {
        private List<TcpClient> ActiveClients = new List<TcpClient>(100);
        
        public NuleTransport(IPAddress address, int port)
        {
            ServerAddress = address;
            ServerPort = port;
        }

        public override async Task<bool> TryConnectAsync()
        {
            if (State != NetworkStates.Offline)
            {
                return false;
            }

            try
            {
                Client = new TcpClient();
                await Client.ConnectAsync(ServerAddress, ServerPort);
                State = NetworkStates.Connected;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override async Task<bool> TrySend(byte[] data, int length)
        {
            //Network is not connected
            if (State == NetworkStates.Offline)
            {
                return false;
            }
            //Check if Network State is a Client
            if (State == NetworkStates.Connected)
            {
                if (Client is not { Connected: true })
                {
                    return false;
                }

                NetworkStream stream = Client.GetStream();
                if (!stream.CanWrite)
                {
                    return false;
                }

                await stream.WriteAsync(data, 0, length);
                return true;
            }
            
            //Check if Network State is a Server and if there are Players to send to
            if (State == NetworkStates.Hosting && ActiveClients.Count != 0)
            {
                bool allSentSuccessfully = true;

                foreach (TcpClient client in ActiveClients)
                {
                    if (client != null && client.Connected)
                    {
                        NetworkStream stream = client.GetStream();
                        //Stream couldn't be written to
                        if (stream.CanWrite)
                        {
                            await stream.WriteAsync(data, 0, data.Length);
                        }
                        else
                        {
                            allSentSuccessfully = false;
                        }
                    }
                }

                return allSentSuccessfully;
            }

            return false;
        }

        public override bool TryStartHosting()
        {
            if (State != NetworkStates.Offline)
            {
                return false;
            }

            try
            {
                Server = new TcpListener(ServerAddress, ServerPort);
                Server.Start();
                State = NetworkStates.Hosting;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool TryStopHosting()
        {
            if (State != NetworkStates.Hosting)
            {
                return false;
            }

            Server.Stop();
            State = NetworkStates.Offline;
            Server = null;
            return true;
        }

        public override async Task<byte[]> ReceiveAsync()
        {
            if (Client is not { Connected: true })
            {
                return null;
            }

            try
            {
                NetworkStream stream = Client.GetStream();
                int bytesRead = await stream.ReadAsync(Buffer, 0, Buffer.Length);
                byte[] result = new byte[bytesRead];
                Array.Copy(Buffer, 0, result, 0, bytesRead);
                return result;
            }
            catch
            {
                // You might want to log exceptions here to know what's happening
                return null;
            }
        }

        
        public override async Task ListenForConnectionsAsync()
        {
            if (State != NetworkStates.Hosting)
            {
                throw new InvalidOperationException("Server is not hosting.");
            }
            
            TcpClient newClient = null;
            
            while (KeepListening)
            {
                try
                {
                    newClient = await Server.AcceptTcpClientAsync();
                    ClientsList.Add(newClient);

                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error accepting new client: {ex.Message}");
                    newClient?.Dispose();
                }
            }
        }
        
    }
}