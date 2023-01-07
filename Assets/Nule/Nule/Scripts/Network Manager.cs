using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] 
    private bool _runInBackground;
    public enum Mode
    {
        Offline,
        Hosting,
        Connected
    };

    public static NetworkManager Instance { get; internal set; }

    void Awake()
    {
        SingletonInitialize();
    }
    
    
    /// <summary>This method is responsible for checking there is only one instance of NetworkManager at a time</summary>
    public bool SingletonInitialize()
    {
        if (Instance != null)
        {
            if (Instance == this)
            {
                return true;
            }

            if (Instance != this)
            {
                Destroy(this);
                return false;
            }
        }
        //If instance is null then assign this instance
        Instance = this;
        return true;
    }
}
