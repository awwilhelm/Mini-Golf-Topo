﻿using UnityEngine;
using System.Collections;
using System.Threading;

public class CameraFollow : MonoBehaviour
{

	//Gets reference to GameObjects needed
	public GameObject ball;
	public GameObject cameraBorder;
	private Camera followCamera;

	//Full screen variables
	private bool startFullScreenAnimation;
	private bool fullScreen;

	//Default mini map coordinates and size
	private Vector2 miniMapCoordinates;
	private Vector2 miniMapScale;

	//Constants needed to calculate camera position or the transition to full screen
	private const int CAMERA_OFFSET = 30;
	private const float FULL_SCREEN_BUFFER = 0.01f;
	private const float FULL_SCREEN_COOR_SPEED = 0.09f;
	private const float FULL_SCREEN_SCALE_SPEED = 0.03f;
	private const float FULL_SCREEN_SPEED_MODIFIER = 2f;
	
	// Use this for initialization
	void Start ()
	{
		followCamera = GetComponent<Camera> ();

		startFullScreenAnimation = false;
		fullScreen = false;

		miniMapCoordinates = new Vector2 (0.84f, 0.74f);
		miniMapScale = new Vector2 (0.15f, 0.25f);
		exitFullScreen ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (startFullScreenAnimation || fullScreen) {
			
			cameraBorder.SetActive (false);
			//If the camera is closer than the screen buffer, the camera will be reset to full screen
			if (!fullScreen && (followCamera.rect.xMin < FULL_SCREEN_BUFFER && followCamera.rect.yMin < FULL_SCREEN_BUFFER && followCamera.rect.width > 1 - FULL_SCREEN_BUFFER && followCamera.rect.height > 1 - FULL_SCREEN_BUFFER)) {
				//resets followCamera to full screen
				makeFullScreen ();
				startFullScreenAnimation = false;
				cameraBorder.SetActive (false);
			} else if (!fullScreen) {
				float vel = 0;
				float x = Mathf.Lerp (followCamera.rect.xMin, 0.5f - followCamera.rect.width / 2, FULL_SCREEN_COOR_SPEED * FULL_SCREEN_SPEED_MODIFIER);
				float y = Mathf.Lerp (followCamera.rect.yMin, 0.5f - followCamera.rect.height / 2, FULL_SCREEN_COOR_SPEED * FULL_SCREEN_SPEED_MODIFIER);
				float w = Mathf.Lerp (followCamera.rect.width, 1, FULL_SCREEN_SCALE_SPEED * FULL_SCREEN_SPEED_MODIFIER);
				float h = Mathf.Lerp (followCamera.rect.height, 1, FULL_SCREEN_SCALE_SPEED * FULL_SCREEN_SPEED_MODIFIER);
				
				//Uses Lerp to transition the mini map from its position to full screen.  The target is to have it centered.
				followCamera.rect = new Rect (x, y, w, h);
			}
			
		} else {
			//Resets followCamera to mini map
			
		}
		cameraController ();
	}

	private void cameraController ()
	{
		transform.position = new Vector3 (ball.transform.position.x - CAMERA_OFFSET, ball.transform.position.y + CAMERA_OFFSET, ball.transform.position.z - CAMERA_OFFSET);
		transform.LookAt (ball.transform.position);
	}

	public void initiateFullScreenAnimation ()
	{
		startFullScreenAnimation = true;
	}

	public void exitFullScreen ()
	{
		fullScreen = false;
		followCamera.rect = new Rect (miniMapCoordinates.x, miniMapCoordinates.y, miniMapScale.x, miniMapScale.y);
		cameraBorder.SetActive (true);
	}

	public bool getFullScreenAnimation ()
	{
		return startFullScreenAnimation;
	}

	public bool getFullScreen ()
	{
		return fullScreen;
	}

	public void makeFullScreen ()
	{
		followCamera.rect = new Rect (0, 0, 1, 1);
		fullScreen = true;
	}
	
}
