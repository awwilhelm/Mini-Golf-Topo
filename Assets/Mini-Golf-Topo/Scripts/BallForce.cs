using UnityEngine;
using System.Collections;

public class BallForce : MonoBehaviour {

	//Gets component reference
	public ScoreKeeping scoreKeepingScript;
	public CameraFollow cameraFollow;
	private Rigidbody ballRigidbody;

	//Values related with the ball moving
	private float hitForce;
	private bool isBallMoving;
	private bool inHole;
	private bool addForceBool;
	private float lastTimeBallStopped=0;
	private bool stopBuffer = false;

	//After ball is done moving after hit, mini map should exit full screen
	private bool hasBallBeenHitForStroke;

	//DEBUG play without mini-map animation
	public bool animationAfterHit;
	public bool fullScreenOnHit;

	//Constant variables
	private const float DELAY_BEFORE_BALL_HIT = 0.25f;
	private const float MIN_BALL_VELOCITY = 0.5f;
	private const float STOP_BUFFER_TIME = 0.25f;

	void Start () {
		ballRigidbody = GetComponent<Rigidbody> ();
		ballRigidbody.maxAngularVelocity = 20;

		hitForce = 0;
		isBallMoving = false;
		inHole =  false;
		addForceBool = false;

		animationAfterHit = false;
		fullScreenOnHit = true;

		hasBallBeenHitForStroke = false;
	}
	
	void Update()
	{
		//If the player has clicked, wants to add force, and is in full screen. Adds force.
		if(addForceBool && (cameraFollow.getFullScreen() || !animationAfterHit))
		{
			addForceBool = false;
			StartCoroutine(addForce(hitForce));

		}

		//The balls velocity has to be stopped for a certain time and if it is, the ball will finally be considered stopped.
		if(Time.time - lastTimeBallStopped > STOP_BUFFER_TIME && stopBuffer)
		{
			//WINNER
			if(inHole)
			{
				scoreKeepingScript.setWin(true);
			}
			else if(cameraFollow.getFullScreen() && hasBallBeenHitForStroke)
			{
				ballRigidbody.velocity = Vector3.zero;
				ballRigidbody.angularVelocity = Vector3.zero;
				cameraFollow.exitFullScreen();
				hasBallBeenHitForStroke = false;
			}
		}
		else if(!stopBuffer)
		{
			lastTimeBallStopped = Time.time;
			stopBuffer = true;
		}

		//Tests if the velocity is close to being stopped
		if((Mathf.Abs (ballRigidbody.velocity.x) < MIN_BALL_VELOCITY) && (Mathf.Abs (ballRigidbody.velocity.y) < MIN_BALL_VELOCITY) && (Mathf.Abs (ballRigidbody.velocity.z) < MIN_BALL_VELOCITY))
		{
			isBallMoving = false;
		}
		else if(hasBallBeenHitForStroke == true)
		{
			isBallMoving = true;
			stopBuffer = false;
		}
	}

	//Add a force to the ball and adds the hit to scorekeeping.
	public IEnumerator addForce(float force)
	{
		Vector3 forward = new Vector3 (transform.forward.x, 0, transform.forward.z);

		if(animationAfterHit || fullScreenOnHit)
			yield return new WaitForSeconds(DELAY_BEFORE_BALL_HIT);

		ballRigidbody.AddForce (forward * force, ForceMode.Acceleration);

		hasBallBeenHitForStroke = true;
		lastTimeBallStopped = Time.time;
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
