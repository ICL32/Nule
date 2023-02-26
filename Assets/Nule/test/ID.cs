using UnityEngine;

namespace Assets.Nule.test
{
    public class ID : MonoBehaviour
    {
        private static uint _ids;
        
        // Start is called before the first frame update
        private void Start()
        {
            GameObject _currentInstance = gameObject;
            Debug.Log(_currentInstance.GetInstanceID(), gameObject);
        }

        // Update is called once per frame
        private void Update()
        {
        
        }
    }
}
