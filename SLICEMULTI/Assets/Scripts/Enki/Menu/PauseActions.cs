using UnityEngine;
using System.Collections;

public class PauseActions : MonoBehaviour 
{

	private LocalManager _LManager;

	void Awake()
	{

		_LManager = GameObject.Find ("Scripts").GetComponent<LocalManager> ();


	}

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}


	public void Quit()
	{
		PhotonNetwork.Disconnect ();
		Application.LoadLevel (0);
	}


	public void Resume()
	{
		StartCoroutine (ResumeGame ());
	}


	IEnumerator ResumeGame()
	{
		yield return 0;
		_LManager._IsPaused = false;
	}


}
