using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

    Rigidbody rb;
    Vector3 sumOfForces;
    public static Vector3 counterForce;
    public static Vector3 pos;
    // Use this for initialization
	void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
        pos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        pos = gameObject.transform.position;
        pos.y = 0;
        sumOfForces = (rb.velocity/Time.deltaTime) * rb.mass;
        counterForce = -sumOfForces;
        counterForce.y = Mathf.Abs(counterForce.y);
	}
}
