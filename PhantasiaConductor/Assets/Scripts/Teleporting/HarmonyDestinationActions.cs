using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonyDestinationActions : MonoBehaviour
{
    public Vector3 newLocalPosition;
    public Vector3 newLocalScale;

    public void Arrive()
    {
        gameObject.SetActive(true);
    }

    public void Depart()
    {
        transform.localPosition = newLocalPosition;
        transform.localScale = newLocalScale;
    }
}
