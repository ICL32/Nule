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
        public NuleTransport(int port)
        {
            ServerPort = port;
        }

        public override async Task<bool> TryConnectAsync(IPAddress address)
        {
            if (State != NetworkStates.Offline)
            {
                return false;
            }

            try
            {
                Client = new TcpClient();
                await Client.ConnectAsync(address, ServerPort);
                State = NetworkStates.Connected;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public override async Task<bool> TryClientSend(byte[] data)
        {
            //Network is not connected
            if (State != NetworkStates.Connected)
            {
                return false;
            }

            NetworkStream stream = Client.GetStream();
            if (!stream.CanWrite)
            {
                return false;
            }

            await stream.WriteAsync(data, 0, data.Length);
            return true;
        }

        public override async Task<bool> TryServerSend(List<TcpClient> activeClients, byte[] buffer)
        {
            if (State != NetworkStates.Hosting || activeClients.Count == 0)
            {
                return false;
            }

            bool allSentSuccessfully = true;

                foreach (TcpClient client in activeClients)
                {
                    if (client != null && client.Connected)
                    {
                        NetworkStream stream = client.GetStream();
                        //Stream couldn't be written to
                        if (stream.CanWrite)
                        {
                            await stream.WriteAsync(buffer, 0, buffer.Length);
                        }
                        else
                        {
                            allSentSuccessfully = false;
                        }
                    }
                }

                return allSentSuccessfully;
        }

        public override bool TryStartHosting()
        {
            if (State != NetworkStates.Offline)
            {
                return false;
            }

            try
            {
                Server = new TcpListener(IPAddress.Parse("127.0.0.1"), ServerPort);
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

        public override async Task<bool> TryReceiveAsync(NetworkStream stream, Memory<byte> buffer)
        {
            if (Client is not { Connected: true })
            {
                return false;
            }

            try
            {
                return true; 
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
        }


        public override async Task<TcpClient> ListenForConnectionsAsync()
        {
            if (State != NetworkStates.Hosting)
            {
                throw new InvalidOperationException("Server is not hosting.");
            }

            TcpClient newClient = null;
            try
            {
                newClient = await Server.AcceptTcpClientAsync();
                return newClient;

            }
            catch (Exception ex)
            {
                Debug.LogError($"Error accepting new client: {ex.Message}");
                newClient?.Dispose();
            }

            return null;
        }
    }

}
