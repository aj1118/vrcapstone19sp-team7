using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class CustomSpawnAndAttachToHand : MonoBehaviour
    {

         public Hand hand;
        public GameObject prefab;

		public GameObject obj;

        [EnumFlags]
        public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic | Hand.AttachmentFlags.SnapOnAttach;

        void Start()
        {
            // SpawnAndAttach(hand);
        }

        //-------------------------------------------------
        public GameObject SpawnAndAttach(Hand h)
        {
            GameObject prefabObject = Instantiate(prefab) as GameObject;
            prefabObject.transform.parent = transform;

            h.HoverLock(null);
			
            // obj.transform.parent = transform;
            
            h.AttachObject(prefabObject, GrabTypes.None, attachmentFlags);
            Debug.Log(h.currentAttachedObject + " info " + h.currentAttachedObjectInfo);

            return prefabObject;
        }

		void Update() {
			// Debug.Log(hand.currentAttachedObject == null);
		}
    }
}
