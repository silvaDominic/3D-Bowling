using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

    public Text standingDisplay;
    private bool ballEnteredBox = false;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (ballEnteredBox) {
            CheckSettledPins();
        }
    }

    public int CountStanding() {
        int pinCount = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                pinCount++; ;
            }
        }
        return pinCount;
    }

    private void OnTriggerEnter(Collider obj) {
        if (obj.GetComponent<BowlingBall>()) {
            ballEnteredBox = true;
            CheckSettledPins(Time.time, 4.0f);
            Debug.Log("Bowling ball entered");
            standingDisplay.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider obj) {
        Debug.Log(obj);
        if (obj.GetComponent<BowlingBall>()) {
            ballEnteredBox = false;
            Debug.Log("BowlingBall left");
        }
    }

    private int CheckSettledPins(float startTime, float checkDuration) {
        float endTime = startTime + checkDuration;

        return 10;
    }



}
