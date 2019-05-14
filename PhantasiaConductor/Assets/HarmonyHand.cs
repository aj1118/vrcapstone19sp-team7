using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonyHand : MonoBehaviour
{
    //NOTE: The harmony puzzle requires that this object is the collider, and the normal hand object is not a collider. I've been using the prefab object SphereHandNoCollider.

	public GameObject hand;
	public bool unlocked;
    private void Awake()
    {
        //hand.GetComponent<Collider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
	{
		if (unlocked) {
            GetComponent<Renderer>().material.color = Color.HSVToRGB(transform.position.y % 1f, .7f, 1f);
            
            //Cool effect.. but nauseating!
            //transform.rotation = Quaternion.Euler(-90, transform.position.y % 1f * 180, 0);
        } else
        {
            GetComponent<Renderer>().material.color = Color.HSVToRGB(transform.position.y % 1f, .3f, 1f);
            transform.position = new Vector3(0, hand.transform.position.y, 0);
        }
	}
}