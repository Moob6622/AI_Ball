using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsAdjust : MonoBehaviour {

    public float mult;
    Rigidbody rb;
	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        rb.AddForce( Physics.counterForce * mult);
        
	}
}
