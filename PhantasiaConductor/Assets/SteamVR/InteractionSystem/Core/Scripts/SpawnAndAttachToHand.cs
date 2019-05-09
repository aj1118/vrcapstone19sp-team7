//======= Copyright (c) Valve Corporation, All rights reserved. ===============
//
// Purpose: Creates an object and attaches it to the hand
//
//=============================================================================

using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    //-------------------------------------------------------------------------
    public class SpawnAndAttachToHand : MonoBehaviour
    {
        public Hand hand;
        public GameObject prefab;

		public GameObject obj;

        [EnumFlags]
        public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic | Hand.AttachmentFlags.SnapOnAttach;

        void Start()
        {
            SpawnAndAttach(hand);
        }

        //-------------------------------------------------
        public void SpawnAndAttach(Hand h)
        {
            // GameObject prefabObject = Instantiate(prefab) as GameObject;
            // prefabObject.transform.parent = transform;

			// obj.transform.parent = transform;

            h.AttachObject(obj, GrabTypes.None, attachmentFlags);
			Debug.Log(h.currentAttachedObject + " info " + h.currentAttachedObjectInfo);
			// Interactable interactable = h.GetComponent<Interactable>();
			// h.HoverLock(interactable);
        }

		void Update() {
			// Debug.Log(hand.currentAttachedObject == null);
		}
    }
}
