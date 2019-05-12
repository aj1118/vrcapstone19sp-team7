using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    public class PerspectiveShift : MonoBehaviour
    {
        public bool MOUSE_DEBUG = false;

        public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
       
        public Hand leftHand;
        public Hand rightHand;
        public Camera playerCamera;

        public float speed;
        public float rotateSpeed;
        public bool teleportEnabled;

        // to prevent motion sickness
        public GameObject motionOverlay;

        private bool teleporting;

        private GameObject target;
        private Vector3 targetPosition;
        private CustomLaserPointer leftLaser;
        private CustomLaserPointer rightLaser;

        void Awake()
        {
            leftLaser = leftHand.GetComponent<CustomLaserPointer>();
            rightLaser = rightHand.GetComponent<CustomLaserPointer>();

            leftLaser.active = false;
            leftLaser.PointerIn += onPointerIn;
            leftLaser.PointerOut += onPointerOut;

            rightLaser.active = false;
            rightLaser.PointerIn += onPointerIn;
            rightLaser.PointerOut += onPointerOut;
        } 

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
                if (transform.position == targetPosition) // reached destination
                {
                    if (transform.rotation == target.transform.rotation) // stopped
                    {
                        teleporting = false;
                        motionOverlay.SetActive(false);

                        target.transform.parent.transform.GetComponent<DestinationActions>().onArrive();
                    }
                    else // rotate 
                    {
                        float step = rotateSpeed * Time.deltaTime;
                        transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);

                        Vector3 overlayPos = playerCamera.transform.forward * 2 + playerCamera.transform.position;

                        motionOverlay.transform.position = overlayPos;
                    }
                }
                else // still moving
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                    Vector3 overlayPos = playerCamera.transform.forward * 2 + playerCamera.transform.position;

                    motionOverlay.transform.position = overlayPos;

                }
            }
            else if (teleportEnabled)
            {
                if (WasTeleportButtonReleased(leftHand))
                {
                    teleport(leftHand);
                    leftLaser.active = false;
                }
                else if (WasTeleportButtonReleased(rightHand))
                {
                    teleport(rightHand);
                    rightLaser.active = false;
                }
                else if (IsTeleportButtonDown(leftHand) && !IsTeleportButtonDown(rightHand))
                {
                    leftLaser.active = true; 
                }
                else if (IsTeleportButtonDown(rightHand) && !IsTeleportButtonDown(leftHand))
                {
                    rightLaser.active = true;
                }
            }
        }

        private void teleport(Hand hand)
        {
            RaycastHit hit;
            Ray ray = new Ray(hand.transform.position, hand.transform.forward);
            
            if (MOUSE_DEBUG)
            {
                ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            }

            Debug.Log("ray " + ray);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~(1 << 2)))
            {
                if (string.Equals(hit.collider.tag, "teleportDest"))
                {
                    teleporting = true;
                    motionOverlay.SetActive(true);

                    target = hit.collider.gameObject;
                    targetPosition = new Vector3(target.transform.position.x, target.transform.position.y - 1.5f, target.transform.position.z);
                    // hide the target when start moving
                    target.gameObject.SetActive(false);

                    Debug.Log("hit " + hit.collider.gameObject.name);

                    float step = speed * Time.deltaTime;
                    transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
                }

            }
        }

        private bool IsTeleportButtonDown(Hand hand)
        {
            if (MOUSE_DEBUG)
            {
                return Input.GetKeyDown(KeyCode.T);
            }
            else
            {
                return teleportAction.GetStateDown(hand.handType);
            }
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

        // Laser Pointer Methods

        private void onPointerIn(object sender, PointerEventArgs e)
        {
            if (string.Equals(e.target.tag, "teleportDest"))
            {
                e.target.gameObject.GetComponent<Glow>().GlowOn();
            }
        }

        private void onPointerOut(object sender, PointerEventArgs e)
        {
            if (string.Equals(e.target.tag, "teleportDest"))
            {
                e.target.gameObject.GetComponent<Glow>().GlowOff();
            }
        }

        public void TeleportTo(GameObject destination) 
        {
            Debug.Log("teleporting to " + destination.name);
            teleporting = true;
            motionOverlay.SetActive(true);
            target = destination;

            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
            targetPosition = target.transform.position;
        }
    }
}
