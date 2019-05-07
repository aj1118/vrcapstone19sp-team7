using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimation : MonoBehaviour
{
    public float speed;
	Animator anim;
    public string animationName;
    Animation animClip;

    // Start is called before the first frame update
    void Awake()
    {
       
    	anim = GetComponent<Animator>();
        anim.speed = speed;
        animClip = GetComponent<Animation>();
    }

    void OnEnable() {
        //Debug.Log("AANIME");
        //assumes only one animation, janky
        anim.Play(animationName);
        Invoke("GoodbyeCruelWorld", animClip.clip.length / speed);
    }

    void GoodbyeCruelWorld() {
        gameObject.SetActive(false);
    }
}
