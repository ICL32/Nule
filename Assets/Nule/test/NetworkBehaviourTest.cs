using UnityEngine;
using Nule;
using Nule.Packet;

namespace Assets.Nule.test
{
    public class NetworkBehaviourTest : NetworkBehaviour
    {
        [Rpc]
        private void Start()
        {
            Debug.Log(InstanceId, this.gameObject);
            Debug.Log(InstanceId, this.gameObject);
        }
    }
}