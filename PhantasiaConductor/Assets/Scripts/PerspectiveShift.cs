using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class PerspectiveShift : MonoBehaviour
    {
        public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

        public Hand leftHand;
        public Hand rightHand;
        public Camera playerCamera;

        public float speed;
        public float fadeDuration;
        public bool teleportEnabled;

        // to prevent motion sickness
        public GameObject motionOverlay; 

        private bool teleporting;

        private GameObject target;

        private bool MOUSE_DEBUG = true; 

        // Start is called before the first frame update
        void Start()
        {
            teleporting = false;
            target = null;
            
        }

        // Update is called once per frame
        void Update()
        {
            if (teleporting)
            {

                if (transform.position == target.transform.position) // reached destination
                {
                    StartCoroutine(FadeRotation());

                    teleporting = false;
                    motionOverlay.SetActive(false);
                }
                else // still moving
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                   
                    Vector3 overlayPos = playerCamera.transform.forward * 2 + playerCamera.transform.position;

                    motionOverlay.transform.position = overlayPos;

                }
            }
            else if (teleportEnabled)
            {
                if (WasTeleportButtonReleased(leftHand))
                {
                    Debug.Log("trying to teleport");
                    teleport(leftHand);
                }
                else if (WasTeleportButtonReleased(rightHand))
                {
                    teleport(rightHand);
                }
            }
        }

        private void teleport(Hand hand)
        {
            RaycastHit hit;
            Ray ray = new Ray(hand.transform.position, hand.transform.rotation * transform.forward);

            if (MOUSE_DEBUG)
            {
                ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            }

            Debug.Log("ray " + ray);
            Debug.Log(Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 2)));

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 2)))
            {
                Debug.Log("hit something");
                if (string.Equals(hit.collider.tag, "teleportDest"))
                {
                    teleporting = true;
                    motionOverlay.SetActive(true);
                    target = hit.collider.gameObject;

                    Debug.Log("hit " + hit.collider.gameObject.name);

                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
                }

            }
        }

        private IEnumerator FadeRotation()
        {
            // Fade to black
            SteamVR_Fade.Start(Color.clear, 0f);
            SteamVR_Fade.Start(Color.black, fadeDuration);

            yield return new WaitForSeconds(fadeDuration);

            transform.rotation = target.transform.rotation;

            yield return new WaitForSeconds(fadeDuration);

            // Fade back in
            SteamVR_Fade.Start(Color.black, 0f);
            SteamVR_Fade.Start(Color.clear, fadeDuration);
        }

        private bool WasTeleportButtonReleased(Hand hand)
        {
            if (MOUSE_DEBUG)
            {
                return Input.GetKeyUp(KeyCode.T);
            }
            else
            {
                return teleportAction.GetStateUp(hand.handType);
            }
        }
    }
}
