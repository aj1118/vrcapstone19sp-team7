using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyPuzzle : MonoBehaviour
{

    public GameObject melodyPrefab;

    public PuzzleMonitor monitor;

    public Flock flock;

    public GameObject boidEmitterPrefab;


    // Start is called before the first frame update
    void Start()
    {
        var path = CreateAndSetupPath("path", 10);
        // path.gameObject.SetActive(true);
        var path1 = CreateAndSetupPath("path1", 10);
        path1.gameObject.SetActive(false);

        var path2 = CreateAndSetupPath("path2", 10);
        path2.gameObject.SetActive(false);

        var path3 = CreateAndSetupPath("path3", 10);
        path3.gameObject.SetActive(false);

        path.onSuccessful.AddListener(delegate ()
        {
            path1.gameObject.SetActive(true);
            Debug.Log("success");

        });

        path1.onSuccessful.AddListener(delegate ()
        {
            path2.gameObject.SetActive(true);
        });

        path2.onSuccessful.AddListener(delegate ()
        {
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

        monitor.Register(pathBeat.onReachedEnd);

        pathBeat.SetCompletionTime(t);
        return pathBeat;
    }

    PathBeat CreateAndSetupPath(string fileName, float t = 3)
    {
        PathBeat pathBeat = InstantiatePath(fileName, t);
        BoidEmitter emitter = AddBoidEmitter(pathBeat);

        pathBeat.onReachedEnd.AddListener(delegate ()
        {
            Debug.Log("end was reached");
        });

        pathBeat.onSuccessful.AddListener(delegate ()
        {
            Debug.Log("successfully completed");
            emitter.EmitBoids(6);
        });

        pathBeat.onFailed.AddListener(delegate ()
        {
            Debug.Log("failed");
        });

        
        return pathBeat;
    }

    BoidEmitter AddBoidEmitter(PathBeat path)
    {
        GameObject go = Instantiate(boidEmitterPrefab);
        go.transform.parent = path.obj.transform;

        BoidEmitter emitter = go.GetComponent<BoidEmitter>();
        emitter.flock = flock;

        go.transform.position = path.obj.transform.position;
        return emitter;
    }
}
