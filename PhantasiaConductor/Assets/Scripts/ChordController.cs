using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;


namespace Valve.VR.InteractionSystem
{
    [RequireComponent (typeof(CustomSpawnAndAttachToHand))]
    public class ChordController : MonoBehaviour
    {
        public GameObject[] targets;

        public Material targetOn;
        public Material targetOff;

        public UnityEvent onCompleteChord;

        private BeatInfo beatInfo;
        private CustomSpawnAndAttachToHand spawning;
        private int beatIndex = -1;
        private int targetIndex = 0; 
        private GameObject leftObj;
        private GameObject rightObj;

        private bool[] completed;
        private bool waitForLoop = false;
        private bool notePlaying = false;
        private bool complete = false;
        private bool invokeComplete = false;

        private void Awake()
        {
            beatInfo = transform.Find("BeatInfo").GetComponent<BeatInfo>();
            spawning = GetComponent<CustomSpawnAndAttachToHand>();
            completed = new bool[targets.Length];
        }

        private void OnEnable()
        {
            waitForLoop = true;
            beatIndex = -1;
            targetIndex = 0;
            RunTarget();
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        public void NewLoop()
        {
            if (!complete)
            {
                beatIndex = -1;
                targetIndex = 0;
                waitForLoop = false;
                notePlaying = false;
            } else if (invokeComplete)
            {
                invokeComplete = false;
                onCompleteChord.Invoke();
            }

        }

        private void RunTarget()
        {
            if (!waitForLoop)
            {
                bool nextBeat = beatInfo.beats[(beatIndex + 1) % beatInfo.beats.Length];
                beatIndex++;
                Debug.Log(targetIndex);
                Debug.Log(completed[targetIndex]);
                Debug.Log("next beat " + beatIndex);

                if (beatIndex  != 0 && beatInfo.beats[(beatIndex - 1) % beatInfo.beats.Length] 
                        && completed[targetIndex]) {
                    targets[targetIndex].GetComponent<Target_hit>().playTarget();
                    targetIndex++;
                }
                if (nextBeat && !completed[targetIndex])
                {
                    targets[targetIndex].GetComponent<Renderer>().material = targetOn;
                    targets[targetIndex].GetComponent<Target_hit>().CanHit = true;   
                    notePlaying = true;
                }
                else if (!nextBeat && notePlaying)
                {
                    Invoke("HitWindowOff", beatInfo.hittableAfter * beatInfo.beatTime);
                }
            }
            if (targetIndex < targets.Length)
            {
                Invoke("RunTarget", beatInfo.beatTime);
            } else {
                complete = true;
                invokeComplete = true;
            }
        }

        private void HitWindowOff() {
            targets[targetIndex].GetComponent<Renderer>().material = targetOff;
            targets[targetIndex].GetComponent<Target_hit>().CanHit = false;
            if (!completed[targetIndex]) {
                waitForLoop = true;
            }
            notePlaying = false;
            targetIndex++;
        }

        public void ResetTargets()
        {
            NewLoop();
            waitForLoop = true;
        }

        public void HitNote() {
            if (targetIndex == 0 || completed[targetIndex - 1]) {
                targets[targetIndex].GetComponent<Target_hit>().CanHit = false;
                completed[targetIndex] = true;
            }
        }
    }
}
