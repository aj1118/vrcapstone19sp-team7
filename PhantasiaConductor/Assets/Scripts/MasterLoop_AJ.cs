using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MasterLoop_AJ : MonoBehaviour
{
    public UnityEvent onNewLoop;
    public UnityEvent unlockedLoop;

    public static float loopTime = 4f;

    public static float unitLoopTime = 4f; //The 'default' time for one loop
    
    public static float delay = .3f;
    // Start is called before the first frame update

    public GameObject[] Targets = new GameObject[4];

    private void OnEnable()
    {
        if (CheckUnlocked())
        {
            Invoke("UnlockedLoop", delay);
        }     

        else
        {
            Invoke("NewLoop", delay);
        }
    }

    void NewLoop()
    {
        onNewLoop.Invoke();
        Invoke("NewLoop", loopTime);
    }
    void UnlockedLoop()
    {
        unlockedLoop.Invoke();
        Invoke("UnlockedLoop", loopTime);
    }
    private bool CheckUnlocked()
    {
        if (Targets[0].GetComponent<BeatBlinkController>().unlocked && Targets[1].GetComponent<BeatBlinkController>().unlocked && Targets[2].GetComponent<BeatBlinkController>().unlocked && Targets[3].GetComponent<BeatBlinkController>().unlocked)
        {
            return true;
        }
        else
        {
            return false;
        }     
    }
}
