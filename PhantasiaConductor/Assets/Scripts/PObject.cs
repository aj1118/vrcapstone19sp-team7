using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PObject : MonoBehaviour
{

    // public GameObject shape;
    // Start is called before the first frame update
    private bool alive;
    void Start()
    {
        // shape = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        // shape.transform.SetParent(gameObject.transform, false);

        // Hittable hit = shape.AddComponent<Hittable>();
        // hit.hitsBeforeBroadcast = 3;
        
        // Material mat = shape.GetComponent<Renderer>().material;
        // mat.shader.
        gameObject.layer = 1 << 2;

        // PObjects start off dead
        Dead();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginLevel() {
    }

    public void EndLevel() {

    }

    public void Alive() {
        GetComponent<Renderer>().enabled = true;
        // shape.GetComponent<Renderer>().enabled = true;
        alive = true;

        SendMessage("OnAlive", SendMessageOptions.DontRequireReceiver);
    }

    public void Dead() {
        // shape.GetComponent<Renderer>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        alive = false;

        SendMessage("OnDead", SendMessageOptions.DontRequireReceiver);
    }

    public bool IsAlive() {
        return alive;
    }

    public void OnSceneActive() {
        Debug.Log("broadcast received");
    }


}
