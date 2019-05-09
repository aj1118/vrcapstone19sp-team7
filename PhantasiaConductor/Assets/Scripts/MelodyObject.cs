using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyObject : MonoBehaviour
{
    
    public AudioClip loopClip;

    public float startTime;
    public float endTime;
    public float windowLength;
    public bool unlocked;

    // Start is called before the first frame update
    
    private AudioSource loopSource;
    private Hittable hittable;
    private bool inWindow;

    private bool inContact;

    //0 - inactive
    //1 - start window
    //2 - in play
    //3 - unlocked


    // We can remove this and set values in the prefab
    void Awake()
    {
        inContact = false;
        GetComponent<MeshRenderer>().enabled = false;
        loopSource = transform.parent.transform.Find("LoopSource").GetComponent<AudioSource>();
        loopSource.pitch = loopClip.length / MasterLoop.loopTime;
        
        //adds 3d(ish) sound
        loopSource.spatialBlend = 1.0f;
        
        loopSource.clip = loopClip;
        
        hittable = GetComponent<Hittable>();
        
    }
    private void Update()
    {
        if (!unlocked)
        {
        }
    }

    public void OnTriggerEnter()
    {
        // if ()
        loopSource.volume = 1;
        inContact = true;
    }

    public void OnTriggerExit()
    {
        loopSource.volume = 0;
        inContact = false;
    }
    

    public void NewLoop()
    {
        loopSource.Play();

        Invoke("StartWindow", MasterLoop.loopTime * startTime);
        Invoke("EndWindow", MasterLoop.loopTime * (startTime + windowLength));

    }

    private void StartWindow()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    private void StartPlay()
    {
        // windowLength = false;
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }


    private void EndPlay()
    {
        // if 
    }

    private void TurnOn()
    {

    }

    private void TurnOff()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
