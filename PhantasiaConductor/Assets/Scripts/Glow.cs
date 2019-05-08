using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class Glow : MonoBehaviour
    {
        public Material glowMaterial;

        private Material originalMaterial;

        void Awake()
        {

            originalMaterial = GetComponent<Renderer>().material;
        }

        public void GlowOn()
        {
            GetComponent<Renderer>().material = glowMaterial;
        }

        public void GlowOff()
        {
            GetComponent<Renderer>().material = originalMaterial;
        }
    }
}
