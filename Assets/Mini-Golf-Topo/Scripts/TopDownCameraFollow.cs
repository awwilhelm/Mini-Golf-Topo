using UnityEngine;
using System.Collections;

public class TopDownCameraFollow : MonoBehaviour {

	public GameObject ball;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.position = new Vector3(ball.transform.position.x , 200, ball.transform.position.z );
	}

}
