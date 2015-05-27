using UnityEngine;
using System.Collections;

public class BallForce : MonoBehaviour {

	private new Rigidbody rigidbody;
	private bool stop = false;
	private bool inHole;
	public ControlBall cbScript;
	private ScoreKeeping scoreKeep;
	private CameraFollow cameraFollow;
	private Vector3 ballPos;
	private int cameraFollowCount = 0;

	private float hitForce;
	private bool addForceBool;

	private const int CAMERA_FOLLOW_BUFFER = 30;

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		cbScript = GetComponent<ControlBall> ();
		scoreKeep = GameObject.Find ("World").GetComponent<ScoreKeeping> ();
		cameraFollow = GameObject.Find("3DCamera").GetComponent<CameraFollow>();
		ballPos = transform.position;
		addForceBool = false;
		hitForce = 0; 
	}

	//If both the x and y are within a certain number, the ball will stop.
	void FixedUpdate()
	{
		if(addForceBool && cameraFollow.getFullScreen())
		{
			StartCoroutine(addForce(hitForce));
			addForceBool = false;
		}
		if((Mathf.Abs (rigidbody.velocity.x) < 1) && (Mathf.Abs (rigidbody.velocity.z) < 1) && (Mathf.Abs (rigidbody.velocity.y) < 1))
		{
			transform.position = ballPos;
			rigidbody.velocity = new Vector3 (0, 0, 0);
			if(inHole)
			{
				scoreKeep.setWin();
			}
			stop = true;
			if(cameraFollow.getFullScreen() && cameraFollowCount < CAMERA_FOLLOW_BUFFER)
			{
				cameraFollowCount++;
			}
			else
			{
				cameraFollowCount = 0;
				//StartCoroutine(fullScreenDelay(1f));
				//cameraFollow.setFullScreen(false);
			}
				
		}
		else
		{
			cameraFollowCount = 0;
			stop = false;
			ballPos = transform.position;
		}

	}

	//Add a force to the ball and adds the hit to scorekeeping.
	public IEnumerator addForce(float force)
	{
		yield return new WaitForSeconds(1f);
		Vector3 forward = new Vector3 (transform.forward.x, 0, transform.forward.z);

		rigidbody.AddForce (forward * force);
		scoreKeep.addToHits ();
		scoreKeep.addToScore ();
	}

	private IEnumerator fullScreenDelay (float delay)
	{
		yield return new WaitForSeconds(delay);
		cameraFollow.setFullScreen(false);
	}
	
	public void OnTriggerEnter(Collider collided){
		if(collided.transform.tag == "Hole" ){
			inHole = true;
		}
	}
	
	public void OnTriggerExit(Collider collided){
		if(collided.transform.tag == "Hole" ){
			inHole = false;
		}
	}
	public bool getStop()
	{
		return stop;
	}

	public void addHitForce(float force)
	{
		hitForce = force;
		addForceBool = true;
		cameraFollow.setFullScreen(true);
	}

}
