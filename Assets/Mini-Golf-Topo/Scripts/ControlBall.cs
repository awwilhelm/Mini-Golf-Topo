using UnityEngine;
using System.Collections;

public class ControlBall : MonoBehaviour {

	private float distance;
	private BallForce bfScript;
	public Texture arrow;
	private Vector3 v3_transform;
	public Canvas canvasArrow;
	private float maxDistance = 20;
	private float minDistance = 3;
	private float arrowScaleCoefficient = 0.048f;
	public ScoreKeeping scoreKeep;

	private const int FORCE = 15000;
	private const int DISTANCE_OFFSET = 20000;

	// Use this for initialization
	void Start () {
		bfScript = GetComponent<BallForce> ();
		scoreKeep = GameObject.Find ("World").GetComponent<ScoreKeeping> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//If player left-clicks add force
		if (Input.GetMouseButtonDown (0))
		{

			if(!scoreKeep.getWin() && bfScript.getStop())
			{
				if(distance>maxDistance)
					distance = maxDistance;
				else if(distance<minDistance)
					distance = minDistance;
				
				bfScript.addForce((distance * FORCE) - DISTANCE_OFFSET); //7500
			}
		}
		//If the player does not left click then this updates the dirction the ball faces and the arrow.
		else {
			if(!scoreKeep.getWin() && bfScript.getStop())
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

		if(distance<=maxDistance && distance>=minDistance)
			canvasArrow.transform.localScale = new Vector3(arrowScaleCoefficient*distance, arrowScaleCoefficient*distance, arrowScaleCoefficient*distance);
		else if(distance>maxDistance)
			canvasArrow.transform.localScale = new Vector3(arrowScaleCoefficient*maxDistance, arrowScaleCoefficient*maxDistance, arrowScaleCoefficient*maxDistance);
		else if(distance<minDistance)
			canvasArrow.transform.localScale = new Vector3(arrowScaleCoefficient*minDistance, arrowScaleCoefficient*minDistance, arrowScaleCoefficient*minDistance);

		


	}

	void OnGUI()
	{
		//Vector3 ballPos = Camera.main.WorldToScreenPoint(transform.position);
		if (bfScript.getStop ()) {
			//GUI.DrawTexture (new Rect (ballPos.x - 100, Screen.height - 20 - ballPos.y, 100, 40), arrow);
		}
		//GUI.Label (new Rect (10, 10, 200, 20), "X: "+Input.mousePosition.x + "Y: " + Input.mousePosition.y + "Z: " + Input.mousePosition.z);
		
		//GUI.Label (new Rect (10, 30, 400, 20), "X: "+ballPos.x + "  Y: " + ballPos.y + "  Z: " + ballPos.z);
		
		//GUI.Label (new Rect (10, 50, 300, 20), " Distance: "+distance);
	}

}
