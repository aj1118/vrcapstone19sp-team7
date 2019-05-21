using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockAction : MonoBehaviour
{
	public Vector3 centerPosition;
	public bool unlocked;
	public bool inCenter;

	// Update is called once per frame
	void FixedUpdate()
	{
		if (unlocked) {
			if (inCenter) {

			} else {
				Vector3 delta = centerPosition - transform.position;
				if (delta.magnitude < .01f) {
					inCenter = true;
					transform.position = centerPosition;
				} else {
					transform.position += delta / 75;
				}
			}
		}
	}

	public void Unlock(){
		GameObject center = GameObject.Find("/CenterArea");
		transform.SetParent(center.transform);
		unlocked = true;
	}
}
