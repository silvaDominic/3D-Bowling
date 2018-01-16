using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BowlingBall : MonoBehaviour {

    private Rigidbody rigidbody;
    public Vector3 launchVelocity;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();

        LaunchBall();
	}

    public void LaunchBall() {
        rigidbody.velocity = launchVelocity;
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
