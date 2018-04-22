using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float fallenPinThreshhold;
    public float distToRaisePin = 80;

    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = gameObject.GetComponent<Rigidbody>();
	}

    private void Update() {
    }

    public bool IsStanding() {

        //// Define rotation in Euler Angles and make values all positive
        //float yRotation = Mathf.Abs(transform.rotation.eulerAngles.y);
        //float xRotation = Mathf.Abs(transform.rotation.eulerAngles.x) + 90;
        //Debug.Log("Pin: " + name);
        //Debug.Log(yRotation);
        //Debug.Log(xRotation);

        //// Normailize rotation
        //yRotation = yRotation > 180 ? 360 - yRotation : yRotation;
        //xRotation = xRotation > 180 ? 360 - xRotation : xRotation;

        //if (yRotation > fallenPinThreshhold || xRotation > fallenPinThreshhold) {
        //    return false;
        //}
        //return true;


        Vector3 rotationInEuler = transform.rotation.eulerAngles;

        float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
        float tiltInZ = Mathf.Abs(rotationInEuler.z);
        Debug.Log("Pin: " + name);

        if (tiltInX < fallenPinThreshhold && tiltInZ < fallenPinThreshhold) {
            Debug.Log("True");
            return true;
        } else {
            Debug.Log("False");
            return false;
        }
    }

    public void Raise() {
        if (IsStanding()) {
            rigidbody.useGravity = false;
            transform.Translate(new Vector3(0, distToRaisePin, 0), Space.World);
            transform.rotation = Quaternion.Euler(270f, 0, 0);
        }
    }

    public void Lower() {
        transform.Translate(new Vector3(0, -distToRaisePin, 0), Space.World);
        GetComponent<Rigidbody>().useGravity = true;
    }


}
