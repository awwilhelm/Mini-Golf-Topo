using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeping : MonoBehaviour {

	public GameObject panel;
	public Text textScore;
	public Text hitsOutOfPar;

	private bool won;
	private int score;
	private int hits;
	private int parForHole;
	private int sumPar;

	// Use this for initialization
	void Start () {
		won = false;
		score = 11;
		hits = 0;
		parForHole = 3;
		sumPar = 15;
	}
	
	// Update is called once per frame
	void Update () {
		textScore.text = score+"/"+sumPar;
		hitsOutOfPar.text = hits + "/" + parForHole;
		if (won == true) {
			panel.SetActive(true);
		} else {
			panel.SetActive(false);
		}
	}

	public void addToHits()
	{
		hits++;
		score++;
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
