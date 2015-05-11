using UnityEngine;
using System.Collections;

public class BallForce : MonoBehaviour {

	private Rigidbody rigidbody;
	private bool stop = false;
	private bool printOnce = true;
	private bool inHole;
	public ControlBall cbScript;
	private ScoreKeeping scoreKeep;
	private Vector3 ballPos;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		cbScript = GetComponent<ControlBall> ();
		scoreKeep = GameObject.Find ("World").GetComponent<ScoreKeeping> ();
		ballPos = transform.position;
	}
	void FixedUpdate()
	{
		if((Mathf.Abs (rigidbody.velocity.x) < 1) && (Mathf.Abs (rigidbody.velocity.z) < 1))
		{
			transform.position = ballPos;
			rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, 0);
			stop = true;
		}
		else
		{
			stop = false;
			ballPos = transform.position;
		}

		if(stop)
		{
			if(inHole)
			{
				scoreKeep.setWin();
				if(printOnce)
				{
					printOnce = false;
					print ("You win!");
				}
			}
			else
			{
				cbScript.addForceAgain();
			}
		}

	}

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
