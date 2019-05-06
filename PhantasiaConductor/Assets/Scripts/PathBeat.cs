using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;
using System.IO;

public class PathBeat : MonoBehaviour
{
    public LineRenderer lineRenderer;

    // time spent at each part of the line
    public List<float> timeMap = new List<float>();

    public GameObject obj;

    float timeElapsed;
    int index;

    void Start()
    {
        obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.transform.parent = transform;
        LoadFromFile("Assets/Paths/path.txt");
    }

    void Update()
    {
        if (index < vertexCount - 1)
        {
            var v = Vector3.Lerp(lineRenderer.GetPosition(index),
                                 lineRenderer.GetPosition(index + 1),
                                 timeElapsed / timeMap[index]);
            obj.transform.position = v;

            if (timeElapsed > timeMap[index])
            {
                timeElapsed -= timeMap[index];
                index++;
            }
            timeElapsed += Time.deltaTime;
        }
    }

    public void AddVertex(Vector3 pos, float t)
    {
        // no line is drawn if only one vertex exists
        if (vertexCount > 0)
        {
            timeMap.Add(t);
        }
        vertexCount += 1;
        lineRenderer.SetPosition(vertexCount - 1, pos);
    }

    public void LoadFromFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string line;
        while (!reader.EndOfStream)
        {
            line = reader.ReadLine();

            var tokens = line.Split(',');
            var v = new Vector3(float.Parse(tokens[0]), float.Parse(tokens[1]), float.Parse(tokens[2]));
            AddVertex(v, 1f);
        }
    }


    public Vector3[] positions
    {
        get
        {
            int count = lineRenderer.positionCount;
            Vector3[] p = new Vector3[count];

            lineRenderer.GetPositions(p);
            return p;
        }
    }

    private int vertexCount
    {
        get
        {
            return lineRenderer.positionCount;
        }

        set
        {
            lineRenderer.positionCount = value;
        }
    }
}
