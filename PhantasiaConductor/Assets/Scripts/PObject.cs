using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PObject : MonoBehaviour
{

    public GameObject shape;
    // Start is called before the first frame update
    void Start()
    {
        shape = GameObject.CreatePrimitive(PrimitiveType.Cube);
        shape.transform.SetParent(gameObject.transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginLevel() {
        if (GetComponent<ScheduledSpawn>() != null) {

        }
    }

    public void EndLevel() {

    }

    public void Alive() {
        shape.GetComponent<Renderer>().enabled = true;
    }

    public void Dead() {
        shape.GetComponent<Renderer>().enabled = false;
    }

    public void ScheduleSpawn(float t) {
        if (GetComponent<ScheduledSpawn>() == null) {
            ScheduledSpawn comp = gameObject.AddComponent<ScheduledSpawn>();
            comp.creationTime = t;
        } else {
            GetComponent<ScheduledSpawn>().creationTime = t;
        }
    }
}
