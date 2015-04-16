using UnityEngine;
using System.Collections;

public class JoinRoom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadScene()
	{
		Application.LoadLevel (Application.loadedLevel+1);
	}


	public void QuitGame()
	{
		Application.Quit ();
	}
}
