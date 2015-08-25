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
		xBuffer = 25;
		zBuffer = 25;
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

		if (cameraXPos + (cameraHeight / 2) < ball.transform.position.x + xBuffer)
			cameraXPos += ball.transform.position.x + xBuffer - (cameraXPos + (cameraHeight / 2));
		if (cameraZPos + (cameraWidth / 2) < ball.transform.position.z + zBuffer)
			cameraZPos += ball.transform.position.z + zBuffer - (cameraZPos + (cameraWidth / 2));

		return new Vector3 (cameraXPos, 200, cameraZPos);
	}
}
