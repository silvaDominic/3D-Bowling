using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    public float pinCheckDuration = 3;
    public Text standingDisplay;

    private GameManager gameManager;
    private bool ballHasLeftBox = false;
    private int previousStandingCount = -1;
    private int lastSettledCount = 10;
    private float lastChangeTime = 0;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.FindObjectOfType<GameManager>();
	}

    // Update is called once per frame
    void Update() {
        if (ballHasLeftBox) {
            standingDisplay.color = Color.red;
            CheckStandingAndUpdate();
        }
    }

    private void OnTriggerExit(Collider collider) {

        if (collider.name == "Bowling-Ball") {
            ballHasLeftBox = true;
        } else {
            Debug.Log("Something went wrong");
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

    private void CheckStandingAndUpdate() {
        int currentStanding = CountStanding();

        if (currentStanding != previousStandingCount) {
            //Debug.Log("Pins still moving");
            lastChangeTime = Time.time;
            previousStandingCount = currentStanding;
            return;
        }

        Debug.Log("Pins settling...");

        if (Time.time > (lastChangeTime + pinCheckDuration)) {
            //Debug.Log("Pins settled.");
            PinsHaveSettled();
            previousStandingCount = -1;
            ballHasLeftBox = false;
        }
    }

    private void PinsHaveSettled() {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        gameManager.Bowl(pinFall);

        previousStandingCount = -1;
        standingDisplay.color = Color.green;
        standingDisplay.text = CountStanding().ToString();
    }

    public void Reset() {
        lastSettledCount = 10;
    }
}
