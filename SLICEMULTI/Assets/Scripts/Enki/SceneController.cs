using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneController : MonoBehaviour {
	public static SceneController _control;
	

	public int _NombreMaxDeJoueurs = 20;
	public int _NumberOfPlayers = 0;


	public List<Vector3> _Positions = new List<Vector3>();


	// Use this for initialization
	void Awake () {
		if (_control == null) {
						_control = this;
				} else {
			Destroy(this.gameObject);
				}


	


	}
	
	// Update is called once per frame
	void Update () {

		_NumberOfPlayers = PhotonNetwork.playerList.Length;
	
	}

	void OnPhotonPlayerConnected(PhotonPlayer newPlayer) 
	{



	} 
	

	void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer) 
	{ 




	}

}
