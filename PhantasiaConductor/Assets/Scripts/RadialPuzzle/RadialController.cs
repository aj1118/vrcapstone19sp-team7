using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialController : MonoBehaviour
{

    public float radius = 10f;

    public float heightOffset = 0f;

    public GameObject objPrefab;

    private GameObject obj;

    private bool mouseMode = true;

    // Start is called before the first frame update
    void Start()
    {
        // obj = Instantiate(objPrefab);
        obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.parent = transform;
    }

    void Update()
    {
        if (mouseMode)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            ray.direction = new Vector3(ray.direction.x, 0, ray.direction.z).normalized;
            ray.origin = transform.position;
            Vector3 pos = ray.origin + (ray.direction * radius);

            obj.transform.position = new Vector3(pos.x, transform.position.y + heightOffset, pos.z);
        }

    }
}
