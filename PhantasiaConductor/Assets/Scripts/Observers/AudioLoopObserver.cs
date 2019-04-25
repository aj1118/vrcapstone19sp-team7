using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoopObserver : PObserver
{
    public AudioSource source;

    public void Start() {
        source.loop = true;
    }

    public override void Run(string e) {
        source.Play();
    }
}
