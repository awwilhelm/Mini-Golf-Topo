using UnityEngine;
using System.Collections;

public class TopDownCameraFollow : MonoBehaviour {

	public GameObject ball;
	private Camera myCamera;

	private float cameraWidth;
	private float cameraHeight;

	// Use this for initialization
	void Start ()
	{
		myCamera = GetComponent<Camera>();
		cameraHeight = 2f * myCamera.orthographicSize;
		cameraWidth = cameraHeight * myCamera.aspect;
	}
	
	// Update is called once per frame
	void Update ()
	{
		gameObject.transform.position = new Vector3(ball.transform.position.x , 500, ball.transform.position.z );
	}

}
