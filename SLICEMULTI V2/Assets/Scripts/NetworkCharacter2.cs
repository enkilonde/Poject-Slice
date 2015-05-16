using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class NetworkCharacter2 : MonoBehaviour {

	/*
	private PhotonView _PhotonView;

	public string _PlayerName;

	private Vector3 _position;
	private Quaternion _rotation;
	public float _LimiteTP = 5;
	public float _Smoothing = 1;

	private float _TPAmount = 0;
	private float _TPLim = 0;
	

	public int _Score = 0;
	private ScoreManager _Scrmanager;
	public float _FrequenceScore = 0.5f;


	public GameObject _AuraContainer;

	public GameManager.Equipe _Equipe;

	public List<GameObject> _Players = new List<GameObject>();

	private GameManager _GManager;

	public Material _PayerBlue;
	public Material _PayerRed;
	public Material _PayerYellow;
	public Material _PayerGreen;



	// Use this for initialization
	void Start () {

		_GManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		_PhotonView = GetComponent<PhotonView> ();


		_AuraContainer = transform.Find ("Aura").gameObject;
		_AuraContainer.SetActive (false);

		if (_PhotonView.isMine == true) 
		{

			SetColor ();


			GetComponent<CharacterControls> ().enabled = true;
			GetComponent<MouseLook> ().enabled = true;
			//GetComponent<CreatePortal> ().enabled = true;
			transform.Find ("Camera").GetComponent<Camera> ().enabled = true;
			transform.Find ("CameraUI").GetComponent<Camera> ().enabled = true;
			transform.Find ("Camera").GetComponent<MouseLook> ().enabled = true;
			transform.Find ("FootCollider").gameObject.SetActive (true);
			transform.Find ("Scripts").gameObject.SetActive (true);
			transform.Find("PlayerName").gameObject.SetActive(false);
			//transform.Find("CharacterMesh").gameObject.SetActive(false);
			//transform.Find("Character Mesh").gameObject.SetActive(false);





		} else 
		{
	


		}

		//Color _plcolor = transform.Find("Character Mesh").Find("MainBody").GetComponent<Renderer>().material.color;


		_Scrmanager = GetComponent<ScoreManager>();
		//StartCoroutine (ScoreIncrement());
		//StartCoroutine (UpdatePlayerList());

	}
	
	// Update is called once per frame
	void Update () {

		_TPLim += Time.deltaTime;
		if (_TPLim >= 2 && _TPAmount > 0) 
		{
			_TPLim = 0;
			_TPAmount--;
		}


		if (_TPAmount > 5) 
		{
			_TPAmount = 0;
			_Smoothing *= 0.75f;
		}


		if (_TPLim > 20)
		{
			//_Smoothing *= 1.01f;
		}

		if (!_PhotonView.isMine) 
		{


			if (Vector3.Distance(transform.position, _position) < _LimiteTP)
			{
				transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime / _Smoothing);
			}else
			{
				transform.position = _position;
				_TPAmount++;
			}
			transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime / _Smoothing);
			




		}


		if (_PhotonView.isMine) 
		{


			
		}






		if (_IsMouse) 
		{
			GetComponent<Renderer> ().material = _Mouse;


		} else 
		{
			GetComponent<Renderer> ().material = _NonMouse;
		}





		_Scrmanager._LocalScore = _Score;
		


		transform.Find ("PlayerName").Find ("Text").GetComponent<Text> ().text = _PlayerName;



		if (_PhotonView.isMine) 
		{
			foreach (GameObject _Playe in _Players) 
			{
				if (_Playe != this.gameObject)
				{
					_Playe.transform.Find("PlayerName").transform.LookAt(transform.position);
				}
				
			}
		}




	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
			stream.SendNext(_Score);
			stream.SendNext(_PlayerName);
		}
		else
		{
			// Network player, receive data
			_position = (Vector3) stream.ReceiveNext();
			_rotation = (Quaternion) stream.ReceiveNext();
			_Score = (int) stream.ReceiveNext();
			_PlayerName = (string) stream.ReceiveNext();
		}
	}


	

	


	


	[RPC]
	public void SetPlayerName(string _name)
	{
		transform.Find("PlayerName").Find("Text").GetComponent<Text>().text = _name;

	}


	IEnumerator UpdatePlayerList()
	{
		while (true) 
		{
			//UpdatePlayerList2();
			yield return new WaitForSeconds (3.0f);
		}


	}


	void UpdatePlayerList2()
	{



	}


	public void SetColor ()
	{
		//_Equipe = _GManager.SetPlayerColor ();



		if (PunTeams.PlayersPerTeam [PunTeams.Team.blue].Count <= (PhotonNetwork.playerList.Length-1)/4) 
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
			_Equipe = GameManager.Equipe.Blue;
		}

		if (PunTeams.PlayersPerTeam [PunTeams.Team.green].Count <= (PhotonNetwork.playerList.Length-1)/4) 
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.green);
			_Equipe = GameManager.Equipe.Green;
		}

		if (PunTeams.PlayersPerTeam [PunTeams.Team.red].Count <= (PhotonNetwork.playerList.Length-1)/4) 
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.red);
			_Equipe = GameManager.Equipe.Red;
		}

		if (PunTeams.PlayersPerTeam [PunTeams.Team.yellow].Count <= (PhotonNetwork.playerList.Length-1)/4) 
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.yellow);
			_Equipe = GameManager.Equipe.Yellow;
		}





		switch (_Equipe) 
		{
		case GameManager.Equipe.Blue : transform.Find("Character Mesh").Find("MainBody").GetComponent<Renderer>().material = _PayerBlue; break;
		case GameManager.Equipe.Green : transform.Find("Character Mesh").Find("MainBody").GetComponent<Renderer>().material = _PayerGreen; break;
		case GameManager.Equipe.Red : transform.Find("Character Mesh").Find("MainBody").GetComponent<Renderer>().material = _PayerRed; break;
		case GameManager.Equipe.Yellow : transform.Find("Character Mesh").Find("MainBody").GetComponent<Renderer>().material = _PayerYellow; break;
			
		}


	}

	void OnPhotonPlayerConnected(PhotonPlayer player)
	{

		//GetComponent<PhotonView>().RPC("SynchroniseColor", PhotonTargets.All, _PlayerColor.ToString());
		//UpdatePlayerList2 ();
		if (_PhotonView.isMine) 
		{
			switch(_Equipe)
			{
			case GameManager.Equipe.Blue : _PhotonView.RPC("UpdateColor", PhotonTargets.AllBuffered, 1); break;
			case GameManager.Equipe.Red : _PhotonView.RPC("UpdateColor", PhotonTargets.AllBuffered, 2); break;
			case GameManager.Equipe.Green : _PhotonView.RPC("UpdateColor", PhotonTargets.AllBuffered, 3); break;
			case GameManager.Equipe.Yellow : _PhotonView.RPC("UpdateColor", PhotonTargets.AllBuffered, 4); break;


			}

		}
	}
	

	

	[RPC]
	public void ActivateAura( bool _AuraState)
	{

		if (!_PhotonView.isMine) 
		{
			_AuraContainer.SetActive (_AuraState);
		}



	}
	


	[RPC]
	public void UpdateColor(int _teamID)
	{
		switch (_teamID) 
		{
		case 1 : _Equipe = GameManager.Equipe.Blue; break;
		case 2 : _Equipe = GameManager.Equipe.Red; break;
		case 3 : _Equipe = GameManager.Equipe.Green; break;
		case 4 : _Equipe = GameManager.Equipe.Yellow; break;

		}

	}
*/

}
