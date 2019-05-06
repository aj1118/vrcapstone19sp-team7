using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Assertions;
using System.IO;

public class PathBeat : MonoBehaviour
{
    public enum PathMode
    {
        SPEED_CONSTANT,
        TIMED
    }

    public LineRenderer lineRenderer;

    // time spent at each part of the line
    public List<float> timeMap = new List<float>();

    public GameObject obj;

    public PathMode pathMode = PathMode.SPEED_CONSTANT;

    // units of movement per sec
    public float speed = 5;

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
            Vector3 v;
            float t = timeMap[index];
            
            switch (pathMode)
            {
                case PathMode.TIMED:
                    t = timeMap[index];
                    break;
                case PathMode.SPEED_CONSTANT:
                    t = CalcTimeTraverse(lineRenderer.GetPosition(index), lineRenderer.GetPosition(index + 1));
                    break;

            }

            float completion = timeElapsed / t;

            v = Vector3.Lerp(lineRenderer.GetPosition(index),
                             lineRenderer.GetPosition(index + 1),
                             completion);

            obj.transform.position = v;

            if (timeElapsed > t)
            {
                timeElapsed -= t;
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

    private float CalcTimeTraverse(Vector3 start, Vector3 end)
    {
        var dist = Vector3.Distance(start, end);
        return dist / speed;
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
