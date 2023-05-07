using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class MonoBehaviourTester : MonoBehaviour
{
    private string MyString;

    void Start()
    {
        Debug.Log("Hi"); // Add this line
        Debug.Log("MyString: " + MyString);
    }
}
