using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// changes color with beats
[RequireComponent(typeof(BeatInfo), typeof(Renderer))]
public class ColorPulse : MonoBehaviour
{
    public float alpha = 1.0f;

    static float numBeats = 8;
    static float timeBetweenPulse = MasterLoop.loopTime / numBeats;

    void Start() {
        ChangeColor();
    }

    void BeatTick() {
        ChangeColor();
        Invoke("BeatTick", timeBetweenPulse);
    }

    void ChangeColor() {
        Color c = ColorGenerator.GenerateColor();
        c.a = alpha;
        Renderer renderer = GetComponent<Renderer>();
        
        renderer.material.color = c;
    }

    public void NewLoop() {
        CancelInvoke();
        ChangeColor();
        Invoke("BeatTick", timeBetweenPulse);
    }
}
