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
    public PuzzleSequence puzzleSequence;

    private AudioSource loopSource;
    private Collider coll;
    private MeshRenderer rend;
    private bool inWindow;
    private bool inContact;

    void Awake()
    {
        coll = GetComponent<Collider>();
        rend = GetComponent<MeshRenderer>();
        loopSource = GetComponent<AudioSource>();
        inContact = false;
        inWindow = false;
        loopSource.clip = loopClip;
        loopSource.pitch = loopClip.length / MasterLoop.loopTime;
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
        loopSource.volume = 1;
        inContact = true;
    }

    private void Start()
    {
        TurnOff();
    }

    public void NewLoop()
    {
        loopSource.Play();
        Invoke("StartWindow", MasterLoop.loopTime * startTime);
        Invoke("StartPlay", MasterLoop.loopTime * (startTime + windowLength));
        Invoke("EndPlay", MasterLoop.loopTime * endTime);
    }
    
    private void StartWindow()
    {
        TurnOn();
        inWindow = true;
    }

    private void StartPlay()
    {
        inWindow = false;
        if (!inContact)
        {
            loopSource.volume = 0;
            TurnOff();
        }
        rend.enabled = true;
        coll.enabled = true;
    }

    private void EndPlay()
    {
        Debug.Log("AAAAAZZZAAA");
        if (unlocked)
        {
            TurnOff();
        }
        else if (inContact)
        {
            Debug.Log("AAAAAAAAAAAA");
            TurnOff();
            unlocked = true;
            //puzzleSequence.NextPuzzle();
        }

    }

    private void TurnOn()
    {
        if (!unlocked)
        {
            coll.enabled = true;
        }
        rend.enabled = true;
    }

    private void TurnOff()
    {
        if (!unlocked)
        {
            coll.enabled = false;
        }
        rend.enabled = false;
    }

    public void OnTriggerEnter()
    {
        loopSource.volume = 1;
        inContact = true;
    }

    public void OnTriggerExit()
    {
        inContact = false;

        if (!inWindow)
        {
            loopSource.volume = 0;
            TurnOff();
        }
    }
}