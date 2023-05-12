using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

namespace Nule
{
    //Handles Game Logic with the transport
    public class NetworkManager : MonoBehaviour
    {
        public static event Action<int> OnConnected;
        public static event Action<int> OnDisconnected;
        
        [SerializeField] private bool _runInBackground = true;
        [SerializeField] private int _serverPort;

        private Transport.Transport _transport;
        private static NetworkManager _instance;
        
      

        private void Awake()
        {
            //If Singleton failed to initalize then don't execute any logic
            if (!TrySingletonInitialize())
            {
                return;
            }
            
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
            return _transport.TryStartHosting();
        }

        public async Task<bool> TryConnectToServer(string ipAddress)
        {
            return await _transport.TryConnectAsync(IPAddress.Parse(ipAddress));
        }

        public Task TryStartListening()
        {
            return _transport.ListenForConnectionsAsync();
        }

    }
}
