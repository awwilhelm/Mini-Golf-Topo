using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeping : MonoBehaviour {

	private bool won;
	private int score;
	private int hits;
	public Text textScore;
	public Text hitsScore;
	public Text hitsForHole;
	public GameObject panel;

	// Use this for initialization
	void Start () {
		won = false;
		score = 0;
		hits = 0;
	}
	
	// Update is called once per frame
	void Update () {
		textScore.text = score+"/30";
		hitsScore.text = ""+hits;
		hitsForHole.text = "3";
		if (won == true) {
			panel.SetActive(true);
		} else {
			panel.SetActive(false);
		}
	}

	public void addToScore()
	{
		score+=5;
	}

	public void addToHits()
	{
		hits++;
	}

	public int getScore()
	{
		return score;
	}

	public int getHits()
	{
		return hits;
	}

	public void setWin()
	{
		won = true;
	}

	public bool getWin()
	{
		return won;
	}
}
