using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BoidEmitter : MonoBehaviour
{
    // The flock the boids will join
    public Flock flock;

    void Start()
    {
        if (flock == null) {
            flock = GameObject.Find("Flock").GetComponent<Flock>();
        }
        InvokeRepeating("EmitBoid", 0, 5);
    }

    public void EmitBoid() {
        EmitBoids(1);
    }

    public void EmitBoids(int n) {
        for (var i = 0; i < n; i++) {
            flock.AddBoid(transform.position);
        }
    }

}
