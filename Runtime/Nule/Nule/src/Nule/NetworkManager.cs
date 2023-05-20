using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Nule.NetStream;
using Nule.Packet;
using Nule.Transport;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nule
{
    //Handles Game Logic with the transport
    public class NetworkManager : MonoBehaviour
    {
        public static event Action<uint> OnConnected;
        public static event Action<uint> OnDisconnected;
        
        [SerializeField] private bool _runInBackground = true;
        [SerializeField] private int _serverPort;
        [SerializeField] private GameObject _playerObject;
        [SerializeField] private Vector3 _spawnPosition;
 
        private Transport.Transport _transport;
        private static NetworkManager _instance;

        //This defines what ID
        //ID 0 is for hosts
        private uint _clientId;
        //This is an incremental counter to assign IDs, starts from 1 because 0 is reserved for hosts
        private uint _idCounter = 1;
        
        private List<NetworkBehaviour> _playerObjects;
        private Dictionary<uint, NuleClient.NuleClient> _clientsList;
        

        private void Awake()
        {
            //If Singleton failed to initalize then don't execute any logic
            if (!TrySingletonInitialize())
            {
                return;
            }

            _transport = new NuleTransport.NuleTransport(_serverPort);
            Debug.Log($"Creating TCP Client on port: {_serverPort}");
            DontDestroyOnLoad(gameObject);
            Application.runInBackground = _runInBackground;
        }

        private void Update()
        {
        }

        /// <summary>This method is responsible for checking there is only one instance of NetworkManager at a time</summary>
        private bool TrySingletonInitialize()
        {
            if (_instance != null)
            {
                if (_instance == this)
                {
                    return true;
                }

                if (_instance != this)
                {
                    Debug.LogWarning("Duplicate Network Manager component found and will be destroyed", gameObject);
                    Destroy(gameObject);
                    return false;
                }
            }

            //If instance is null then assign this instance
            _instance = this;
            return true;
        }
        IEnumerator LoadSceneAndInstantiate()
        {
            // Start loading the scene
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");

            // Wait until the scene has finished loading
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            // Instantiate the object in the loaded scene
            Instantiate(_playerObject, _spawnPosition, Quaternion.identity);
        }

        public bool TryStartHosting()
        {
            if (_transport.TryStartHosting())
            {
                _clientId = 0;
                StartCoroutine(LoadSceneAndInstantiate());
                return true;
            }

            return false;
        }

        public async Task<bool> TryConnectToServer(string ipAddress)
        {
            bool connected = await _transport.TryConnectAsync(IPAddress.Parse(ipAddress));
            
            if (connected)
            {
                StartCoroutine(LoadSceneAndInstantiate());
            }

            return true;
        }

        public async Task StartListening()
        {

            while (true) // Continue accepting new clients indefinitely
            {
                TcpClient client = await _transport.ListenForConnectionsAsync(); // Assuming this method waits for a new client to connect

                if (client != null)
                {
                    while (_clientsList.ContainsKey(_idCounter))
                    {
                        _idCounter++;
                    }

                    var nuleClient = new NuleClient.NuleClient(client);
                    _clientsList.Add(_idCounter, nuleClient);
                    StartCoroutine(LoadSceneAndInstantiate());

                    NetworkManagerIdPacket packet = new NetworkManagerIdPacket(_idCounter);
                    Memory<byte> packetBuffer = new byte[Marshal.SizeOf<NetworkManagerIdPacket>()];
                    if (!Serializer.TrySerialize(ref packet, packetBuffer.Span))
                    {
                        throw new Exception("Failed to serialize packet.");
                    }

                    await nuleClient.ClientStream.WriteAsync(packetBuffer);
                    
                    
                    // Start a new task to handle this client's communication
                    HandleClient(nuleClient);
                }
            }
        }
        private async void HandleClient(NuleClient.NuleClient client)
        {
            Memory<byte> buffer = new byte[1024]; 
            bool receivedData = await _transport.TryReceiveAsync(client.ClientStream, buffer);

            if (!receivedData)
            {
                return;
            }

            Memory<byte> packetTypeBytes = buffer.Slice(0, 2);
            
            PacketTypes packetType = (PacketTypes)BitConverter.ToUInt16(packetTypeBytes.Span);
            //TODO: Handle different packet types
            
            HandleClient(client);
        }

    }
}
