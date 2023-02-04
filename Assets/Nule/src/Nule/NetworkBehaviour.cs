using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Nule
{
    public class NetworkBehaviour : MonoBehaviour
    {
        public bool isServer { get; internal set; }
        public bool isLocalPlayer { get; internal set; }


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
