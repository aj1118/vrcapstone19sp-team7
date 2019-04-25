using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PObserver : MonoBehaviour
{

    private List<GameObject> subscribers;

    public void Subscribe(GameObject g)
    {
        if (!subscribers.Contains(g))
        {
            subscribers.Add(g);
        }
    }

    public void TriggerObserver(string e)
    {
        Debug.Log("triggered " + e);
    }

    public abstract void Run(string e); 
}
