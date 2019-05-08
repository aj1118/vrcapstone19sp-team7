using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class VisualFeedback : MonoBehaviour
    {
        public GameObject[] targets;

        public void MakeColorful()
        {
            Debug.Log("making colorful");
            foreach (GameObject target in targets)
            {
                // target.GetComponent<Glow>().GlowOn();
            }
        }

        public void ResetToBlank()
        {
            foreach (GameObject target in targets)
            {
                // target.GetComponent<Glow>().GlowOff();
            }
        }
    }
}
