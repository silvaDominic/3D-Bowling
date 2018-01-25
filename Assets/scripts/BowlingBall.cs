using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BowlingBall : MonoBehaviour {

    private Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
	}

    public void LaunchBall(Vector3 launchSpeed, float rotation) {
        LaunchBall(launchSpeed);
        rigidbody.rotation = new Quaternion(rotation, 0, 0, 0);
    }

    public void LaunchBall(Vector3 launchSpeed) {
        rigidbody.useGravity = true;
        rigidbody.velocity = launchSpeed;
    }

    public void NudgeRight() {
        rigidbody.position += new Vector3(2, rigidbody.position.y, rigidbody.position.z);
    }

    public void NudgeLeft() {
        rigidbody.position -= new Vector3(2, rigidbody.position.y, rigidbody.position.z);
    }

    // Update is called once per frame
    void Update () {
	
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Floor>()) {
            GetComponent<AudioSource>().Play();
        }
    }
}
