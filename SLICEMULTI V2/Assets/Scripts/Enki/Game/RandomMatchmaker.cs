using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomMatchmaker : MonoBehaviour {




	private Vector3 _SpawnPosition;
	private float _SpawnRandom;


	public GameObject _Player;

	private OnlineManager _OManager;


	private LocalManager _LManager;

	// Use this for initialization
	void Start () {

		_OManager = GameObject.Find ("OnlineManager").GetComponent<OnlineManager> ();
		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();

		_SpawnPosition = _LManager._SpawnPosition;
		_SpawnRandom = _LManager._SpawnRandom;


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
		print ("room name : " + PhotonNetwork.room.name);
		Vector3 _position = new Vector3 (_SpawnPosition.x + Random.Range(-_SpawnRandom, _SpawnRandom), _SpawnPosition.y, _SpawnPosition.z + Random.Range(-_SpawnRandom, _SpawnRandom));

		_Player = PhotonNetwork.Instantiate ("Player", _position, Quaternion.identity, 0) as GameObject;
		//GameObject Player = PhotonNetwork.Instantiate ("Player", new Vector3(0, 0, 0), Quaternion.identity, 0) as GameObject;

		_Player.name = _LManager._PlayerName;
		PhotonNetwork.player.name = _LManager._PlayerName;
		_Player.transform.Find("PlayerName").Find("Text").GetComponent<Text>().text = _LManager._PlayerName;
		_Player.GetComponent<NetworkCharacter> ()._PlayerName = _LManager._PlayerName;
		//_OManager._PhotonView.RPC ("updatePlayerList", PhotonTargets.All);

	}

	

	// Update is called once per frame
	void Update () {


	}
}
