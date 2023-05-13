using UnityEngine;

namespace Nule
{
    [HideInInspector]
    public class NetworkBehaviour : MonoBehaviour
    {
        private static ulong incrementalCounter;
        
        public bool IsServer { get; internal set; }
        public bool IsLocalPlayer { get; internal set; }
        
        //Instance ID is unique for each object, to check for local player check that NetworkManager ID matches with object
        internal ulong InstanceId { get; } = incrementalCounter++;
        
        //Each network manager has an ID to associate client's authority over object. The ID is assigned by the server network manager to client's network manager
        internal uint AuthorityId;
    }
}
