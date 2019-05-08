using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MasterLoop : MonoBehaviour
{
    public UnityEvent onNewLoop;
    public static float loopTime = 4f;
    
     public static float standardLoopTime = 4f; //The 'default' time for one loop (audio rendered at this tempo)
    
    public static float delay = .3f;

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
