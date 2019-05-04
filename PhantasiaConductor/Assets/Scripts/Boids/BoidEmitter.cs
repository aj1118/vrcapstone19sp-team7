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
        Assert.IsNotNull(flock);

        InvokeRepeating("EmitBoid", 0, 5);
    }

    public void EmitBoid() {
        flock.AddBoid(transform.position);
    }

    public void EmitBoids() {

    }
}
