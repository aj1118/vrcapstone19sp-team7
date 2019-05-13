using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class HarmonyObject : MonoBehaviour
{
	public AudioSource loopSource;
	public UnityEvent onUnlock;
	public int[] notes;
	public bool unlocked;
	public float earlyStart = .1f;
	public float threshold = .01f;
	public float speed= .001f;
	public int notesPerOctave = 12;

	private float fadeIn = 1;
	public float velocityGoal = 0;
	public float velocity = 0;
	public int beatCount = 0;
	public float positionGoal = 0;
	public bool inContact = false;
	public bool moving = false;
	public float beatTime;

    // Start is called before the first frame update
    
	void Awake()
	{
		loopSource = GetComponent<AudioSource>();
		loopSource.volume = 0;
		beatTime = MasterLoop.loopTime / notes.Length;
		positionGoal = ((float)notes[beatCount]) / notesPerOctave;
		transform.position = new Vector3(0, positionGoal, 0);
		Debug.Log("HI");
	}

	void OnEnable(){
		FadeIn();
	}

	private void FadeIn() {
		fadeIn -= .01f;
		if (fadeIn > 0) {
			Invoke("FadeIn", .01f);
		} else {
			fadeIn = 0;
		}
	}

	// Update is called once per frame
	void Update()
	{

		Color color = Color.HSVToRGB(transform.position.y % 1f,1f,1f);
		if (unlocked || inContact) {
			color.a = .5f - fadeIn;
		} else {
			color.a = .2f - fadeIn;
		}
		GetComponent<Renderer>().material.color = color;

		if (moving) {
			if (Math.Abs(positionGoal - transform.position.y) < threshold) {
				transform.position = new Vector3(0, positionGoal, 0);
				moving = false;
			} else {
				velocityGoal = (positionGoal - transform.position.y) / 20f;
				if (velocity > velocityGoal) {
					velocity -= speed;
				} else {
					velocity += speed;
				}
				Vector3 delta = new Vector3(0, velocity, 0) ;
				transform.position += delta;
			}
		}
	}

	//runs slightly before each bit, so harmony object can get a head start
	public void EarlyRunBeat() {
		Debug.Log("RB");
		beatCount++;
		if (beatCount == notes.Length) {
			beatCount = 0;
		} else {
			Invoke("EarlyRunBeat", beatTime);
		}
		Debug.Log(beatTime);
		if (positionGoal != ((float)notes[beatCount]) / notesPerOctave) {
			positionGoal = ((float)notes[beatCount]) / notesPerOctave;
			moving = true;
		} else {
			moving = false;
		}
	}

	public void NewLoop(){
		loopSource.Play();
		beatCount = 0;
		Invoke("EarlyRunBeat", beatTime - (beatTime * earlyStart));
	}

	public void OnTriggerEnter()
	{
	  loopSource.volume = 1;
	  inContact = true;
	  Invoke("Unlock", MasterLoop.loopTime);

	}

	public void OnTriggerExit()
	{
	  inContact = false;
	  loopSource.volume = 0;
	  CancelInvoke("Unlock");
	}

	private void Unlock()
	{
		GetComponent<Collider>().enabled = false;
	  	loopSource.volume = 1;
	  	onUnlock.Invoke();
	}

}
