using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    Flock flock;

    public float minVelocity;

    public float maxVelocity;

    public float randomness;

    public Rigidbody rigidBody;

    private bool inited;

    private GameObject chasee;

    float noiseOffset;


    // Start is called before the first frame update
    void Start()
    {
        noiseOffset = Random.value * 10.0f;

        // rigidBody = GetComponent<Rigidbody>();
        // if (rigidBody == null)
        // {
        //     Debug.Log("boid needs rigidbody");
        // }
        // StartCoroutine("Steering");
    }

    Vector3 GetSeparationVector(Transform target) {
        var diff = transform.position - target.transform.position;
        var diffLen = diff.magnitude;
        var scaler = Mathf.Clamp01(1.0f - diffLen / flock.neighborRadius);
        return diff * (scaler / diffLen);
    }

    // IEnumerator Steering()
    // {
    //     while (true)
    //     {
    //         if (inited)
    //         {
    //             rigidBody.velocity = rigidBody.velocity + Calc() * Time.deltaTime;

    //             float speed = rigidBody.velocity.magnitude;
    //             if (speed > maxVelocity)
    //             {
    //                 rigidBody.velocity = rigidBody.velocity.normalized * maxVelocity;
    //             }
    //             else if (speed < minVelocity)
    //             {
    //                 rigidBody.velocity = rigidBody.velocity.normalized * minVelocity;
    //             }
    //         }

    //         float waitTime = Random.Range(0.3f, 0.5f);
    //         yield return new WaitForSeconds(waitTime);
    //     }
    // }

    void Update() {
        var currentPos = transform.position;
        var currentRot = transform.rotation;

        var noise = Mathf.PerlinNoise(Time.time, noiseOffset) * 2.0f - 1.0f;
        var velocity = flock.flockVelocity * (1.0f + noise);

        var separation = Vector3.zero;
        var alignment = flock.transform.forward;
        var cohesion = flock.transform.position;

        var nearbyBoids = Physics.OverlapSphere(currentPos, flock.neighborRadius, flock.searchLayer);
        foreach (var boid in nearbyBoids) {
            if (boid.gameObject == gameObject) continue;
            var t = boid.transform;
            separation += GetSeparationVector(t);
            alignment += t.forward;
            cohesion += t.position;
        }

        var avg = 1.0f / nearbyBoids.Length;
        alignment *= avg;
        cohesion *= avg;
        cohesion = (cohesion - currentPos).normalized;

        var direction = separation + alignment + cohesion;
        var rotation = Quaternion.FromToRotation(Vector3.forward, direction.normalized);
        if (rotation != currentRot) {
            var ip = Mathf.Exp(-flock.rotationCoef * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(rotation, currentRot, ip);
        }

        transform.position = currentPos + transform.forward * (velocity * Time.deltaTime);

    }

    // Vector3 Calc()
    // {
    //     Vector3 randomize = new Vector3((Random.value * 2) - 1, (Random.value * 2) - 1, (Random.value * 2) - 1);
    //     randomize.Normalize();

    //     Vector3 flockCenter = flock.flockCenter;
    //     Vector3 flockVelocity = flock.flockVelocity;
    //     Vector3 follow = chasee.transform.localPosition;
    //     Debug.Log(follow + " " + flockCenter + " " + flockVelocity);

    //     return (flockCenter + flockVelocity + follow * 2 + randomize * randomness);
    //     // Vector3 flockCenter = 
    // }

    public void SetFlock(Flock flock)
    {
        this.flock = flock;

        minVelocity = flock.minVelocity;
        maxVelocity = flock.maxVelocity;

        randomness = flock.randomness;
        chasee = flock.chasee;


        inited = true;
    }
}
