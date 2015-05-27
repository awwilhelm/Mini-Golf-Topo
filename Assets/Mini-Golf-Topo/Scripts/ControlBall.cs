using UnityEngine;
using System.Collections;
using System.Threading;

public class ControlBall : MonoBehaviour {

	
	public Texture arrow;
	public Canvas canvasArrow;
	public ScoreKeeping scoreKeep;
	private BallForce bfScript;

	private float distance;
	private Vector3 v3_transform;

	private const int FORCE = 15000;
	private const int DISTANCE_OFFSET = 20000;
	private const float MAX_DISTANCE = 20;
	private const float MIN_DISTANCE = 3;
	private const float ARROW_SCALE_COEFFICIENT = 0.048f;

	// Use this for initialization
	void Start () {
		bfScript = GetComponent<BallForce> ();
		distance = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//If player left-clicks add force
		if (Input.GetMouseButtonDown (0))
		{

			if(!scoreKeep.getWin() && !bfScript.getBallMoving())
			{
				if(distance>MAX_DISTANCE)
					distance = MAX_DISTANCE;
				else if(distance<MIN_DISTANCE)
					distance = MIN_DISTANCE;

				bfScript.addHitForce((distance * FORCE) - DISTANCE_OFFSET);
			}
		}
		//If the player does not left click then this updates the dirction the ball faces and the arrow.
		else {
			if(!scoreKeep.getWin() && !bfScript.getBallMoving())
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
		canvasArrow.transform.position = new Vector3(transform.position.x, 99, transform.position.z);
		canvasArrow.transform.rotation = Quaternion.LookRotation(new Vector3(v3T.x, v3T.y, v3T.z));
		canvasArrow.transform.rotation = Quaternion.Euler(new Vector3(270, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z-90));
		//canvasArrow.transform.position = new Vector3(canvasArrow.transform.position.x, canvasArrow.transform.position.y +300, canvasArrow.transform.position.z);

		if(distance<=MAX_DISTANCE && distance>=MIN_DISTANCE)
			canvasArrow.transform.localScale = new Vector3(ARROW_SCALE_COEFFICIENT*distance, ARROW_SCALE_COEFFICIENT*distance, ARROW_SCALE_COEFFICIENT*distance);
		else if(distance>MAX_DISTANCE)
			canvasArrow.transform.localScale = new Vector3(ARROW_SCALE_COEFFICIENT*MAX_DISTANCE, ARROW_SCALE_COEFFICIENT*MAX_DISTANCE, ARROW_SCALE_COEFFICIENT*MAX_DISTANCE);
		else if(distance<MIN_DISTANCE)
			canvasArrow.transform.localScale = new Vector3(ARROW_SCALE_COEFFICIENT*MIN_DISTANCE, ARROW_SCALE_COEFFICIENT*MIN_DISTANCE, ARROW_SCALE_COEFFICIENT*MIN_DISTANCE);
	}

}
