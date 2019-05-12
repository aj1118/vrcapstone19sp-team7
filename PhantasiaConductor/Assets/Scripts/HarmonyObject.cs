using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonyObject : MonoBehaviour
{
	public GameObject hand;
	public int[] notes;
	
	public bool unlocked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	Color color = Color.HSVToRGB(transform.position.y % 1f,1f,1f);
    	color.a = .5f;
    	GetComponent<Renderer>().material.color = color;
    	// transform.position.y = hand.GetComponent<transform>().position.y;
    }

}
