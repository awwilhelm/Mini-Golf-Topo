using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{

	public void ExitGame ()
	{
		Application.Quit ();
	}
	
	public void returnToMainLevel ()
	{
		LevelManager levelManagerScript = GameObject.Find ("LevelManager").GetComponent<LevelManager> ();
		levelManagerScript.returnToMainMenu ();
	}
}
