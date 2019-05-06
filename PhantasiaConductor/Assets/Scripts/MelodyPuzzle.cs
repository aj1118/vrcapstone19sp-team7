using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyPuzzle : MonoBehaviour
{

    public GameObject melodyPrefab;

    public PuzzleMonitor monitor;


    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(melodyPrefab, transform);

        go.transform.SetParent(transform);
        PathBeat pathBeat = go.GetComponent<PathBeat>();
        pathBeat.fileName = "path";

        pathBeat.onReachedEnd.AddListener(delegate() {
            Debug.Log("end was reached");
        });

        monitor.Register(pathBeat.onReachedEnd);
        monitor.onPuzzleCompleted.AddListener(delegate() {
            Debug.Log("puzzle was completed");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
