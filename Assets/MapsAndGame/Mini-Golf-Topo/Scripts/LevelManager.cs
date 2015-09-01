using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour
{

	private ScoreKeeping scoreKeepingScript;

	//startingLevel is the index of levelOrder
	public int startingLevel;
	public Material coloredLevels;
	public Material dashedLines;
	private Material currentMaterial;
	private int currentLevel;
	private int[] levelOrder;
	private GameObject[] obstacles;
	private bool mouseOverUI;
	private GameObject mainMenu;

	void Start ()
	{
		DontDestroyOnLoad (this);

		//Change the numbers to customize the order of the levels
		levelOrder = new int[32] {
			4,
			1,
			2,
			3,
			5,
			6,
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			17,
			18,
			20,
			21,
			22,
			23,
			25,
			26,
			27,
			28,
			29,
			30,
			31,
			32,
			33,
			34,
			35
		};

		if (startingLevel < 1)
			startingLevel = 1;

		currentLevel = startingLevel - 1;
		mainMenu = transform.FindChild ("Canvas").Find ("Main Menu").gameObject;
		
	}

	void OnLevelWasLoaded (int level)
	{
		scoreKeepingScript = GameObject.Find ("World").GetComponent<ScoreKeeping> ();
		scoreKeepingScript.resetHitsForHole ();
		transform.FindChild ("Canvas").GetComponent<Canvas> ().worldCamera = Camera.main;
		mouseOverUI = false;
		addObstacles ();
		if (currentLevel <= 20) {
			currentMaterial = coloredLevels;
		} else {
			currentMaterial = dashedLines;
		}
		setMaterialType (currentMaterial);
	}

	void loadCurrentLevel ()
	{
		removeAllObstacles ();
		if (levelOrder [currentLevel] < 10) {
			Application.LoadLevel ("Level0" + levelOrder [currentLevel] + "F");
		} else {
			Application.LoadLevel ("Level" + levelOrder [currentLevel] + "F");
		}	
		
	}

	public void restartLevel ()
	{
		loadCurrentLevel ();
	}

	public void nextLevel ()
	{
		currentLevel++;
		loadCurrentLevel ();
	}

	public void toggleMaterial ()
	{
		if (currentMaterial == dashedLines) {
			currentMaterial = coloredLevels;
		} else {
			currentMaterial = dashedLines;
		}
		setMaterialType (currentMaterial);
	}
	
	public void MouseIsOverUI ()
	{
		mouseOverUI = true;
	}
	
	public void MouseIsNotOverUI ()
	{
		mouseOverUI = false;
	}
	
	public bool GetMouseOverUI ()
	{
		return mouseOverUI;
	}
	
	public void StartingLevel (int level)
	{
		currentLevel = level - 1;
		loadCurrentLevel ();
		Destroy (mainMenu);		
	}

	private void addObstacles ()
	{
		obstacles = GameObject.FindGameObjectsWithTag ("Obstacle");
	}

	private void removeAllObstacles ()
	{
		obstacles = null;
	}

	private void setMaterialType (Material mat)
	{
		foreach (GameObject obstacle in obstacles) {
			obstacle.GetComponent<Renderer> ().material = mat;
		}
	}
}
