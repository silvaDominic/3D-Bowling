using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    public float pinCheckDuration = 3;
    public Text standingDisplay;

    private GameObject pins;
    private bool ballEnteredBox = false;
    private int previousStandingCount = -1;
    private float lastChangeTime = 0;
    private BowlingBall bowlingBall;

    // Use this for initialization
    void Start() {
        pins = GameObject.Find("Pins");
        bowlingBall = GameObject.FindObjectOfType<BowlingBall>();
    }

    // Update is called once per frame
    void Update() {
        if (ballEnteredBox) {
            CheckStandingPins();
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
            standingDisplay.color = Color.red;
        }
    }

    private void CheckStandingPins() {
        int currentStanding = CountStanding();

        if (currentStanding != previousStandingCount) {
            Debug.Log("Pins still moving");
            lastChangeTime = Time.time;
            previousStandingCount = currentStanding;
            return;
        }

        Debug.Log("Pins settling...");

        if (Time.time > (lastChangeTime + pinCheckDuration)) {
            Debug.Log("Pins settled.");
            PinsHaveSettled();
            previousStandingCount = -1;
            ballEnteredBox = false;
        }
    }

    private void PinsHaveSettled() {
        bowlingBall.Reset();
        standingDisplay.color = Color.green;
        standingDisplay.text = CountStanding().ToString();
    }

    public void RaisePins() {
        pins.transform.Translate(0, 90, 0);
    }

    public void LowerPins() {
        pins.transform.Translate(0, 0, 0);
    }



}
