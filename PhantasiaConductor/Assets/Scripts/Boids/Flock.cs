﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    // number of boids to spawn
    public uint flockSize = 20;

    public float minVelocity = 5;
    public float maxVelocity = 20;

    public float randomness = 1;

    public GameObject boidPrefab;

    public GameObject chasee;

    public Vector3 flockCenter;

    // public Vector3 flockVelocity;
    public float flockVelocity = 6.0f;

    public float spawnRadius = 10;

    public float neighborRadius = 2;

    public float velocityVariation = 0.5f;

    public float rotationCoef = 4.0f;

    public LayerMask searchLayer;

    private List<Boid> boids = new List<Boid>();



    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < flockSize; i++)
        {
            SpawnBoid();
        }
    }

    void SpawnBoid()
    {
        Vector3 pos = transform.position + Random.insideUnitSphere * 10;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Random.rotation, 0.3f);
        Boid boid = Instantiate(boidPrefab, transform.position, rot).GetComponent<Boid>();
        boid.transform.parent = transform;
        boid.transform.position = pos;
        boids.Add(boid);
        boid.SetFlock(this);
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 center = Vector3.zero;
        // Vector3 velocity = Vector3.zero;

        // foreach (Boid boid in boids)
        // {
        //     center += boid.transform.localPosition;
        //     velocity += boid.rigidBody.velocity;
        // }

        // center /= flockSize;
        // velocity /= flockSize;

        // flockCenter = center;
        // flockVelocity = velocity;
    }
}
