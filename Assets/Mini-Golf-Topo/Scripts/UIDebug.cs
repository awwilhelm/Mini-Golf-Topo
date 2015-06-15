using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDebug : MonoBehaviour {

	public Button restartButton;
	public Button nextLevelButton;
	private LevelManager levelManagerScript;
	
	void Start ()
	{
		levelManagerScript = transform.parent.GetComponent<LevelManager>();
	}
	
	// Update is called once per frame
	void Update ()
	{
//		restartButton.onClick.AddListener(() => {
//			//levelManagerScript.restartLevel();
//			print ("hi");
//		});
	}




}
