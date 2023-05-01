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

        public NuleTransport.NuleTransport Transport { get; private set; }
        private static NetworkManager _instance;
        
        public NetworkManager()
        {
            IPAddress address = IPAddress.Parse(_ipAddress);
            Transport = new NuleTransport.NuleTransport(address, _serverPort);
        }

        private void Awake()
        {
            SingletonInitialize();
            Application.runInBackground = _runInBackground;
        }


        /// <summary>This method is responsible for checking there is only one instance of NetworkManager at a time</summary>
        private bool SingletonInitialize()
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
                    Destroy(this);
                    return false;
                }
            }

            //If instance is null then assign this instance
            _instance = this;
            return true;
        }
    }
}
