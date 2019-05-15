using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class BoidTrail : MonoBehaviour
{
    [SerializeField]
    private float widthScale = 1.0f;
    private TrailRenderer tr;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<TrailRenderer>();

        int numColorKeys = Random.Range(2, 8);
        // Color[] colors = GenerateColors(numColorKeys);
        Color[] colors = GenerateColorsHsv(numColorKeys);

        GradientAlphaKey[] alphaKeys = { new GradientAlphaKey(Random.Range(.4f, 1f), 0.0f), new GradientAlphaKey(0.0f, 1.0f) };
        GradientColorKey[] colorKeys = new GradientColorKey[numColorKeys];
        {
            float delta = 1.0f / numColorKeys;
            for (var i = 0; i < numColorKeys; i++)
            {
                colorKeys[i] = new GradientColorKey(colors[i], delta * (i + 1));
            }
        }

        Gradient gradient = new Gradient();
        gradient.SetKeys(colorKeys, alphaKeys);

        tr.colorGradient = gradient;

        float trailTime = Random.Range(.15f, .6f);
        tr.time = trailTime;

        // handle the width curve
        AnimationCurve curve = new AnimationCurve();
        int numCurveKeys = Random.Range(2, 8);
        float[] widths = new float[numCurveKeys];

        float seed = Random.Range(0f, 1000f);
        for (var i = 0; i < numCurveKeys; i++)
        {
            widths[i] = Mathf.PerlinNoise(seed + (0.5f * i), seed + (0.25f *i));
        }

        {
            float delta = 1.0f / numCurveKeys;
            for (var i = 0; i < numCurveKeys; i++)
            {
                curve.AddKey((1 + i) * delta, widths[i]);
            }
        }

        tr.widthCurve = curve;
        tr.widthMultiplier = widthScale;

        // TODO maybe average the trail color with the mesh color for more visual coherence
    }

    Color[] GenerateColorsHsv(int n)
    {
        Color[] colors = new Color[n];
        float seed = Random.Range(0f, 1000f);
        for (var i = 0; i < n; i++)
        {
            // float hue = Random.Range(0f, 1f);
            float hue = Mathf.PerlinNoise(seed + i, seed + i);

            float saturation = Random.Range(.6f, 1f);
            float value = Random.Range(.5f, 1f);
            // float value = Random.Range(0f, 1f);

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
            float seed = Random.Range(0f, 1000f);

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
