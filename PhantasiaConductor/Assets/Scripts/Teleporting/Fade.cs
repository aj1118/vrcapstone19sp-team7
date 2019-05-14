using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    // publically editable speed
    public float fadeDelay = 0.0f;
    public float fadeTime = 2f;

    // fade sequence
    IEnumerator FadeSequence(GameObject obj, float targetAlpha, float fadingOutTime)
    {
        // log fading direction, then precalculate fading speed as a multiplier
        bool fadingOut = (fadingOutTime < 0.0f);
        float fadingOutSpeed = 1.0f / fadingOutTime;

        // grab all child objects
        Renderer renderer = obj.GetComponent<Renderer>();
        Color color = renderer.material.color;

        // make all objects visible
        renderer.enabled = true;


        // get current max alpha
        float alphaValue = color.a;

        // iterate to change alpha value 
        while ((alphaValue >= targetAlpha && fadingOut) || (alphaValue <= targetAlpha && !fadingOut))
        {
            alphaValue += Time.deltaTime * fadingOutSpeed;

            Color newColor = (color != null ? color : renderer.material.color);
            if (fadingOut)
            {
                newColor.a = Mathf.Min(newColor.a, alphaValue);
            } else
            {
                newColor.a = Mathf.Max(newColor.a, alphaValue);
            }
            newColor.a = Mathf.Clamp(newColor.a, 0.0f, 1.0f);
            renderer.material.SetColor("_Color", newColor);

            yield return null;
        }

        // turn objects off after fading out
        if (fadingOut)
        {
            renderer.enabled = false;
        }

        Debug.Log("fade sequence end : " + fadingOut);
    }


    public void FadeIn(GameObject obj, float a)
    {
        FadeIn(obj, a, fadeTime);
    }

    public void FadeOut(GameObject obj, float a)
    {
        FadeOut(obj, a, fadeTime);
    }

    public void FadeIn(GameObject obj, float a, float newFadeTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeSequence(obj, a, newFadeTime));
    }

    public void FadeOut(GameObject obj, float a, float newFadeTime)
    {
        StopAllCoroutines();
        StartCoroutine(FadeSequence(obj, a, -newFadeTime));
    }
}
