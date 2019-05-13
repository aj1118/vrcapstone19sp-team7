using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonyHand : MonoBehaviour
{

	public GameObject hand;
	public bool unlocked;
    // Update is called once per frame
	void Update()
	{
		GetComponent<Renderer>().material.color = Color.HSVToRGB(transform.position.y % 1f,1f,1f);
		if (!unlocked) {
			transform.position = new Vector3(0, hand.transform.position.y, 0);
		}
	}
}