using UnityEngine;
using System.Collections;

public class LocalManager : MonoBehaviour 
{

	public bool _IsPaused = false;

	public GameObject _PauseUI;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			_IsPaused = !_IsPaused;
		}
		_PauseUI.SetActive (_IsPaused);

		
	}
}
