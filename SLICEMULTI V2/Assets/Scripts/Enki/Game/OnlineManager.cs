using UnityEngine;
using System.Collections;

public class OnlineManager : MonoBehaviour 
{



	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void Fall(int _KilledID, int _KillerID)
	{

		GetComponent<PhotonView> ().RPC ("SendDeathMessage", PhotonTargets.All, _KilledID, _KillerID);

	}


	[RPC]
	public void SendDeathMessage(int _KilledID, int _KillerID)
	{
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
	}


}
