using System.Collections;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class DestinationActions : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject puzzlePrefab;
        public GameObject startingPosition;
        public int returnDelay = 1;

        private GameObject instruments;

        void Awake() {
            instruments = transform.Find("Instruments").gameObject;
        }

        public void onArrive() {
            Debug.Log("called onArrive");
            if (puzzlePrefab != null) 
            {
                Debug.Log("disabling children");
                Debug.Log(instruments);
                int childCount = instruments.transform.childCount;
                for (int i = 0; i < childCount; i++) {
                    // should probably animate this 
                    transform.GetChild(i).gameObject.SetActive(false);
                }

                Debug.Log("setting puzzle active");
                puzzlePrefab.SetActive(true);
            }
        }

        public void onPrepareToLeave() {
            if (puzzlePrefab != null) 
            {
                puzzlePrefab.SetActive(false);
                StartCoroutine(glowIndicator());
            }
        }

        private IEnumerator glowIndicator() {
            startingPosition.GetComponent<Glow>().GlowOn();
            
            int childCount = instruments.transform.childCount;
            for (int i = 0; i < childCount; i++) {
                Transform child = transform.GetChild(i);

                child.gameObject.SetActive(true); // should probably animate

                // child.GetComponent<AddColor>().SetColor();
            }

            yield return new WaitForSeconds(returnDelay);

            Player.instance.GetComponent<PerspectiveShift>().TeleportTo(startingPosition);
            startingPosition.GetComponent<Glow>().GlowOff();
        }
    }
}
