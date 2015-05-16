using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class NetworkCharacter : MonoBehaviour {

	

	private PhotonView _PhotonView;

	public LocalManager.Equipe _Equipe;

	public string _PlayerName;

	private Vector3 _position;
	private Quaternion _rotation;
	public float _LimiteTP = 5;
	public float _Smoothing = 1;

	private float _TPAmount = 0;
	private float _TPLim = 0;

	public bool _IsMouse = false;
	
	private bool _NoColor = true;


	public Color _plcolor = new Color();
	public string _plcolorHEXA;

	public int _MouseScoreMultiplier = 1;
	public int _BaseScorePerSecond = 1;

	public int _Score = 0;
	private ScoreManager _Scrmanager;
	public float _FrequenceScore = 0.5f;


	public GameObject _AuraContainer;
	public GameObject _CentralDisplayPrefab;

	public Color _Red;
	public Color _Blue;
	public Color _Green;
	public Color _Yellow;


	private ParticleSystem _FLAME;

	public List<GameObject> _Players = new List<GameObject>();

	// Use this for initialization
	void Awake () {

		_PhotonView = GetComponent<PhotonView> ();

		//SetColor ();
		_AuraContainer = transform.Find ("Aura").gameObject;
		_AuraContainer.SetActive (false);

		_FLAME = transform.Find ("THE FLAME").GetComponent<ParticleSystem> ();


		if (_PhotonView.isMine == true) 
		{

			SetColor();

			GetComponent<CharacterControls> ().enabled = true;
			GetComponent<MouseLook> ().enabled = true;
			GetComponent<CreatePortal> ().enabled = true;
			transform.Find ("Camera").GetComponent<Camera> ().enabled = true;
			transform.Find ("Camera").GetComponent<AudioListener> ().enabled = true;
			transform.Find ("CameraUI").GetComponent<Camera> ().enabled = true;
			transform.Find("Camera").Find ("Camera Flame").GetComponent<Camera> ().enabled = true;
			transform.Find ("Camera").GetComponent<MouseLook> ().enabled = true;
			transform.Find ("FootCollider").gameObject.SetActive (true);
			transform.Find ("Scripts").gameObject.SetActive (true);
			transform.Find("PlayerName").gameObject.SetActive(false);
			//transform.Find("CharacterMesh").gameObject.SetActive(false);
			transform.Find("Character Mesh").gameObject.SetActive(false);
			_FLAME.Stop();




		} else 
		{


		}

		//Color _plcolor = transform.Find("Character Mesh").Find("MainBody").GetComponent<Renderer>().material.color;


		_Scrmanager = GetComponent<ScoreManager>();
		StartCoroutine (ScoreIncrement());
		StartCoroutine (UpdatePlayerList());

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
			


			_AuraContainer.SetActive(_IsMouse);


		}


		if (_PhotonView.isMine) 
		{


			
		}





		/*
		if (_IsMouse) 
		{
			GetComponent<Renderer> ().material = _Mouse;


		} else 
		{
			GetComponent<Renderer> ().material = _NonMouse;
		}
		*/




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
			stream.SendNext(_IsMouse);
			stream.SendNext(_Score);
			stream.SendNext(_PlayerName);
		}
		else
		{
			// Network player, receive data
			_position = (Vector3) stream.ReceiveNext();
			_rotation = (Quaternion) stream.ReceiveNext();
			_IsMouse = (bool) stream.ReceiveNext();
			_Score = (int) stream.ReceiveNext();
			_PlayerName = (string) stream.ReceiveNext();
		}
	}



	[RPC]
	public void SwapMouse(bool mouse)
	{

		_IsMouse = mouse;

		if (mouse) 
		{
			if (!_PhotonView.isMine)
			{
				_FLAME.Play();
			}



		} else 
		{
			_FLAME.Stop();

		}


		if (mouse && _PhotonView.isMine) 
		{
			string _msg = "Player " + "<color="+ _plcolorHEXA +">" + _PlayerName +"</color>"+" is now the Fame";
			_PhotonView.RPC("SendSwapMouseMessage", PhotonTargets.All, _msg);
			SendCentralMessageMouse("You are now the flame \n RUN!");
		}

	}

	[RPC]
	public void SendSwapMouseMessage(string txt)
	{
		if (!_PhotonView.isMine) 
		{
			SendCentralMessageMouse (txt);
		}
	}


	IEnumerator ScoreIncrement()
	{
		while (true) 
		{
			yield return new WaitForSeconds(1/_FrequenceScore);
			if(_PhotonView.isMine && _IsMouse)
			{
				_Score += _BaseScorePerSecond * _MouseScoreMultiplier;
			}

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
			UpdatePlayerList2();
			yield return new WaitForSeconds (3.0f);
		}


	}


	void UpdatePlayerList2()
	{

		_Players = new List<GameObject> ();
		GameObject[] _g = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject _gg in _g)
		{
			_Players.Add(_gg);


		}

	
	
	

	}


	public void SetColor ()
	{
		//_Equipe = _GManager.SetPlayerColor ();
		
		
		
		if (PunTeams.PlayersPerTeam [PunTeams.Team.blue].Count <= (PhotonNetwork.playerList.Length-1)/4) 
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.blue);
			_Equipe = LocalManager.Equipe.Blue;
		}
		
		if (PunTeams.PlayersPerTeam [PunTeams.Team.green].Count <= (PhotonNetwork.playerList.Length-1)/4) 
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.green);
			_Equipe = LocalManager.Equipe.Green;
		}
		
		if (PunTeams.PlayersPerTeam [PunTeams.Team.red].Count <= (PhotonNetwork.playerList.Length-1)/4) 
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.red);
			_Equipe = LocalManager.Equipe.Red;
		}
		
		if (PunTeams.PlayersPerTeam [PunTeams.Team.yellow].Count <= (PhotonNetwork.playerList.Length-1)/4) 
		{
			PhotonNetwork.player.SetTeam(PunTeams.Team.yellow);
			_Equipe = LocalManager.Equipe.Yellow;
		}
		
		
		

		
		switch (_Equipe) 
		{
		case LocalManager.Equipe.Blue : _plcolor = _Blue; break;
		case LocalManager.Equipe.Green : _plcolor = _Green; break;
		case LocalManager.Equipe.Red : _plcolor = _Red; break;
		case LocalManager.Equipe.Yellow : _plcolor = _Yellow; break;
			
		}

		transform.Find("Character Mesh").Find("MainBody").GetComponent<Renderer>().material.color = _plcolor;
		
		_AuraContainer.transform.Find ("AuraUp").GetComponent<ParticleSystem> ().startColor = _plcolor;
		_AuraContainer.transform.Find ("AuraGround").GetComponent<ParticleSystem> ().startColor = _plcolor;
		
		transform.Find("Canvas").Find("UISideGauche").GetComponent<Image>().color = _plcolor;
		transform.Find("Canvas").Find("UISideDroite").GetComponent<Image>().color = _plcolor;
		
		_plcolorHEXA = RGBToHexa(_plcolor);
		

		
	}

	void OnPhotonPlayerConnected(PhotonPlayer player)
	{

		//GetComponent<PhotonView>().RPC("SynchroniseColor", PhotonTargets.All, _PlayerColor.ToString());
		//UpdatePlayerList2 ();
	}


	[RPC]
	public void SynchroniseColor(string _col)
	{


	}

	

	[RPC]
	public void ActivateAura( bool _AuraState)
	{

		if (!_PhotonView.isMine) 
		{
			_AuraContainer.SetActive (_AuraState);
		}



	}




	public void SendCentralMessageMouse(string txt)
	{
		
		GameObject CTXT = GameObject.Instantiate (_CentralDisplayPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		CTXT.transform.Find ("Text").GetComponent<Text> ().text = txt;
		
	}


	private string RGBToHexa(Color _Color)
	{

		float _r = _Color.r * 255;
		float _g = _Color.g * 255;
		float _b = _Color.b * 255;

		int _R = (int)_r;
		int _G = (int)_g;
		int _B = (int)_b;


		string _hexcode = "#" + _R.ToString ("X") + _G.ToString("X") + _B.ToString("X") + "FF";
		return _hexcode;

	}





}
