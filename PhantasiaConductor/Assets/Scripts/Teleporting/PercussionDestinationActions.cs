using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercussionDestinationActions : MonoBehaviour
{
    public Vector3 puzzleCompleteOffset;

    public void Arrive()
    {
        GameObject ground = transform.Find("Ground").gameObject;
        Color orig = ground.GetComponent<Renderer>().material.color;
        float a = orig.a;
        Color newCOlor = new Color(orig.r, orig.g, orig.b, 0.0f);
        ground.GetComponent<Renderer>().material.color = newCOlor;

        gameObject.SetActive(true);

        // GetComponent<Fade>().FadeIn(ground, a);
    }

    public void Depart()
    {
        GameObject ground = transform.Find("Ground").gameObject;
        // GetComponent<Fade>().FadeOut(ground, 0.0f);

        StartCoroutine(MoveDelay(GetComponent<Fade>().fadeTime));
    }

    private IEnumerator MoveDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 newPos = transform.position;
        newPos += puzzleCompleteOffset;
        transform.position = newPos;
    }
}
