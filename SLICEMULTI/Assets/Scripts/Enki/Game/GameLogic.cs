using UnityEngine;
using System.Collections;

public class GameLogic : Photon.MonoBehaviour {

	public static int playerWhoIsIt;


	private static PhotonView ScenePhotonView;
	
	void Start()
	{
		ScenePhotonView = this.GetComponent<PhotonView>();
	}
	
	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		Debug.Log("OnPhotonPlayerConnected: " + player);
		
		// when new players join, we send "who's it" to let them know
		// only one player will do this: the "master"

		Debug.Log ("New Player");

		if (PhotonNetwork.isMasterClient)
		{
			//TagPlayer(playerWhoIsIt);
		}
	}
	
	public static void TagPlayer(int playerID)
	{
		Debug.Log("TagPlayer: " + playerID);
		ScenePhotonView.RPC("TaggedPlayer", PhotonTargets.All, playerID);
	}



	void OnJoinedRoom()
	{
		// game logic: if this is the only player, we're "it"
		if (PhotonNetwork.playerList.Length == 1)
		{
			playerWhoIsIt = PhotonNetwork.player.ID;
		}
		
		Debug.Log("playerWhoIsIt: " + playerWhoIsIt);
	}
}
