
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Event to transition between object active/inactive
public class ChangeActive : TriggerEvent {

    public GameObject Sphere1;
    public GameObject Sphere2;
    private bool _state;

    public override void Activate()
    {
        _state = !_state;
        Sphere1.SetActive(_state);
        Sphere2.SetActive(!_state);
    }

    public override void Activate(bool state)
    {
        _state = state;
        Sphere1.SetActive(_state);
        Sphere2.SetActive(!_state);
    }

    public override void Activate(int state)
    {
        Activate(state != 0);
    }

    // Use this for initialization
    void Start () {
        Activate(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}