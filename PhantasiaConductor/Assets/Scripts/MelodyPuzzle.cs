using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyPuzzle : MonoBehaviour
{

    public GameObject melodyPrefab;

    public PuzzleMonitor monitor;

    public Flock flock;


    // Start is called before the first frame update
    void Start()
    {
        var path = InstantiatePath("path", 10);
        // path.gameObject.SetActive(true);
        var path1 = InstantiatePath("path1", 10);
        path1.gameObject.SetActive(false);

        var path2 = InstantiatePath("path2", 10);
        path2.gameObject.SetActive(false);

        var path3 = InstantiatePath("path3", 10);
        path3.gameObject.SetActive(false);

        path.onSuccessful.AddListener(delegate () {
            path1.gameObject.SetActive(true);
            Debug.Log("success");
        });

        path1.onSuccessful.AddListener(delegate () {
            path2.gameObject.SetActive(true);
        });

        path2.onSuccessful.AddListener(delegate() {
            path3.gameObject.SetActive(true);
        });

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
