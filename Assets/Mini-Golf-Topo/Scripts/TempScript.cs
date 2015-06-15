using UnityEngine;
using System.Collections;

public class TempScript : MonoBehaviour {

	// Use this for initialization
	private Renderer myRend;
	// Use this for initialization
	void Start () {
		myRend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		//print(Renderer.material.GetFloat("_temp"));
	}
}
