using System.Net;
using UnityEngine;

namespace Nule
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] 
        private bool _runInBackground = true;
        [SerializeField] 
        private int _serverPort;
        [SerializeField]
        private string _ipAddress;
        
        private NuleTransport.NuleTransport transport;
      
        public static NetworkManager Instance { get; private set; }

        public NetworkManager()
        {
            
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
        
        //Event that gets called when a User Connects
        public void OnConnect()
        {
        }
  
        //Event that gets called when a User Leaves
        public void OnDisconnect()
        {
        }

    }
}
