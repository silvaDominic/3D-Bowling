using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    private float width;
    private float length;

	// Use this for initialization
	void Start () {
        width = transform.localScale.x;
        length = transform.localScale.z;
    }
	
    public float GetWidth() {
        return width;
    }

    public float GetLength() {
        return length;
    }
}
