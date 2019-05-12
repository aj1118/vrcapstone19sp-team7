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

        // Start is called before the first frame update
        void Awake()
        {
            currentPuzzle = 0;
            puzzles[0].SetActive(true);
            for (int i = 1; i < puzzles.Length; i++)
            {
                puzzles[i].SetActive(false);
            }
            Debug.Log("CP" + currentPuzzle);
            Debug.Log("CLLL" + puzzles.Length);

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
            currentPuzzle++;
            Debug.Log("CP" + currentPuzzle);
            Debug.Log("CLLL" + puzzles.Length);
            Debug.Log(puzzles.Length + "HIIIIIZZZZZZZZZZZ" + currentPuzzle);
            GetComponent<AudioSource>().Play();
            if (currentPuzzle < puzzles.Length - 1)
            {
                //currentPuzzle++;
                puzzles[currentPuzzle].SetActive(true);
                Debug.Log("HIIIII" + currentPuzzle);
            } else {
                //leftHand.renderModelPrefab = leftOriginalPrefab;
                //rightHand.renderModelPrefab = rightOriginalPrefab;

                onPuzzleComplete.Invoke();
            }
        }
    }
}
