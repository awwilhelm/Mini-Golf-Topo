using UnityEngine;
using System.Collections;
using System.Threading;

public class CameraFollow : MonoBehaviour {

	public GameObject ball;
	private bool fullScreen;
	private Camera followCamera;
	public GameObject cameraBorder;

	private const int OFFSET = 30;
	private  Vector2 VIEWPORT_COORDINATES = new Vector2(0.84f, 0.74f);
	private  Vector2 VIEWPORT_SCALE = new Vector2(0.15f, 0.25f);

	private Vector2 CURR_VIEWPORT_COORDINATES;
	private Vector2 CURR_VIEWPORT_SCALE;

	private const float FULL_SCREEN_BUFFER = 0.01f;
	
	public bool fullyFullScreen = false;

	// Use this for initialization
	void Start () {
		followCamera = GetComponent<Camera>();
		CURR_VIEWPORT_COORDINATES = new Vector2(VIEWPORT_COORDINATES.x,  VIEWPORT_COORDINATES.y);
		CURR_VIEWPORT_SCALE = new Vector2(VIEWPORT_SCALE.x, VIEWPORT_SCALE.y);
		fullScreen = false;
		//camera.aspect = 1;

	}
	
	// Update is called once per frame
	void Update ()
	{
		print (followCamera.rect.xMin +" "+ followCamera.rect.yMin +" "+ followCamera.rect.width + " "+ followCamera.rect.height + " " );
		if(fullScreen)
		{
			followCamera.rect =  new Rect(Mathf.Lerp(followCamera.rect.xMin, 0.5f-followCamera.rect.width/2, 0.07f), Mathf.Lerp(followCamera.rect.yMin, 0.5f-followCamera.rect.height/2, 0.07f), Mathf.Lerp(followCamera.rect.width, 1, 0.02f), Mathf.Lerp(followCamera.rect.height, 1, 0.02f));
			//cameraBorder.SetActive(false);
			if(followCamera.rect.xMin < FULL_SCREEN_BUFFER && followCamera.rect.yMin < FULL_SCREEN_BUFFER && followCamera.rect.width > 1 - FULL_SCREEN_BUFFER && followCamera.rect.height > 1- FULL_SCREEN_BUFFER)
			{
				followCamera.rect =  new Rect(0, 0, 1, 1);
				fullyFullScreen = true;
			}
				
			else 
				fullyFullScreen = false;

		}
		else
		{
			fullyFullScreen = false;
			followCamera.rect =  new Rect(CURR_VIEWPORT_COORDINATES.x, CURR_VIEWPORT_COORDINATES.y, CURR_VIEWPORT_SCALE.x, CURR_VIEWPORT_SCALE.y);
			cameraBorder.SetActive(true);
		}
		
		
		transform.position = new Vector3(ball.transform.position.x - OFFSET, ball.transform.position.y + OFFSET, ball.transform.position.z - OFFSET);
		//transform.rotation = Quaternion.LookRotation(ball.transform.position);
		transform.LookAt(ball.transform.position);
	}

	public void setFullScreen(bool full)
	{
		fullScreen = full;
	}

	public bool getFullScreen()
	{
		return fullyFullScreen;
	}


	
}
