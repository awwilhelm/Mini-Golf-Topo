using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	private ScoreKeeping scoreKeepingScript;

	//startingLevel is the index of levelOrder
	public int startingLevel;
	private int currentLevel;
	private int[] levelOrder;

	void Start ()
	{
		DontDestroyOnLoad(this);

		//Change the numbers to customize the order of the levels
		levelOrder = new int[32] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 17, 18, 20, 21, 22, 23, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35};

		if(startingLevel<1)
			startingLevel = 1;

		currentLevel = startingLevel-1;
		loadNextLevel(currentLevel);
	}

	void OnLevelWasLoaded(int level)
	{
		scoreKeepingScript = GameObject.Find("Scene").transform.Find("World").GetComponent<ScoreKeeping>();
	}

	void Update ()
	{
		if(scoreKeepingScript != null && scoreKeepingScript.getWin())
		{
			scoreKeepingScript.setWin(false);
			currentLevel++;
			loadNextLevel(currentLevel);
		}
	}

	void loadNextLevel(int level)
	{
		if(currentLevel<10)
		{
			Application.LoadLevel("Level0"+levelOrder[currentLevel]+"F");
		}
		else
		{
			Application.LoadLevel("Level"+levelOrder[currentLevel]+"F");
		}
	}

}
