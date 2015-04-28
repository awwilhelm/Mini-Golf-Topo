using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {
	Rigidbody rig;
	// Use this for initialization
	void Start () {
		rig = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
			rig.AddForce (transform.forward * 15000);
	}
}
