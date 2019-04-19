using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScheduledSpawn : MonoBehaviour
{
    // time after activation to start
    public float creationTime;
    // Start is called before the first frame update
    public float elapsedTime;

    bool hasShown;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (!hasShown && elapsedTime >= creationTime) {
            PObject pObj = gameObject.GetComponent<PObject>();
            pObj.Alive();
        }
    }

    void OnEnable() {
        Debug.Log("scheduled spawn enabled");
        
    }


}
