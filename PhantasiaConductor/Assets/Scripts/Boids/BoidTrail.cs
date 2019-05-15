using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class BoidTrail : MonoBehaviour
{

    private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<TrailRenderer>();

        int numColorKeys = Random.Range(2, 8);
        // Color[] colors = GenerateColors(numColorKeys);
        Color[] colors = GenerateColorsHsv(numColorKeys);
        Debug.Log(colors);
        // int numAlphaKeys = Random.Range(2, 5);
        GradientAlphaKey[] alphaKeys = { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) };
        GradientColorKey[] colorKeys = new GradientColorKey[numColorKeys];
        float delta = 1.0f / numColorKeys;
        for (var i = 0; i < numColorKeys; i++)
        {
            colorKeys[i] = new GradientColorKey(colors[i], delta * (i + 1));
        }
        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        // Debug.Log("Starting" + tr.startColor);
        tr.colorGradient = gradient;
        // Debug.Log("Ending" + tr.startColor);

        float trailTime = Random.Range(.15f, .6f);
        tr.time = trailTime;
    }

    Color[] GenerateColorsHsv(int n)
    {
        Color[] colors = new Color[n];
        for (var i = 0; i < n; i++)
        {
            float hue = Random.Range(0f, 1f);
            Debug.Log(hue);

            float saturation = Random.Range(.6f, 1f);
            float value = Random.Range(.5f, .9f);

            colors[i] = Color.HSVToRGB(hue, saturation, value);
        }

        return colors;
    }

    Color[] GenerateColors(int n)
    {
        Color[] colors = new Color[n];
        for (var i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }

        // generate component by component
        for (var comp = 0; comp < 3; comp++)
        {
            // use same time value for components
            float seed = Time.realtimeSinceStartup * ((comp + 1) * 100);

            for (var i = 0; i < n; i++)
            {
                float v = Mathf.PerlinNoise(seed + i * 10, seed + i * 10);
                // tend towards brighter colors
                // v = (1 + v) / 2;
                colors[i][comp] = v;
            }
        }

        return colors;
    }

}
