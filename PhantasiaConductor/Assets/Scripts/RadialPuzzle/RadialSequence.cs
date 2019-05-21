using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialSequence : MonoBehaviour
{

    public BeatInfo[] beatInfos;

    public float[] spawnDegrees;

    public MasterLoop masterLoop;

    public GameObject radialObjectPrefab;

    public float radius = 10f;
    public float spawnHeight = 5f;

    private int beatInfoIndex = 0;
    private int beatIndex = 0;
    private int rIndex = 0;

    private float timePerBeat;

    private float degOffset = 90;

    // Start is called before the first frame update
    void Start()
    {
        timePerBeat = loopTime / beatInfos[0].beats.Length;
    }

    void NextBeat()
    {
        BeatInfo currBeatInfo = beatInfos[beatInfoIndex];
        bool shouldSpawn = currBeatInfo.beats[beatIndex];

        if (shouldSpawn)
        {
            GameObject obj = Instantiate(radialObjectPrefab);
            obj.transform.parent = transform.parent;

            float deg = spawnDegrees[rIndex] + degOffset;
            float x = Mathf.Cos(deg * Mathf.Deg2Rad) * radius;
            float z = Mathf.Sin(deg * Mathf.Deg2Rad) * radius;
            obj.transform.localPosition = new Vector3(x, spawnHeight, z);

            rIndex++;
        }

        beatIndex++;
        if (beatIndex >= currBeatInfo.beats.Length)
        {
            beatIndex = 0;
            beatInfoIndex++;
        }

        if (beatInfoIndex < beatInfos.Length)
        {
            Invoke("NextBeat", timePerBeat);
        }
    }

    public void NewLoop()
    {
        if (beatInfoIndex >= beatInfos.Length)
        {
            rIndex = 0;
            beatIndex = 0;
            beatInfoIndex = 0;

            CancelInvoke();
            Invoke("NextBeat", timePerBeat);
        }
    }

    void OnEnable()
    {
        // allows new loop to start the beat sequence
        beatInfoIndex = beatInfos.Length;
    }

    float loopTime
    {
        get
        {
            return MasterLoop.loopTime;
        }
    }
}
