using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlacer : MonoBehaviour
{

    public float numWalls = 8;

    public float radius = 50;

    public GameObject wallPrefab;
    public MasterLoop masterLoop;

    void Start()
    {
        float y = transform.localPosition.y;
        float deltaRad = 2 * Mathf.PI / numWalls;
        for (int i = 0; i < numWalls; i++)
        {
            GameObject wall = Instantiate(wallPrefab);
            wall.transform.parent = transform;

            float rad = deltaRad * i;
            float x = Mathf.Cos(rad) * radius;
            float z = Mathf.Sin(rad) * radius;

            // wall.transform.LookAt(transform);

            wall.transform.localPosition = new Vector3(x, y, z);
            wall.transform.Rotate(0, -Mathf.Rad2Deg * (rad), 0);

            ColorPulse cp = wall.GetComponent<ColorPulse>();
            masterLoop.onNewLoop.AddListener(delegate ()
            {
                Debug.Log("new loop");
                cp.NewLoop();
            });
        }
    }


}
