using UnityEngine;

namespace Nule
{
    [HideInInspector]
    public class NetworkBehaviour : MonoBehaviour
    {
        public bool isServer { get; internal set; }
        public bool isLocalPlayer { get; internal set; }
        private static ulong _identifiers;
        public ulong InstanceId { get; } = _identifiers++;
    }
}
