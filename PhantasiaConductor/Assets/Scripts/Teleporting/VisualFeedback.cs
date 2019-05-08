using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class VisualFeedback : MonoBehaviour
    {
        public GameObject[] targets;
        public Material glowMaterial;

        private void Awake()
        {
            foreach (GameObject target in targets)
            {
                target.GetComponent<Glow>().glowMaterial = glowMaterial;
            }
        }

        public void GlowTargets()
        {
            foreach (GameObject target in targets)
            {
                target.GetComponent<Glow>().GlowOn();
            }
        }

        public void StopGlowTargets()
        {
            foreach (GameObject target in targets)
            {
                target.GetComponent<Glow>().GlowOff();
            }
        }
    }
}
