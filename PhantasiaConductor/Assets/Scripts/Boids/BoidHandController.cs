using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Valve.VR.InteractionSystem
{
    public class BoidHandController : MonoBehaviour
    {

        public Hand leftHand;
        public Hand rightHand;

        public GameObject rightTarget;
        public GameObject leftTarget;

        public float placementRadius = 20;


        // Update is called once per frame
        void Update()
        {

            PlaceTarget(rightTarget, rightHand);
            PlaceTarget(leftTarget, leftHand);
        }

        void PlaceTarget(GameObject targ, Hand hand)
        {
            var dir = hand.transform.rotation * transform.forward;
            var pos = placementRadius * dir + hand.transform.position;

            targ.transform.position = pos;
        }
    }
}