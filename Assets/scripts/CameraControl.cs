using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public BowlingBall bowlingball;
    private Vector3 offset;

    // Use this for initialization
    void Start () {
        bowlingball = GameObject.FindObjectOfType<BowlingBall>();

        offset = gameObject.transform.position - bowlingball.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        // If ball is in front of head pin
        if (bowlingball.transform.position.z <= 1829f) {
            gameObject.transform.position = bowlingball.transform.position + offset;
        }
	}
}
