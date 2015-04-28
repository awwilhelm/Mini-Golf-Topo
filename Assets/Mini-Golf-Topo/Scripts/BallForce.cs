using UnityEngine;
using System.Collections;

public class BallForce : MonoBehaviour {

	private Rigidbody rigidbody;
	private bool stopX = false;
	private bool stopZ = false;
	private bool printOnce = true;
	private bool inHole;
	public ControlBall cbScript;
	private ScoreKeeping scoreKeep;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		cbScript = GetComponent<ControlBall> ();
		scoreKeep = GameObject.Find ("World").GetComponent<ScoreKeeping> ();
	}
	void FixedUpdate()
	{
		if((Mathf.Abs (rigidbody.velocity.x) < 3) && (Mathf.Abs (rigidbody.velocity.z) < 3))
		{
			stopX = true;
			rigidbody.velocity = new Vector3 (0, rigidbody.velocity.y, rigidbody.velocity.z);
			stopZ = true;
			rigidbody.velocity = new Vector3 (rigidbody.velocity.x, rigidbody.velocity.y, 0);
		}
		else
		{
			stopX = false;
			stopZ = false;
		}

		if(stopX && stopZ)
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
	public void rotateBall()
	{

	}

	public void addForce(float force)
	{
		//print ("force: "+force);
		rigidbody.AddForce (transform.forward * force);
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
	public bool getStopX()
	{
		return stopX;
	}

	public bool getStopZ()
	{
		return stopZ;
	}
}
