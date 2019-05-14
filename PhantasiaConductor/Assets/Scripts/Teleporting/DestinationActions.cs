using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    public class DestinationActions : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject puzzleObj;
        public GameObject startingPosition;
        public int returnDelay = 1;
        public GameObject[] nextActive;
        public GameObject[] fadeObjects;
        public Material[] colorMaterials;
        public float loadDelay = 2f;

        public UnityEvent LoadPuzzle;
        public UnityEvent OnDepart;

        private GameObject instruments;
        private GameObject teleportIndicator;

        void Awake() {
            instruments = transform.Find("Instruments").gameObject;
            teleportIndicator = transform.Find("TeleportInd").gameObject;
        }

        public void onArrive() {
            if (teleportIndicator != null)
            {
                teleportIndicator.SetActive(false);
            }

            if (puzzleObj != null) 
            {
                startingPosition.SetActive(false);

                instruments.GetComponent<FadeChildren>().FadeOut();

                foreach (GameObject obj in fadeObjects)
                {
                    obj.GetComponent<FadeChildren>().FadeOut();
                }

                Player.instance.GetComponent<PerspectiveShift>().teleportEnabled = false;
                StartCoroutine(DelayedLoad());
            } 
        }

        public void onPrepareToLeave() {
            if (puzzleObj != null)
            {
                Player.instance.GetComponent<PerspectiveShift>().teleportEnabled = true;
                startingPosition.SetActive(true);
                StartCoroutine(glowIndicator());
            }
        }

        private IEnumerator DelayedLoad()
        {
            yield return new WaitForSeconds(loadDelay);
            LoadPuzzle.Invoke();
        }

        private IEnumerator glowIndicator() {

            OnDepart.Invoke();

            startingPosition.GetComponent<Glow>().GlowOn();

            yield return new WaitForSeconds(returnDelay);

            Renderer[] renderers = instruments.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                Color origColor = colorMaterials[i].color;
                Color newColor = new Color(origColor.r, origColor.g, origColor.b, 0.0f);

                Material newMaterial = colorMaterials[i];
                newMaterial.color = newColor;

                renderers[i].material = newMaterial;
            }

            Player.instance.GetComponent<PerspectiveShift>().TeleportTo(startingPosition);
            startingPosition.GetComponent<Glow>().GlowOff();

            yield return new WaitForSeconds(1);

            // Fade in
            instruments.GetComponent<FadeChildren>().FadeIn();

            foreach (GameObject obj in fadeObjects)
            {
                obj.GetComponent<FadeChildren>().FadeIn();
            }


            foreach (GameObject next in nextActive)
            {
                next.SetActive(true);
            }
        }
    }
}
