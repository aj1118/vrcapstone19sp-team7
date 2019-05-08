using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class AddColor : MonoBehaviour
    {
        public Material blank;
        public Material color;

        public void SetColor()
        {
            GetComponent<Renderer>().material = color;
        }

        public void SetBlank()
        {
            GetComponent<Renderer>().material = blank;
        }
    }
}
