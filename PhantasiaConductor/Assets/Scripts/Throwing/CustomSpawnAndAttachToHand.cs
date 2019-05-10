using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class CustomSpawnAndAttachToHand : MonoBehaviour
    {

        [EnumFlags]
        public Hand.AttachmentFlags attachmentFlags = Hand.AttachmentFlags.ParentToHand | Hand.AttachmentFlags.DetachFromOtherHand | Hand.AttachmentFlags.TurnOnKinematic | Hand.AttachmentFlags.SnapOnAttach;

        //-------------------------------------------------
        public void SpawnAndAttach(Hand h, GameObject prefabObject)
        {
            h.AttachObject(prefabObject, GrabTypes.None, attachmentFlags);
            Debug.Log("attached object");
        }
    }
}
