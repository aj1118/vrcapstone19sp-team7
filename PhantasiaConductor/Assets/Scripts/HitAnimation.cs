using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimation : MonoBehaviour
{
	Animator anim;
    public string animationName;
    Animation animClip;

    // Start is called before the first frame update
    void Awake()
    {
    	anim = GetComponent<Animator>();
        animClip = GetComponent<Animation>();
    }

    void OnEnable() {

        //assumes only one animation, janky
        anim.Play(animationName);
        float speed = anim.GetCurrentAnimatorStateInfo(0).speed;
        Invoke("GoodbyeCruelWorld", animClip.clip.length / speed);
    }

    void GoodbyeCruelWorld() {
        gameObject.SetActive(false);
    }
}
