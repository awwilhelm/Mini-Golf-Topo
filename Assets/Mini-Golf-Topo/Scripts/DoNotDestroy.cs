using UnityEngine;
using System.Collections;

public class DoNotDestroy : MonoBehaviour
{

	void Start ()
	{
		DontDestroyOnLoad (this);
	}

}
