using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonyHand : MonoBehaviour
{

	public GameObject hand;

    // Update is called once per frame
	void Update()
	{
		//GetComponent<Renderer>().material.color = Color.HSVToRGB(transform.position.y % 1f,.3f,1f);
		Vector3 pos = new Vector3(0, hand.transform.position.y, 0);
		//transform.position = pos;
	}
}