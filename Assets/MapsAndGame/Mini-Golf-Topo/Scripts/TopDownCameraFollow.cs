using UnityEngine;
using System.Collections;

public class TopDownCameraFollow : MonoBehaviour
{

	public GameObject ball;
	private GameObject hole;
	private Camera thisCamera;

	public float xBuffer;
	public float zBuffer;

	// Use this for initialization
	void Start ()
	{
		hole = GameObject.Find ("Hole");
		thisCamera = GetComponent<Camera> ();
		xBuffer = Screen.height / 18; //Max is 750
		zBuffer = Screen.width / 35;  //Max is 1400
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.position = calcCameraPosition ();
	}

	private Vector3 calcCameraPosition ()
	{
		float cameraXPos = (ball.transform.position.x + hole.transform.position.x) / 2.0f;
		float cameraZPos = (ball.transform.position.z + hole.transform.position.z) / 2.0f;
		float cameraHeight = thisCamera.ScreenToWorldPoint (new Vector3 (0, thisCamera.pixelHeight, thisCamera.nearClipPlane)).x - thisCamera.ScreenToWorldPoint (new Vector3 (0, 0, thisCamera.nearClipPlane)).x;
		float cameraWidth = thisCamera.ScreenToWorldPoint (new Vector3 (0, 0, thisCamera.nearClipPlane)).z - thisCamera.ScreenToWorldPoint (new Vector3 (thisCamera.pixelWidth, 0, thisCamera.nearClipPlane)).z;

		//Left and right or the width is the z coordinate and up and down or height is the x coordinate
		//(0,0) is at the CENTER and gets bigger in top left
		if (cameraXPos + (cameraHeight / 2) < ball.transform.position.x + xBuffer) {
			cameraXPos = ball.transform.position.x + xBuffer - (cameraHeight / 2);
		} else if (cameraXPos - (cameraHeight / 2) > ball.transform.position.x - xBuffer) {
			cameraXPos = ball.transform.position.x - xBuffer + (cameraHeight / 2);
		}
		
		if (cameraZPos + (cameraWidth / 2) < ball.transform.position.z + zBuffer) {
			cameraZPos = ball.transform.position.z + zBuffer - (cameraWidth / 2);
		} else if (cameraZPos - (cameraWidth / 2) > ball.transform.position.z - zBuffer) {
			cameraZPos = ball.transform.position.z - zBuffer + (cameraWidth / 2);
		}

		return new Vector3 (cameraXPos, 200, cameraZPos);
	}
}
