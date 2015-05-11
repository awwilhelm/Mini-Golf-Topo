using UnityEngine;
using System.Collections;

public class ControlBall : MonoBehaviour {

	private float forceBase = 2;
	private float distance;
	private BallForce bfScript;
	public Texture arrow;
	bool dirSet;
	bool forceSet;
	private Vector3 v3_transform;
	public Canvas canvasArrow;
	private float maxDistance = 20;
	private float minDistance = 3;
	private float arrowScaleCoefficient = 0.048f;
	public ScoreKeeping scoreKeep;

	private const int FORCE = 9000;

	// Use this for initialization
	void Start () {
		bfScript = GetComponent<BallForce> ();
		scoreKeep = GameObject.Find ("World").GetComponent<ScoreKeeping> ();
		dirSet = false;
		forceSet = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			dirSet = true;
		if (!dirSet) {
			v3_transform = Input.mousePosition;
			v3_transform.z = Mathf.Abs (Camera.main.transform.position.y - transform.position.y);
			v3_transform = Camera.main.ScreenToWorldPoint (v3_transform);
			distance = Vector3.Distance(v3_transform, transform.position);
			v3_transform -= transform.position;
			v3_transform = v3_transform * 10000.0f + transform.position;
			transform.LookAt (-v3_transform);
			transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
			UpdateArrow(v3_transform);


		}
		else
		{
			if(!forceSet && !scoreKeep.getWin() && bfScript.getStop())
			{
				forceSet = true;
				if(distance>maxDistance)
					distance = maxDistance;
				else if(distance<minDistance)
					distance = minDistance;
				// print (distance);
				//if(distance<10)
				bfScript.addForce(distance * FORCE); //7500
				//else
					//bfScript.addForce(500 + Mathf.Pow(forceBase, distance)/10 );//+500 /33

			}
		}

	}
	public void addForceAgain()
	{
		dirSet = false;
		forceSet = false;
	}

	private void UpdateArrow(Vector3 v3T)
	{
		canvasArrow.transform.position = new Vector3(transform.position.x, 99, transform.position.z);
		canvasArrow.transform.rotation = Quaternion.LookRotation(new Vector3(v3T.x, v3T.y, v3T.z));
		canvasArrow.transform.rotation = Quaternion.Euler(new Vector3(270, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z-90));
		//canvasArrow.transform.position = new Vector3(canvasArrow.transform.position.x, canvasArrow.transform.position.y +300, canvasArrow.transform.position.z);
		if(distance<=maxDistance && distance>=minDistance)
			canvasArrow.transform.localScale = new Vector3(arrowScaleCoefficient*distance, arrowScaleCoefficient*distance, arrowScaleCoefficient*distance);
		else if(distance>maxDistance)
			canvasArrow.transform.localScale = new Vector3(arrowScaleCoefficient*maxDistance, arrowScaleCoefficient*maxDistance, arrowScaleCoefficient*maxDistance);
		else if(distance<minDistance)
			canvasArrow.transform.localScale = new Vector3(arrowScaleCoefficient*minDistance, arrowScaleCoefficient*minDistance, arrowScaleCoefficient*minDistance);

		
		if(!bfScript.getStop() || scoreKeep.getWin())
		{
			canvasArrow.enabled = false;
		} else {
			canvasArrow.enabled = true;
		}

	}

	void OnGUI()
	{
		Vector3 ballPos = Camera.main.WorldToScreenPoint(transform.position);
		if (bfScript.getStop ()) {
			//GUI.DrawTexture (new Rect (ballPos.x - 100, Screen.height - 20 - ballPos.y, 100, 40), arrow);
		}
		//GUI.Label (new Rect (10, 10, 200, 20), "X: "+Input.mousePosition.x + "Y: " + Input.mousePosition.y + "Z: " + Input.mousePosition.z);
		
		//GUI.Label (new Rect (10, 30, 400, 20), "X: "+ballPos.x + "  Y: " + ballPos.y + "  Z: " + ballPos.z);
		
		//GUI.Label (new Rect (10, 50, 300, 20), " Distance: "+distance);
	}

}
