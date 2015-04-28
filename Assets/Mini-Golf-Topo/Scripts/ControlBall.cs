using UnityEngine;
using System.Collections;

public class ControlBall : MonoBehaviour {

	private float forceBase = 2;
	private float distance;
	private BallForce bfScript;
	public Texture arrow;
	bool dirSet = false;
	bool forceSet = false;
	private Vector3 v3T;
	public Canvas canvasArrow;
	private float maxDistance = 20;
	public ScoreKeeping scoreKeep;

	// Use this for initialization
	void Start () {
		bfScript = GetComponent<BallForce> ();
		scoreKeep = GameObject.Find ("World").GetComponent<ScoreKeeping> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			dirSet = true;
		if (!dirSet) {
			v3T = Input.mousePosition;
			v3T.z = Mathf.Abs (Camera.main.transform.position.y - transform.position.y);
			v3T = Camera.main.ScreenToWorldPoint (v3T);
			distance = Vector3.Distance(v3T, transform.position);
			v3T -= transform.position;
			v3T = v3T * 10000.0f + transform.position;
			transform.LookAt (-v3T);
			transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
			canvasArrow.transform.position = new Vector3(transform.position.x, 99, transform.position.z);
			canvasArrow.transform.rotation = Quaternion.LookRotation(new Vector3(v3T.x, v3T.y, v3T.z));
			canvasArrow.transform.rotation = Quaternion.Euler(new Vector3(270, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z-90));
			//canvasArrow.transform.position = new Vector3(canvasArrow.transform.position.x, canvasArrow.transform.position.y +300, canvasArrow.transform.position.z);
			if(distance<=maxDistance && distance>=3)
				canvasArrow.transform.localScale = new Vector3(0.1f*distance, 0.1f*distance, 0.1f*distance);

			if(!(bfScript.getStopX() && bfScript.getStopZ()) || scoreKeep.getWin())
			{
				canvasArrow.enabled = false;
			} else {
				canvasArrow.enabled = true;
			}

		}
		else
		{
			if(!forceSet && !scoreKeep.getWin())
			{
				if(distance>maxDistance)
					distance = maxDistance;
				else if(distance<3)
					distance = 3;
				// print (distance);
				if(distance<10)
					bfScript.addForce(distance * 500 );
				else
					bfScript.addForce(500 + Mathf.Pow(forceBase, distance)/33 );//+500 /33
				forceSet = true;
			}
		}

	}
	public void addForceAgain()
	{
		dirSet = false;
		forceSet = false;
	}

	void OnGUI()
	{
		Vector3 ballPos = Camera.main.WorldToScreenPoint(transform.position);
		if (bfScript.getStopX () == true && bfScript.getStopZ () == true) {
			//GUI.DrawTexture (new Rect (ballPos.x - 100, Screen.height - 20 - ballPos.y, 100, 40), arrow);
		}
		GUI.Label (new Rect (10, 10, 200, 20), "X: "+Input.mousePosition.x + "Y: " + Input.mousePosition.y + "Z: " + Input.mousePosition.z);
		
		GUI.Label (new Rect (10, 30, 400, 20), "X: "+ballPos.x + "  Y: " + ballPos.y + "  Z: " + ballPos.z);
		
		GUI.Label (new Rect (10, 50, 300, 20), " Distance: "+distance);
	}

}
