using System;
using System.Net;
using UnityEngine;

namespace Nule
{
    public class NetworkManager : MonoBehaviour
    {
        public event Action<int> OnConnected;
        public event Action<int> OnDisconnected;
        
        [SerializeField] private bool _runInBackground = true;
        [SerializeField] private int _serverPort;
        [SerializeField] private string _ipAddress;

        private NuleTransport.NuleTransport transport;
        private static NetworkManager Instance;
        
        public NetworkManager()
        {
            IPAddress address = IPAddress.Parse(_ipAddress);
            transport = new NuleTransport.NuleTransport(address, _serverPort);
        }

        private void Awake()
        {
            SingletonInitialize();
            Application.runInBackground = _runInBackground;
        }


        /// <summary>This method is responsible for checking there is only one instance of NetworkManager at a time</summary>
        private bool SingletonInitialize()
        {
            if (Instance != null)
            {
                if (Instance == this)
                {
                    return true;
                }

                if (Instance != this)
                {
                    Debug.LogWarning("Duplicate Network Manager component found and will be destroyed", gameObject);
                    Destroy(this);
                    return false;
                }
            }

            //If instance is null then assign this instance
            Instance = this;
            return true;
        }
    }
}
