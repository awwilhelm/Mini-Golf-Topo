using UnityEngine;
using System.Collections;

public class BallManager : MonoBehaviour
{
	//Gets component reference
	private LevelManager levelManagerScript;
	private Rigidbody ballRigidbody;
	public CameraFollow cameraFollow;
	
	//DEBUG play without mini-map animation
	public bool animationAfterHit;
	public bool fullScreenOnHit;
	
	//Values related with the ball moving
	private float hitForce;
	private float lastTimeBallStopped;
	private bool isBallMoving;
	private bool inHole;
	private bool addForceBool;
	private bool stopBuffer;
	
	//After ball is done moving after hit, mini map should exit full screen
	private bool hasBallBeenHitForStroke;

	//Make sure ball is in the course
	private int inBoundsCount;
	private Vector3 lastPosition;
	
	//Prevent the ball from moving once it stopped
	private bool ballLock;
	
	//Constant Variables
	private const float DELAY_BEFORE_BALL_HIT = 0.4f;
	private const float MIN_BALL_VELOCITY = 0.5f;
	private const float STOP_BUFFER_TIME = 0.1f;
	
	void Start ()
	{
		
		levelManagerScript = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		ballRigidbody = GetComponent<Rigidbody> ();
		ballRigidbody.maxAngularVelocity = 20;
		
		lastTimeBallStopped = 0;
		isBallMoving = false;
		inHole = false;
		addForceBool = false;
		stopBuffer = false;
		
		hasBallBeenHitForStroke = false;
		
		inBoundsCount = 0;
		lastPosition = transform.position;
		
		ballLock = true;
	}
	
	void Update ()
	{	
		


		//Once the player clicks it will wait until it is full screen to hit the ball
		//print ((addForceBool && (cameraFollow.getFullScreen () || !animationAfterHit)) + " " + addForceBool + " " + cameraFollow.getFullScreen () + " " + !animationAfterHit);
		if (addForceBool && (cameraFollow.getFullScreen () || !animationAfterHit)) {
			addForceBool = false;
			ballLock = false;
			StartCoroutine (addForce (hitForce));
		} else if (ballLock) {
			stopBallAndExitFullScreen (false);
		}
		
		//If the ball is barely moving, the ball will stop.
		if (Time.time - lastTimeBallStopped > STOP_BUFFER_TIME && stopBuffer) {
			//WINNER
			if (inHole) {
				levelManagerScript.nextLevel ();
			} else if (cameraFollow.getFullScreen () && hasBallBeenHitForStroke) {
				stopBallAndExitFullScreen (true);
				if (inBoundsCount <= 0) {
					transform.position = lastPosition;
				} else {
					lastPosition = transform.position;
				}
			}
		} else if (!stopBuffer) {
			//Sets the timer for how long the ball needs to be stopped
			lastTimeBallStopped = Time.time;
			stopBuffer = true;
		}
		
		//Tests if the velocity is close to being stopped
		if ((Mathf.Abs (ballRigidbody.velocity.x) < MIN_BALL_VELOCITY) && (Mathf.Abs (ballRigidbody.velocity.y) < MIN_BALL_VELOCITY) && (Mathf.Abs (ballRigidbody.velocity.z) < MIN_BALL_VELOCITY)) {
			isBallMoving = false;
		} else if (hasBallBeenHitForStroke == true) {
			isBallMoving = true;
			stopBuffer = false;
		}
		
		
		
	}
	
	//Add a force to the ball and adds the hit to scorekeeping.
	public IEnumerator addForce (float force)
	{
		Vector3 forward = new Vector3 (transform.forward.x, 0, transform.forward.z);
		
		if (animationAfterHit || fullScreenOnHit)
			yield return new WaitForSeconds (DELAY_BEFORE_BALL_HIT);
		
		ballRigidbody.AddForce (forward * force, ForceMode.Acceleration);
		
		hasBallBeenHitForStroke = true;
		lastTimeBallStopped = Time.time;
	}
	
	//Allows the animation to fully play through before hitting the ball
	public void startCameraAnimationAndForce (float force)
	{
		hitForce = force;
		addForceBool = true;
		
		if (animationAfterHit) {
			cameraFollow.initiateFullScreenAnimation ();
		} else if (fullScreenOnHit) {
			cameraFollow.makeFullScreen ();
		}
		
	}
	
	//Checks if the ball entered the hole
	public void OnTriggerEnter (Collider collided)
	{
		if (collided.transform.tag == "Hole") {
			inHole = true;
		} else if (collided.transform.tag == "InBounds") {
			inBoundsCount++;
		}
	}
	
	//Checks if the ball leaves the hole
	public void OnTriggerExit (Collider collided)
	{
		if (collided.transform.tag == "Hole") {
			inHole = false;
		} else if (collided.transform.tag == "InBounds") {
			inBoundsCount--;
		} else if (collided.transform.tag == "OutOfBounds") {
			stopBallAndExitFullScreen (true);
			transform.position = lastPosition;
		}
	}
	
	//Getter to see if the ball is currently moving
	public bool getBallMoving ()
	{
		return isBallMoving;
	}
	
	private void stopBallAndExitFullScreen (bool exitFullScreen)
	{
		ballRigidbody.velocity = Vector3.zero;
		ballRigidbody.angularVelocity = Vector3.zero;
		hasBallBeenHitForStroke = false;
		ballLock = true;
		
		if (exitFullScreen) {
			cameraFollow.exitFullScreen ();
		}
	}
	
}
