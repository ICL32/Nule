using Nule;
using Nule.Weaver.Attributes;
using UnityEngine;
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
