using UnityEngine;
using System.Collections;

public class OnGameOpen : MonoBehaviour {

	public string _GameID = "Room";

	// Use this for initialization
	void Awake () 
	{
		DontDestroyOnLoad (this.gameObject);
		PhotonNetwork.ConnectUsingSettings(_GameID);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
