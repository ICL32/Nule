using UnityEngine;

namespace Nule
{
    [HideInInspector]
    public class NetworkBehaviour : MonoBehaviour
    {
        private static ulong _identifiers;
        public bool isServer { get; internal set; }
        public bool isLocalPlayer { get; internal set; }
        public ulong InstanceId { get; } = _identifiers++;
    }
}
