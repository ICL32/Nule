using UnityEngine;

namespace Nule
{
    [HideInInspector]
    public class NetworkBehaviour : MonoBehaviour
    {
        private static ulong _identifiers;
        public bool IsServer { get; internal set; }
        public bool IsLocalPlayer { get; internal set; }
        public ulong InstanceId { get; } = _identifiers++;
    }
}
