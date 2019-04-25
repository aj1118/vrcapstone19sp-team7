using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceLoop : MonoBehaviour
{
    public AudioSource source;

    public float delay = 0;
    // Start is called before the first frame update

    public bool isPlaying;

    public void PlayOnce()
    {
        source.Play();
    }

    public void PlayLooping()
    {
        InvokeRepeating("PlayOnce", 0, delay);
    }

    public void StopLooping()
    {
        CancelInvoke();
    }

    public float Delay {
        get {
            return delay;
        }

        set {
            if (isPlaying) {
                StopLooping();
                delay = value;
                PlayLooping();
            }
        }
    }
}
