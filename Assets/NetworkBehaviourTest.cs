using UnityEngine;
using Nule;
using Nule.Packet;
using UnityEditor;

[InitializeOnLoad]
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
