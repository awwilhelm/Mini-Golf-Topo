using UnityEngine;
using System.Collections;

public class BallForce : MonoBehaviour {

	//Gets component reference
	public ScoreKeeping scoreKeep;
	public CameraFollow cameraFollow;
	private Rigidbody ballRigidbody;
	private ControlBall cbScript;

	//Values related with the ball moving
	private Vector3 ballPos;
	private float hitForce;
	private bool isBallMoving;
	private bool inHole;
	private bool addForceBool;

	//After ball is done moving after hit, mini map should exit full screen
	private bool isBallMovingAfterHit = false;

	//Constant variables
	private const float DELAY_BEFORE_BALL_HIT = 0.8f;
	private const float MIN_BALL_VELOCITY = 1f;

	void Start () {
		ballRigidbody = GetComponent<Rigidbody> ();
		cbScript = GetComponent<ControlBall> ();

		ballPos = transform.position;
		hitForce = 0;
		isBallMoving = false;
		inHole =  false;
		addForceBool = false;

		isBallMovingAfterHit = false;
	}
	
	void FixedUpdate()
	{
		//If the player has clicked, wants to add force, and is in full screen. Adds force.
		if(addForceBool && cameraFollow.getFullScreen())
		{
			addForceBool = false;
			StartCoroutine(addForce(hitForce));

		}
		if((Mathf.Abs (ballRigidbody.velocity.x) < MIN_BALL_VELOCITY) && (Mathf.Abs (ballRigidbody.velocity.y) < MIN_BALL_VELOCITY) && (Mathf.Abs (ballRigidbody.velocity.z) < MIN_BALL_VELOCITY))
		{
			transform.position = ballPos;
			ballRigidbody.velocity = new Vector3 (0, 0, 0);
			isBallMoving = false;

			//WINNER
			if(inHole)
			{
				scoreKeep.setWin();
			}
			else if(cameraFollow.getFullScreen() && isBallMovingAfterHit)
			{
				cameraFollow.exitFullScreen();
				isBallMovingAfterHit = false;
			}
		}
		else
		{
			ballPos = transform.position;
			isBallMoving = true;
			isBallMovingAfterHit = true;
		}

	}

	//Add a force to the ball and adds the hit to scorekeeping.
	public IEnumerator addForce(float force)
	{
		Vector3 forward = new Vector3 (transform.forward.x, 0, transform.forward.z);

		yield return new WaitForSeconds(DELAY_BEFORE_BALL_HIT);

		ballRigidbody.AddForce (forward * force);
		scoreKeep.addToHits ();
		scoreKeep.addToScore ();
	}

	//Checks if the ball entered the hole
	public void OnTriggerEnter(Collider collided){
		if(collided.transform.tag == "Hole" )
		{
			inHole = true;
		}
	}

	//Checks if the ball leaves the hole
	public void OnTriggerExit(Collider collided){
		if(collided.transform.tag == "Hole" )
		{
			inHole = false;
		}
	}

	//Getter to see if the ball is currently moving
	public bool getBallMoving()
	{
		return isBallMoving;
	}

	//Allows the animation to fully play through before hitting the ball
	public void addHitForce(float force)
	{
		hitForce = force;
		addForceBool = true;
		cameraFollow.initiateFullScreenAnimation();
	}

}
