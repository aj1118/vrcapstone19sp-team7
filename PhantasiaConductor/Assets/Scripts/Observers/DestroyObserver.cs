using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObserver : PObserver
{
    public override void Run(string e) {
        Destroy(gameObject);
    }
}
