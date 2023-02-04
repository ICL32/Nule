using UnityEngine;

namespace Nule
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] 
        private bool _runInBackground;
        private Mode _state;
        
        private enum Mode
        {
            Offline,
            Hosting,
            Connected
        };

        public static NetworkManager Instance { get; internal set; }

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
