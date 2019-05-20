using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    public class DestinationActions : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject puzzleObj;
        public GameObject[] makeColorful;
        public Material[] colorMaterials;
        public float loadDelay = 2f;

        public Hand leftHand;
        public Hand rightHand;
        public GameObject handObject;

        public UnityEvent LoadPuzzle;
        public UnityEvent OnDepart;

        private GameController gameController;

        void Awake() {
            gameController = transform.parent.transform.GetComponent<GameController>();
        }

        public void onArrive() {
            if (puzzleObj != null) 
            {
                gameController.FadeOutAll();
              
                Player.instance.GetComponent<PerspectiveShift>().teleportEnabled = false;
                StartCoroutine(DelayedLoad());
            }
            leftHand.SetRenderModel(handObject);
            rightHand.SetRenderModel(handObject);
        }

        public void onPrepareToLeave() {
            if (puzzleObj != null)
            {
                // Make instruments colorful
                for (int i = 0; i < makeColorful.Length; i++)
                {
                    Color orig = colorMaterials[i].color;
                    Color newColor = new Color(orig.r, orig.g, orig.b, 0.0f);
                    Material newMaterial = colorMaterials[i];
                    newMaterial.color = newColor;

                    makeColorful[i].GetComponent<Renderer>().material = newMaterial;
                }

                OnDepart.Invoke();
            }
            leftHand.SetRenderModel(null);
            rightHand.SetRenderModel(null);
        }

        private IEnumerator DelayedLoad()
        {
            yield return new WaitForSeconds(loadDelay);
            LoadPuzzle.Invoke();
        }
    }
}
