using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop : MonoBehaviour
{

	AudioSource loopSource;

    // Start is called before the first frame update
	void Awake() {
   	loopSource = GetComponent<AudioSource>();
	}

	void Start() {

   	loopSource.pitch = loopSource.clip.length / MasterLoop.loopTime;
	}

   public void Unlock()
   {
   	loopSource.volume = 1.0f;
   }

    // Update is called once per frame
    public void NewLoop() {
    	loopSource.Play();
    }
}
