using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Glow : MonoBehaviour
    {
        public Material glowMaterial;
        public float blinkFreq = 1f;

        private Material originalMaterial;
        private int glowCount = -1;

        void Awake()
        {

            originalMaterial = GetComponent<Renderer>().material;
        }

        public void GlowBlink()
        {
            

        }

        public void GlowOn()
        {
            originalMaterial = GetComponent<Renderer>().material;
            GetComponent<Renderer>().material = glowMaterial;
        }

        public void GlowOff()
        {
            GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
