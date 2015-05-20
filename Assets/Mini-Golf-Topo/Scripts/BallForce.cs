using UnityEngine;
using System.Collections;

public class BallForce : MonoBehaviour {

	private new Rigidbody rigidbody;
	private bool stop = false;
	private bool inHole;
	public ControlBall cbScript;
	private ScoreKeeping scoreKeep;
	private Vector3 ballPos;

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		cbScript = GetComponent<ControlBall> ();
		scoreKeep = GameObject.Find ("World").GetComponent<ScoreKeeping> ();
		ballPos = transform.position;
	}

	//If both the x and y are within a certain number, the ball will stop.
	void FixedUpdate()
	{
		if((Mathf.Abs (rigidbody.velocity.x) < 1) && (Mathf.Abs (rigidbody.velocity.z) < 1))
		{
			transform.position = ballPos;
			rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
			if(inHole)
			{
				scoreKeep.setWin();
			}
			stop = true;
		}
		else
		{
			stop = false;
			ballPos = transform.position;
		}

	}

	//Add a force to the ball and adds the hit to scorekeeping.
	public void addForce(float force)
	{
		//print ("force: "+force);
		Vector3 forward = new Vector3 (transform.forward.x, 0, transform.forward.z);
		rigidbody.AddForce (forward * force);
		scoreKeep.addToHits ();
		scoreKeep.addToScore ();
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

}
