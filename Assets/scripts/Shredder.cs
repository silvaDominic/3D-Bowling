using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider obj) {
        if (obj.GetComponentInParent<Pin>()) {
            Destroy(obj.GetComponentInParent<Pin>().gameObject);
        } else if (obj.GetComponentInParent<BowlingBall>()) {
            
        }
    }
}
