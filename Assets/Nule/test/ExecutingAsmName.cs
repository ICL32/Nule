using System;
using Nule;
using UnityEngine;

namespace Nule.test
{
    public class ExecutingAsmName : MonoBehaviour
    {

        // Start is called before the first frame update
        private void Start()
        {
            var monobehaviourTypes = UnityEditor.TypeCache.GetTypesDerivedFrom<NetworkBehaviour>();
        
            foreach(Type type in monobehaviourTypes)
            {
                if (type.Assembly.FullName == "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null")
                {
                    Debug.Log(type.FullName);
                }
            }

        }
        // Update is called once per frame
        private void Update()
        {

        }
    
    }
}
