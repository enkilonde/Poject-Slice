using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnlineManager : MonoBehaviour 
{
	public static OnlineManager _OManager;


	public int _NumberOfPlayers;
	public int _NumberOfMouse = 0;

	public PhotonView _PhotonView;

	public List<GameObject> _Players = new List<GameObject>();

	void Awake()
	{

		if (_OManager == null) 
		{
			_OManager = this;
		} else 
		{
			Destroy(this.gameObject);
		}

	}

	// Use this for initialization
	void Start () 
	{
		_NumberOfPlayers = PhotonNetwork.playerList.Length;
		_PhotonView = GetComponent<PhotonView> ();


		StartCoroutine (updatePlayersList ());
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (_NumberOfPlayers == 1) 
		{
			//_LManager._IsMouse = true;
		}


	}

	public void Fall(GameObject _Killed, GameObject _Killer, bool IsMouse)
	{

		int _KilledID = _Killed.GetComponent<PhotonView> ().ownerId;
		int _KillerID = _Killer.GetComponent<PhotonView> ().ownerId;

		if (_KilledID != _KillerID) 
		{
			GetComponent<PhotonView> ().RPC ("SendDeathMessage", PhotonTargets.All, _KilledID, _KillerID);
			if (IsMouse)
			{
				_Killed.GetComponent<PlayerState>().SetMouseState(false);
				_Killer.GetComponent<PlayerState>().SetMouseState(true);
			}

		} else 
		{
			print("YOU KILLED YOURSELF, NOOB");
		}
	}


	[RPC]
	public void SendDeathMessage(int _KilledID, int _KillerID)
	{
		print ("Message");
		if (PhotonNetwork.player.ID == _KilledID) 
		{
			print("YOU GOT KILLED BY " + _KillerID);
		}

		if (PhotonNetwork.player.ID == _KillerID) 
		{
			print("YOU KILLED " + _KilledID);
		}

	}



	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		print ("player " + player.ID + "Connected");
		_NumberOfPlayers = PhotonNetwork.playerList.Length;

	}


	void OnPhotonPlayerDisonnected(PhotonPlayer player)
	{
		print ("player " + player.ID + "Disconnected");
		_NumberOfPlayers = PhotonNetwork.playerList.Length;

	}

	[RPC]
	public void updatePlayerList()
	{

		_Players = new List<GameObject> ();
		GameObject[] PL = GameObject.FindGameObjectsWithTag ("Player");
		foreach (GameObject player in PL) 
		{
			_Players.Add(player);
		}

		if (_Players.Count == 1) 
		{
			_Players[0].GetComponent<PlayerState>().SetMouseState(true);

		}

	}


	IEnumerator updatePlayersList()
	{
		while (true) 
		{
			yield return new WaitForSeconds (5);
			_PhotonView.RPC ("updatePlayerList", PhotonTargets.All);
		}

	}


}
