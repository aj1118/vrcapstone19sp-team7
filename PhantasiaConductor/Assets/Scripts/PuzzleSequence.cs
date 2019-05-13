using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Valve.VR.InteractionSystem
{
    public class PuzzleSequence : MonoBehaviour
    {

        public GameObject[] puzzles;
        private int currentPuzzle;

        public Hand leftHand;
        public Hand rightHand;

        public GameObject handObject;

        public UnityEvent onPuzzleComplete;

        private GameObject leftOriginalPrefab;
        private GameObject rightOriginalPrefab;
        private AudioSource source;

        // Start is called before the first frame update
        void Awake()
        {
            // source = GetComponent<AudioSource>();

            currentPuzzle = 0;
            puzzles[0].SetActive(true);
            for (int i = 1; i < puzzles.Length; i++)
            {
                puzzles[i].SetActive(false);
            }
        }
        /*
        public void OnEnable() {
            // set hands
            leftOriginalPrefab = leftHand.renderModelPrefab;
            rightOriginalPrefab = rightHand.renderModelPrefab;

            leftHand.renderModelPrefab = handObject;
            rightHand.renderModelPrefab = handObject;
        }
        */
        

        public void NextPuzzle() {
            // source.Play();
            currentPuzzle++;
            if (currentPuzzle < puzzles.Length - 2)
            {
                puzzles[currentPuzzle].SetActive(true);

            } else {
                //leftHand.renderModelPrefab = leftOriginalPrefab;
                //rightHand.renderModelPrefab = rightOriginalPrefab;
                onPuzzleComplete.Invoke();
            }
        }
    }
}
