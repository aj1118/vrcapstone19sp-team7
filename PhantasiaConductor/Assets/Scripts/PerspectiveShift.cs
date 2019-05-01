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

        private bool teleporting;

        private GameObject target;

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
                }
                else // still moving
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

                }
            }

            else if (teleportEnabled && WasTeleportButtonReleased(leftHand))
            {
                teleport(leftHand);
            }
            else if (teleportEnabled && WasTeleportButtonReleased(rightHand))
            {
                teleport(rightHand);
            }
        }

        private void teleport(Hand hand)
        {
            RaycastHit hit;
            Ray ray = new Ray(hand.transform.position, hand.transform.rotation * transform.forward);

            if (hand.noSteamVRFallbackCamera != null)
            {
                ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            }
            
            Debug.Log(Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 2)));
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 2))) {
                if (string.Equals(hit.collider.tag, "teleportDest"))
                {
                    Debug.Log("hit");

                    teleporting = true;
                    target = hit.collider.gameObject;

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
            if (hand.noSteamVRFallbackCamera != null)
            {
                return Input.GetKeyUp(KeyCode.T);
            }
            else
            {
                return teleportAction.GetStateUp(hand.handType);
            }
        }

        private bool IsTeleportButtonDown(Hand hand)
        {
            if (hand.noSteamVRFallbackCamera != null)
            {
                return Input.GetKey(KeyCode.T);
            }
            else
            {
                return teleportAction.GetState(hand.handType);
            }
        }
    }
}
