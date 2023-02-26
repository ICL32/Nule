
using UnityEngine;

namespace Nule
{
    public class NetworkBehaviour : MonoBehaviour
    {
        public bool isServer { get; internal set; }
        public bool isLocalPlayer { get; internal set; }
        private static ulong _identifiers;
        public ulong InstanceId { get; } = _identifiers++;

        //Event that gets called when a User Connects
        public virtual void OnConnect()
        {
        }
  
        //Event that gets called when a User Leaves
        public virtual void OnDisconnect()
        {
        }
        
    }
}
