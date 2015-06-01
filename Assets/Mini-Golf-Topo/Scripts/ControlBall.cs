﻿using UnityEngine;
using System.Collections;
using System.Threading;

public class ControlBall : MonoBehaviour {

	
	public Texture arrow;
	public Canvas canvasArrow;
	public GameObject camera3D;
	public GameObject scoreKeepingObject;
	private ScoreKeeping scoreKeepingScript;	
	private BallForce bfScript;
	private CameraFollow cameraFollowScript;

	private float distance;
	private Vector3 v3_transform;

	private const int FORCE_MULTIPLIER = 35000;
	private const int DISTANCE_OFFSET = 5000;
	private const float MAX_DISTANCE = 20;
	private const float MIN_DISTANCE = 1;
	private const float MAX_ARROW_SCALE = 25;
	private const float MIN_ARROW_SCALE = 3;
	private const float ARROW_SCALE_COEFFICIENT = 0.048f;

	// Use this for initialization
	void Start () {
		bfScript = GetComponent<BallForce> ();
		cameraFollowScript = camera3D.GetComponent<CameraFollow>();
		scoreKeepingScript = scoreKeepingObject.GetComponent<ScoreKeeping>();
		distance = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//If player left-clicks add force
		if (Input.GetMouseButtonDown (0))
		{

			if(!scoreKeepingScript.getWin() && !bfScript.getBallMoving())
			{
				scoreKeepingScript.addToHits();

				//Scales arrow scale percent to force
				float arrowScalePercent, force;
				arrowScalePercent = (distance+MIN_ARROW_SCALE)/(MIN_ARROW_SCALE+MAX_ARROW_SCALE);
				force = arrowScalePercent*(MIN_DISTANCE+MAX_DISTANCE) - MIN_DISTANCE;

				bfScript.addHitForce((force * FORCE_MULTIPLIER) - DISTANCE_OFFSET);
			}
		}
		//If the player does not left click then this updates the dirction the ball faces and the arrow.
		else {
			if(!scoreKeepingScript.getWin() && !bfScript.getBallMoving() && !cameraFollowScript.getFullScreenAnimation() && !cameraFollowScript.getFullScreen())
			{
				v3_transform = UpdateFacingDirection();

				transform.LookAt (-v3_transform);
				transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));

				UpdateArrow(v3_transform);
				canvasArrow.enabled = true;
			}
			else
			{
				canvasArrow.enabled = false;
			}
		}
	}

	//Makes a variable that converts the mouse position to a 2-D position on the screen.
	private Vector3 UpdateFacingDirection()
	{
		Vector3 temp_transform;
		temp_transform = Input.mousePosition;
		temp_transform.z = Mathf.Abs (Camera.main.transform.position.y - transform.position.y);
		temp_transform = Camera.main.ScreenToWorldPoint (temp_transform);

		//Finds distance before makes transform negative and adding constants.
		distance = Vector3.Distance(temp_transform, transform.position);

		temp_transform -= transform.position;

		//Without this the arrow moves based on the percentage of the screen the mouse is on.
		temp_transform = temp_transform * 10000.0f + transform.position;
		

		return temp_transform;
	}

	//Sets the arrows position and rotation.  Also adjusts the scale of the arrow.
	private void UpdateArrow(Vector3 v3T)
	{
		canvasArrow.transform.position = new Vector3(transform.position.x, 100, transform.position.z);
		canvasArrow.transform.rotation = Quaternion.LookRotation(new Vector3(v3T.x, v3T.y, v3T.z));
		canvasArrow.transform.rotation = Quaternion.Euler(new Vector3(270, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z-90));

		if(distance<=MAX_ARROW_SCALE && distance>=MIN_ARROW_SCALE)
		{
			canvasArrow.transform.localScale = new Vector3(ARROW_SCALE_COEFFICIENT*distance, ARROW_SCALE_COEFFICIENT*distance, ARROW_SCALE_COEFFICIENT*distance);
		}
		else if(distance>MAX_ARROW_SCALE)
		{
			canvasArrow.transform.localScale = new Vector3(ARROW_SCALE_COEFFICIENT*MAX_ARROW_SCALE, ARROW_SCALE_COEFFICIENT*MAX_ARROW_SCALE, ARROW_SCALE_COEFFICIENT*MAX_ARROW_SCALE);
			distance = MAX_ARROW_SCALE;
		}
		else if(distance<MIN_ARROW_SCALE)
		{
			canvasArrow.transform.localScale = new Vector3(ARROW_SCALE_COEFFICIENT*MIN_ARROW_SCALE, ARROW_SCALE_COEFFICIENT*MIN_ARROW_SCALE, ARROW_SCALE_COEFFICIENT*MIN_ARROW_SCALE);
			distance = MIN_ARROW_SCALE;
		}
	}

}
