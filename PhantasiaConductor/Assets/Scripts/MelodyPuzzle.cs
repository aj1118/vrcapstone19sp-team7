﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MasterLoop))]
public class MelodyPuzzle : MonoBehaviour
{

    public GameObject melodyPrefab;

    public PuzzleMonitor monitor;

    public Flock flock;

    public GameObject boidEmitterPrefab;

    public MasterLoop masterLoop;

    private List<PathBeat> allPaths;


    // Start is called before the first frame update
    void Start()
    {
        allPaths = new List<PathBeat>();

        // var loopTime = masterLoop.loopTime;
        // var loopTime = 10;

        var path = CreateAndSetupPath("bezierpath", 4);
        // path.gameObject.SetActive(true);
        var path1 = CreateAndSetupPath("path1", 10);
        path1.gameObject.SetActive(false);

        var path2 = CreateAndSetupPath("path2", 10);
        path2.gameObject.SetActive(false);

        var path3 = CreateAndSetupPath("path3", 10);
        path3.gameObject.SetActive(false);
        

        foreach (var p in allPaths) {
            var go = p.obj.gameObject;
            MelodyObject mo = go.GetComponent<MelodyObject>();
            masterLoop.onNewLoop.AddListener(delegate() {
                mo.NewLoop();
            });
        }
        
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

    // Creates and path and puts it into all paths list
    PathBeat CreateAndSetupPath(string fileName, float t = 3)
    {
        PathBeat pathBeat = InstantiatePath(fileName, t);
        // BoidEmitter emitter = AddBoidEmitter(pathBeat);

        pathBeat.onReachedEnd.AddListener(delegate ()
        {
            Debug.Log("end was reached");
        });

        pathBeat.onSuccessful.AddListener(delegate ()
        {
            Debug.Log("successfully completed");
            // emitter.EmitBoids(6);
        });

        pathBeat.onFailed.AddListener(delegate ()
        {
            Debug.Log("failed");
        });

        allPaths.Add(pathBeat);
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
