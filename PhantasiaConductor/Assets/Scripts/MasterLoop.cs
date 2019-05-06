using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MasterLoop : MonoBehaviour
{
    public UnityEvent onNewLoop;
    public static float loopTime = 5f;

    public static float delay = .4f;
    // Start is called before the first frame update


    private void OnEnable()
    {
        Invoke("NewLoop", delay);
    }

    void NewLoop()
    {
        onNewLoop.Invoke();
        Invoke("NewLoop", loopTime);
    }
    
}
