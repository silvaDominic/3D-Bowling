using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BowlingBall))]
public class SwipeLaunch : MonoBehaviour {

    private BowlingBall bowlingball;
    private Vector3 dragStartPos, dragEndPos;
    private float startTime, endTime;

    // Adjustable scale factor for fine tuning
    [Range(1, 3)]
    public float forceScaler = 1;

    // Use this for initialization
    void Start() {
        bowlingball = GetComponent<BowlingBall>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void Nudge(float amount) {
        if (!bowlingball.isInPlay) {
            transform.Translate(amount, 0, 0);
            Debug.Log("Ball X Position: " + transform.position.x);
        }
    }

    public void DragStart() {
        dragStartPos = Input.mousePosition;
        startTime = Time.time;
    }

    public void DragEnd() {
        endTime = Time.time;
        dragEndPos = Input.mousePosition;

        float dragTime = endTime - startTime;
        float dragDistance = Mathf.Abs(dragEndPos.y - dragStartPos.y);
        Debug.Log("Drag Time: " + dragTime);
        Debug.Log("Drag Distance: " + dragDistance);

        float launchSpeed = (dragDistance / dragTime) / forceScaler;
        Debug.Log("Speed: " + launchSpeed);

        // Will come back to this later
        //float rotation = Mathf.Abs(dragEndPos.x - dragStartPos.x);
        //float normalizedRotation = rotation / Mathf.Sqrt(Mathf.Pow(rotation, 2));
        //Debug.Log("Rotation: " + rotation);

        bowlingball.LaunchBall(new Vector3(0, 0, launchSpeed));
    }
}
