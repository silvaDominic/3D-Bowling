using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PinSetter : MonoBehaviour {
    public float distToRaisePins = 80;
    public GameObject pinSet;

    private GameObject pinSetParent;
    private ActionMaster actionMaster;
    private PinCounter pinCounter;
    private Animator anim;

    // Use this for initialization
    void Start() {
        pinSetParent = GameObject.Find("Pins");
        pinCounter = GameObject.FindObjectOfType<PinCounter>();
        anim = gameObject.GetComponent<Animator>();
        actionMaster = new ActionMaster();
    }

    public void RenewPins() {
        GameObject newPinSet = Instantiate(pinSet, new Vector3(0, 100, 1829), Quaternion.identity) as GameObject;
        newPinSet.transform.parent = pinSetParent.transform.parent;
    }



    public void RaisePins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            if (pin.IsStanding()) {
                pin.Raise();
            }
        }
    }

    public void LowerPins() {
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>()) {
            pin.Lower();
        }
    }

    public void PerformAction(ActionMaster.Action action) {
        if (action == ActionMaster.Action.Tidy) {
            anim.SetTrigger("triggerCleanUp");
        } else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Not sure how to handle END GAME scenario");
        } else {
            anim.SetTrigger("triggerReset");
            pinCounter.Reset();
        }
    }
}
