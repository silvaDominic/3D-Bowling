using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {
    public float pinCheckDuration = 3;
    public float distToRaisePins = 80;
    public Text standingDisplay;
    public GameObject pinSet;

    private bool ballLeftBox = false;
    private GameObject pinSetParent;
    private int lastSettledCount = 10;
    private int previousStandingCount = -1;
    private float lastChangeTime = 0;
    private BowlingBall bowlingBall;
    private Animator anim;
    private ActionMaster actionMaster;

    // Use this for initialization
    void Start() {
        pinSetParent = GameObject.Find("Pins");
        bowlingBall = GameObject.FindObjectOfType<BowlingBall>();
        anim = gameObject.GetComponent<Animator>();
        actionMaster = new ActionMaster();
    }

    // Update is called once per frame
    void Update() {
        if (ballLeftBox) {
            standingDisplay.color = Color.red;
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
            ballLeftBox = false;
        }
    }

    private void PinsHaveSettled() {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        ActionMaster.Action action = actionMaster.Bowl(pinFall);
        Debug.Log("Pin Fall: " + pinFall + ", Action: " + action);

        if (action == ActionMaster.Action.Tidy) {
            anim.SetTrigger("triggerCleanUp");
        } else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Not sure how to handle END GAME scenario");
        } else {
            anim.SetTrigger("triggerReset");
            lastSettledCount = 10;
        }

        bowlingBall.Reset();
        previousStandingCount = -1;
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

    public void SetBallLeftBox(bool state) {
        this.ballLeftBox = state;
    }
}
