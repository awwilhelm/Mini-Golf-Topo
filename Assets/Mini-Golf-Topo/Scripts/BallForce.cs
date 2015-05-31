using UnityEngine;
using System.Collections;

public class BallForce : MonoBehaviour {

	//Gets component reference
	public ScoreKeeping scoreKeep;
	public CameraFollow cameraFollow;
	private Rigidbody ballRigidbody;

	//Values related with the ball moving
	private Vector3 ballPos;
	private float hitForce;
	public bool isBallMoving;
	private bool inHole;
	private bool addForceBool;

	//After ball is done moving after hit, mini map should exit full screen
	private bool isBallMovingAfterHit;

	//DEBUG play without mini-map animation
	public bool animationAfterHit;
	public bool fullScreenOnHit;

	//Constant variables
	private const float DELAY_BEFORE_BALL_HIT = 0.25f;
	private const float MIN_BALL_VELOCITY = 1f;

	void Start () {
		ballRigidbody = GetComponent<Rigidbody> ();

		ballPos = transform.position;
		hitForce = 0;
		isBallMoving = false;
		inHole =  false;
		addForceBool = false;

		animationAfterHit = true;
		fullScreenOnHit = false;

		isBallMovingAfterHit = false;
	}
	
	void FixedUpdate()
	{
		//If the player has clicked, wants to add force, and is in full screen. Adds force.
		if(addForceBool && (cameraFollow.getFullScreen() || !animationAfterHit))
		{
			addForceBool = false;
			StartCoroutine(addForce(hitForce));

		}

		if((Mathf.Abs (ballRigidbody.velocity.x) < MIN_BALL_VELOCITY) && (Mathf.Abs (ballRigidbody.velocity.y) < MIN_BALL_VELOCITY) && (Mathf.Abs (ballRigidbody.velocity.z) < MIN_BALL_VELOCITY))
		{
			ballRigidbody.velocity = new Vector3 (0, 0, 0);
			ballRigidbody.angularVelocity = new Vector3 (0, 0, 0);

			//WINNER
			if(inHole)
			{
				scoreKeep.setWin();
			}
			else if(cameraFollow.getFullScreen() && isBallMovingAfterHit && isBallMoving)
			{
				cameraFollow.exitFullScreen();
				isBallMovingAfterHit = false;
			}
			isBallMoving = false;

		}
		else
		{
			isBallMoving = true;
		}
	}

	//Add a force to the ball and adds the hit to scorekeeping.
	public IEnumerator addForce(float force)
	{
		Vector3 forward = new Vector3 (transform.forward.x, 0, transform.forward.z);

		if(animationAfterHit || fullScreenOnHit)
			yield return new WaitForSeconds(DELAY_BEFORE_BALL_HIT);

		ballRigidbody.AddForce (forward * force);
		scoreKeep.addToHits ();
		scoreKeep.addToScore ();

		isBallMovingAfterHit = true;
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

		if(animationAfterHit)
			cameraFollow.initiateFullScreenAnimation();
		else if(fullScreenOnHit)
			cameraFollow.makeFullScreen();

	}

}
