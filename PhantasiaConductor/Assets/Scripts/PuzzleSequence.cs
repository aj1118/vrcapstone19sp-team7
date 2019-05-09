using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSequence : MonoBehaviour
{

    public GameObject[] puzzles;
    public int currentPuzzle;

    // Start is called before the first frame update
    void Awake()
    {
        currentPuzzle = 0;
        puzzles[0].SetActive(true);
        for (int i = 1; i < puzzles.Length; i++)
        {
            puzzles[i].SetActive(false);
        }
    }
    
    public void NextPuzzle() {
        if (currentPuzzle < puzzles.Length - 1)
        {
            currentPuzzle++;
            puzzles[currentPuzzle].SetActive(true);
        }
    }
}
