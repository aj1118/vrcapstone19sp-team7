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
        InstantiatePath("path", 5);
        InstantiatePath("path1", 2);
        InstantiatePath("path2", 3);
        PathBeat pathBeat = InstantiatePath("path3", 20);

        monitor.onPuzzleCompleted.AddListener(delegate ()
        {
            Debug.Log("All puzzles were completed");
        });

        
    }

    PathBeat InstantiatePath(string fileName, float t = 3)
    {
        GameObject go = Instantiate(melodyPrefab, transform);

        go.transform.SetParent(transform);
        PathBeat pathBeat = go.GetComponent<PathBeat>();
        pathBeat.LoadFromFile(fileName);

        pathBeat.onReachedEnd.AddListener(delegate ()
        {
            Debug.Log("end was reached");
        });

        pathBeat.onSuccessful.AddListener(delegate() {
            Debug.Log("successfully completed");
        });

        pathBeat.onFailed.AddListener(delegate() {
            Debug.Log("failed");
        });

        monitor.Register(pathBeat.onReachedEnd);

        pathBeat.SetCompletionTime(t);
        return pathBeat;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
