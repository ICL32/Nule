using UnityEngine;

namespace Nule.NuleClient
{
    public struct NetID
    {
        //Network ID is the Scene Object ID assigned by the host
        private uint _networkId;
        //Object ID is the GUID in the Scene
        private uint _objectId;
        
    }
}