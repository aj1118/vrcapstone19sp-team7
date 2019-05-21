using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;

public class RadialObject : MonoBehaviour
{
    public UnityEvent onSuccess;

    public UnityEvent onFailed;
     

    public float lifetime = 4f;

    private float acc = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        acc += Time.deltaTime;
        if (acc >= lifetime)
        {
            onFailed.Invoke();
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        onSuccess.Invoke();
        Destroy(gameObject);
    }

    void Fadeout()
    {

    }
}
