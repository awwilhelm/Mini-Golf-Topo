using UnityEngine;
using System.Collections;
using System.Threading;

public class ControlBall : MonoBehaviour {

	//Contains instances of GameObjects and scripts
	public Canvas canvasArrow;
	public RectTransform arrowHead;
	public RectTransform arrowTail;
	public GameObject camera3D;
	public GameObject scoreKeepingObject;
	public Material arrowTailShader;
	private ScoreKeeping scoreKeepingScript;	
	private BallForce bfScript;
	private CameraFollow cameraFollowScript;
	private CanvasRenderer arrowTailRenderer;

	//Deals with mouse position on screen and where it is
	private float distance;
	private float arrowScalePercent;
	private Vector3 v3_transform;

	//Used for scaling and how much power to use to hit the ball
	private const int DISTANCE_OFFSET = 30000;
	private const float FORCE_CURVE_SCALE = -1/12000000.0f;
	private const float MAX_POWER = 10;
	private const float MIN_POWER = 0;
	private const float MAX_ARROW_SCALE = 40;
	private const float MIN_ARROW_SCALE = 10;
	private const float ARROW_HEAD_SCALE_COEFFICIENT = 3;
	private const float ARROW_TAIL_SCALE_COEFFICIENT = 2;
	private const float ARROW_TAIL_MOUSE_OFFEST = 6;
	
	void Start () {
		arrowHead.sizeDelta = new Vector2(arrowHead.rect.width * ARROW_HEAD_SCALE_COEFFICIENT, arrowHead.rect.height * ARROW_HEAD_SCALE_COEFFICIENT);
		arrowTail.sizeDelta = new Vector2(arrowTail.rect.width, arrowTail.rect.height * ARROW_TAIL_SCALE_COEFFICIENT);
		scoreKeepingScript = scoreKeepingObject.GetComponent<ScoreKeeping>();
		bfScript = GetComponent<BallForce> ();
		cameraFollowScript = camera3D.GetComponent<CameraFollow>();
		arrowTailRenderer = arrowTail.GetComponent<CanvasRenderer>();
		arrowTailRenderer.SetMaterial(arrowTailShader, null);
		distance = 0;
		arrowScalePercent = 0;
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
				float force, forceCurve;
				force = arrowScalePercent*(MAX_POWER-MIN_POWER) + MIN_POWER;

				forceCurve = 2750 * force + 300;

				bfScript.addHitForce(forceCurve);
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

		if(distance<MAX_ARROW_SCALE && distance>MIN_ARROW_SCALE)
		{
			arrowTail.sizeDelta = new Vector2(distance - ARROW_TAIL_MOUSE_OFFEST, arrowTail.rect.height);

		}
		else if(distance>=MAX_ARROW_SCALE)
		{
			arrowTail.sizeDelta = new Vector2(MAX_ARROW_SCALE - ARROW_TAIL_MOUSE_OFFEST, arrowTail.rect.height);
			distance = MAX_ARROW_SCALE;
		}
		else if(distance<=MIN_ARROW_SCALE)
		{
			arrowTail.sizeDelta = new Vector2(MIN_ARROW_SCALE - ARROW_TAIL_MOUSE_OFFEST, arrowTail.rect.height);
			distance = MIN_ARROW_SCALE;
		}
		arrowScalePercent = (distance-MIN_ARROW_SCALE)/(MAX_ARROW_SCALE-MIN_ARROW_SCALE);
		arrowTailRenderer.GetMaterial().SetFloat("_TexWidth",  arrowScalePercent);
	}

}
