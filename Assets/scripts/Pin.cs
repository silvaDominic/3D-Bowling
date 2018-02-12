using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float fallenPinThreshhold;

	// Use this for initialization
	void Start () {

	}

    private void Update() {
    }

    public bool IsStanding() {

        // Define rotation in Euler Angles and make values all positive
        float zRotation = Mathf.Abs(transform.rotation.eulerAngles.z);
        float xRotation = Mathf.Abs(transform.rotation.eulerAngles.x);

        // Normailize rotation
        zRotation = zRotation > 180 ? 360 - zRotation : zRotation;
        xRotation = xRotation > 180 ? 360 - xRotation : xRotation;

        if (zRotation > fallenPinThreshhold || xRotation > fallenPinThreshhold) {
            return false;
        }
        return true;
    }


}
