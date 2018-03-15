using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour {

    private PinSetter pinSetter;

	// Use this for initialization
	void Start () {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
	}

    private void OnTriggerExit(Collider collider) {

        if (collider.name == "Bowling-Ball") {
            pinSetter.SetBallLeftBox(true);
            Debug.Log("Ball left box");
        } else {
            Debug.Log("Something went wrong");
        }
    }
}
