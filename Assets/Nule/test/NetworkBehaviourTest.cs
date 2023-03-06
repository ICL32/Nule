using UnityEngine;
using Nule;
using Nule.Packet;

namespace Assets.Nule.test
{
    public class NetworkBehaviourTest : NetworkBehaviour
    {
        private void Start()
        {
            TestGenerate();
        }

        [Rpc]
        private void TestGenerate()
        {
            Debug.Log("Method is called");
        }
    }
}