using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PianoDestinationActions : MonoBehaviour
{
    public GameObject[] targets;
    public Vector3 newPosition;
    public Quaternion newRotation;
    public Vector3 newScale;

    // public UnityEvent startPuzzle;

    public void Arrive()
    {
        foreach (GameObject target in targets)
        {
            Color original = target.GetComponent<Renderer>().material.color;
            Color newColor = new Color(original.r, original.g, original.b, 0.0f);
            target.GetComponent<Renderer>().material.color = newColor;
        }

        gameObject.SetActive(true);
        
        // GetComponent<FadeChildren>().FadeIn();

        // startPuzzle.Invoke();
    }

    public void Depart()
    {
        transform.localPosition = newPosition;
        transform.rotation = newRotation;
        transform.localScale = newScale;
    }
}
