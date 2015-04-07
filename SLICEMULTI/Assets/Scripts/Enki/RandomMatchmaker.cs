using UnityEngine;
using System.Collections;

public class RandomMatchmaker : MonoBehaviour {

	public GameObject _Player;

	// Use this for initialization
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.6");

	}
	
	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		//GUILayout.Label(PhotonNetwork.room.ToString());
	}
	
	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}
	
	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom ("Room1");
	} 
	
	void OnJoinedRoom ()
	{
		Debug.Log ("Room Joined");
		Debug.Log (PhotonNetwork.room);

		_Player = PhotonNetwork.Instantiate ("Player", new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f)), Quaternion.identity, 0) as GameObject;
		//GameObject Player = PhotonNetwork.Instantiate ("Player", new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;

		_Player.name = "Player " + PhotonNetwork.player.ID;
		PhotonNetwork.player.name = _Player.name;
	




	}

	

	// Update is called once per frame
	void Update () {


	}
}
