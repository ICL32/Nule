using System;
using System.Net;
using UnityEngine;

namespace Nule
{
    public class NetworkManager : MonoBehaviour
    {
        public static event Action<int> OnConnected;
        public static event Action<int> OnDisconnected;
        
        [SerializeField] private bool _runInBackground = true;
        [SerializeField] private int _serverPort;
        [SerializeField] private string _ipAddress;

        public Transport.Transport Transport { get; private set; }
        private static NetworkManager _instance;
        
      

        private void Awake()
        {
            //If Singleton failed to initalize then don't execute any logic
            if (!TrySingletonInitialize())
            {
                return;
            }

            if (_ipAddress.Length == 0)
            {
                _ipAddress = "127.0.0.1";
            }
            IPAddress address = IPAddress.Parse(_ipAddress);
            Transport = new NuleTransport.NuleTransport(address, _serverPort);
            Debug.Log($"Creating TCP Client on: {address}:{_serverPort}");
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
    }
}
