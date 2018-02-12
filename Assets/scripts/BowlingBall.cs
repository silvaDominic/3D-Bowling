using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BowlingBall : MonoBehaviour {

    public bool isInPlay = true;

    private Rigidbody rigidbody;
    private float leftFloorEdge;
    private float rightFloorEdge;
    private Floor floor;
    private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
        startingPosition = gameObject.transform.position;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;

        floor = GameObject.FindObjectOfType<Floor>();
        
        leftFloorEdge = -floor.GetWidth();
        rightFloorEdge = floor.GetWidth();
    }

    // Used for optional rotation mechanic
    public void LaunchBall(Vector3 launchSpeed, float rotation) {
        LaunchBall(launchSpeed);
        rigidbody.rotation = new Quaternion(rotation, 0, 0, 0);
    }

    public void LaunchBall(Vector3 launchSpeed) {
        isInPlay = true;
        rigidbody.useGravity = true;
        rigidbody.velocity = launchSpeed;
    }

    public void Reset() {
        Debug.Log("Resetting ball");
        isInPlay = false;
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.position = startingPosition;
    }

    // Update is called once per frame
    void Update () {
        Vector3 ballPosition = transform.position;
        ballPosition.x = Mathf.Clamp(transform.position.x, leftFloorEdge, rightFloorEdge);
        transform.position = ballPosition;
	}

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Floor>()) {
            GetComponent<AudioSource>().Play();
        }
    }
}
