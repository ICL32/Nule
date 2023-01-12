using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID : MonoBehaviour
{
    private GameObject _currentInstance;
    // Start is called before the first frame update
    void Start()
    {
        _currentInstance = this.gameObject;
        Debug.Log(_currentInstance.GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
