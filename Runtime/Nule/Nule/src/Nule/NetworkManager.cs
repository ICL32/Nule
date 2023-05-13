using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
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
        [SerializeField] private SceneAsset _scene;
        [SerializeField] private GameObject _playerObject;

        private Transport.Transport _transport;
        private static NetworkManager _instance;

        //This defines what ID
        //ID 0 is for hosts
        private uint _clientId;
        //This is an incremental counter to assign IDs, starts from 1 because 0 is reserved for hosts
        private uint _idCounter = 1;
        
        private List<NetworkBehaviour> _playerObjects;
        private Dictionary<uint, TcpClient> _clientsList;

        private void Awake()
        {
            //If Singleton failed to initalize then don't execute any logic
            if (!TrySingletonInitialize())
            {
                return;
            }

            _transport = new NuleTransport.NuleTransport(_serverPort);
            Debug.Log($"Creating TCP Client on port: :{_serverPort}");
            DontDestroyOnLoad(gameObject);
            Application.runInBackground = _runInBackground;
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

        public bool TryStartHosting()
        {
            if (_transport.TryStartHosting())
            {
                _clientId = 0;
                SceneManager.LoadScene(_scene.name);
                return true;
            }

            return false;
        }

        public async Task<bool> TryConnectToServer(string ipAddress)
        {
            return await _transport.TryConnectAsync(IPAddress.Parse(ipAddress));
        }

        public async Task TryStartListening()
        {
            TcpClient client = await _transport.ListenForConnectionsAsync();

            if (client == null)
            {
                return;
            }
            
            while (_clientsList.ContainsKey(_idCounter))
            {
                _idCounter++;
            }
            
            _clientsList.Add(_idCounter, client);
            var clientStream = client.GetStream();
            
        }

    }
}
