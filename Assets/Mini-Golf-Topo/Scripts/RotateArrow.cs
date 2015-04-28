using UnityEngine;
using System.Collections;

public class RotateArrow : MonoBehaviour {

	public GameObject ball;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 ballPos = Camera.main.WorldToScreenPoint(ball.transform.position);
		ballPos = new Vector3 (ballPos.x, Screen.height - ballPos.y, ballPos.z);
		transform.position = ballPos;
	}
}
