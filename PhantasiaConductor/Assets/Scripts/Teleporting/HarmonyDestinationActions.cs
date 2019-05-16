using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonyDestinationActions : MonoBehaviour
{
    public float delayDepart = 4f;
    public Vector3 newLocalPosition;
    public Vector3 newLocalScale;

    public void Arrive()
    {
        gameObject.SetActive(true);
    }

    public void Depart()
    {
        transform.position = new Vector3(10, 10, 10);
        StartCoroutine(DelayDepart());
    }

    private IEnumerator DelayDepart()
    {
        yield return new WaitForSeconds(delayDepart);
        transform.localPosition = newLocalPosition;
        transform.localScale = newLocalScale;
    }
}
