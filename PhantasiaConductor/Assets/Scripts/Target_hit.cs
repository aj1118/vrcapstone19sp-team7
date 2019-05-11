using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Target_hit : MonoBehaviour
    {
        public Material Glow;
        public Material def;
        public AudioClip chord1;
        bool hitTarget = false;

        void Start()
        {
            GetComponent<Renderer>().material = def;
            GetComponent<AudioSource>().playOnAwake = false;
            GetComponent<AudioSource>().clip = chord1;

        }
        void OnCollisionEnter(Collision col)
        {
            hitTarget = !hitTarget;
            if (hitTarget)
            {
                GetComponent<Renderer>().material = Glow;
                GetComponent<AudioSource>().Play();
                Invoke("goBack", 0.2f);
            }
        }

        void goBack()
        {
            GetComponent<Renderer>().material = def;
            hitTarget = !hitTarget;
        }
    }
