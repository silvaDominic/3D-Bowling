using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    public float pinCheckDuration = 3;
    public float distToRaisePins = 80;
    public Text standingDisplay;
    public GameObject pinSet;

    private GameObject pinSetParent;
    private bool ballEnteredBox = false;
    private int previousStandingCount = -1;
    private float lastChangeTime = 0;
    private BowlingBall bowlingBall;

    // Use this for initialization
    void Start() {
        pinSetParent = GameObject.Find("Pins");
        bowlingBall = GameObject.FindObjectOfType<BowlingBall>();
    }

    // Update is called once per frame
    void Update() {
        if (ballEnteredBox) {
            CheckStandingAndUpdate();
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

    public void RenewPins() {
        GameObject newPinSet = Instantiate(pinSet, new Vector3(0, 100, 1829), Quaternion.identity) as GameObject;
        newPinSet.transform.parent = pinSetParent.transform.parent;
    }

    private void OnTriggerEnter(Collider obj) {
        if (obj.GetComponent<BowlingBall>()) {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

    private void CheckStandingAndUpdate() {
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
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.Raise();
        }
    }

    public void LowerPins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.Lower();
        }
    }
}
