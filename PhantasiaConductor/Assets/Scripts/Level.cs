using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public PObject pObjectPrefab;
    // Start is called before the first frame update

    private List<PObject> allPObjects = new List<PObject>();

    void Awake() {
        gameObject.SetActive(false);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeginLevel() {
        foreach (PObject obj in allPObjects) {
            obj.BeginLevel();
        }
    }

    public void EndLevel() {
        foreach (PObject obj in allPObjects) {
            obj.EndLevel();
        }
    }

    public PObject AddObject() {
        // Instantiat
        PObject pObj = Instantiate(pObjectPrefab, transform.position, Quaternion.identity);
        pObj.transform.parent = this.transform;
        
        allPObjects.Add(pObj);
        return pObj;
    }

}
