using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class NetworkCharacter : MonoBehaviour {


	public enum PlayerColor{Yellow, Red, Green, Purple, None};

	public PlayerColor _PlayerColor = PlayerColor.None;

	private PhotonView _PhotonView;

	public string _PlayerName;

	private Vector3 _position;
	private Quaternion _rotation;
	public float _LimiteTP = 5;
	public float _Smoothing = 1;

	private float _TPAmount = 0;
	private float _TPLim = 0;

	public bool _IsMouse = false;
	
	private bool _NoColor = true;
	
	public bool _Yellow = false;
	public bool _Red = false;
	public bool _Purple = false;
	public bool _Green = false;

	public Color _plcolor = new Color();


	public int _MouseScoreMultiplier = 1;
	public int _BaseScorePerSecond = 1;

	public int _Score = 0;
	private ScoreManager _Scrmanager;
	public float _FrequenceScore = 0.5f;


	public GameObject _AuraContainer;


	public List<GameObject> _Players = new List<GameObject>();

	// Use this for initialization
	void Start () {

		_PhotonView = GetComponent<PhotonView> ();

		//SetColor ();
		_AuraContainer = transform.Find ("Aura").gameObject;
		_AuraContainer.SetActive (false);

		if (_PhotonView.isMine == true) 
		{
			GetComponent<move> ().enabled = true;
			GetComponent<MouseLook> ().enabled = true;
			GetComponent<CreatePortal> ().enabled = true;
			transform.Find ("Camera").GetComponent<Camera> ().enabled = true;
			transform.Find ("CameraUI").GetComponent<Camera> ().enabled = true;
			transform.Find ("Camera").GetComponent<MouseLook> ().enabled = true;
			transform.Find ("FootCollider").gameObject.SetActive (true);
			transform.Find ("Scripts").gameObject.SetActive (true);
			transform.Find("PlayerName").gameObject.SetActive(false);
			//transform.Find("CharacterMesh").gameObject.SetActive(false);






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

			switch(_PlayerColor)
			{
			case NetworkCharacter.PlayerColor.Green : _PhotonView.RPC("ColorGreen", PhotonTargets.AllBuffered); break;
			case NetworkCharacter.PlayerColor.Yellow : _PhotonView.RPC("ColorYellow", PhotonTargets.AllBuffered); break;
			case NetworkCharacter.PlayerColor.Red : _PhotonView.RPC("ColorRed", PhotonTargets.AllBuffered); break;
			case NetworkCharacter.PlayerColor.Purple : _PhotonView.RPC("ColorPurple", PhotonTargets.AllBuffered); break;
			default : break;
			}
			
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
		_Green = false;
		_Yellow = false;
		_Red = false;
		_Purple = false;


		_Players = new List<GameObject> ();
		GameObject[] _g = GameObject.FindGameObjectsWithTag ("Player");
		foreach(GameObject _gg in _g)
		{
			_Players.Add(_gg);
			if(_gg.GetComponent<PhotonView>().isMine)
			{
				switch(_PlayerColor)
				{
				case NetworkCharacter.PlayerColor.Green : _PhotonView.RPC("ColorGreen", PhotonTargets.AllBuffered); break;
				case NetworkCharacter.PlayerColor.Yellow : _PhotonView.RPC("ColorYellow", PhotonTargets.AllBuffered); break;
				case NetworkCharacter.PlayerColor.Red : _PhotonView.RPC("ColorRed", PhotonTargets.AllBuffered); break;
				case NetworkCharacter.PlayerColor.Purple : _PhotonView.RPC("ColorPurple", PhotonTargets.AllBuffered); break;
				default : break;
				}
			}


			switch(_gg.GetComponent<NetworkCharacter>()._PlayerColor)
			{
			case NetworkCharacter.PlayerColor.Green : _Green = true; break;
			case NetworkCharacter.PlayerColor.Yellow : _Yellow = true; break;
			case NetworkCharacter.PlayerColor.Red : _Red = true; break;
			case NetworkCharacter.PlayerColor.Purple : _Purple = true; break;
				default : break;
			}
			
		}

		if (_PlayerColor == PlayerColor.None && GetComponent<PhotonView>().isMine) 
		{
			SetColor();
		}

		if (_NoColor && _PlayerColor != PlayerColor.None) 
		{
			_NoColor = false;
			if (_PlayerColor == PlayerColor.Green)
			{
				_plcolor.r = 191.0f / 255.0f;
				_plcolor.g = 83.0f / 255.0f;
				_plcolor.b = 73.0f / 255.0f;
			}
			else if (_PlayerColor == PlayerColor.Purple)
			{
				_plcolor.r = 191.0f / 255.0f;
				_plcolor.g = 122.0f / 255.0f;
				_plcolor.b = 200.0f / 255.0f;
				
			}else if(_PlayerColor == PlayerColor.Red)
			{
				_plcolor.r = 255.0f / 255.0f;
				_plcolor.g = 0.0f / 255.0f;
				_plcolor.b = 0.0f / 255.0f;
				
			}
			else if(_PlayerColor == PlayerColor.Yellow)
			{
				_plcolor.r = 149.0f / 255.0f;
				_plcolor.g = 147.0f / 255.0f;
				_plcolor.b = 93.0f / 255.0f;
			}
			_plcolor.a = 1;
			transform.Find("Character Mesh").Find("MainBody").GetComponent<Renderer>().material.color = _plcolor;
			
			
			
			_AuraContainer.transform.Find ("AuraUp").GetComponent<ParticleSystem> ().startColor = _plcolor;
			_AuraContainer.transform.Find ("AuraGround").GetComponent<ParticleSystem> ().startColor = _plcolor;
		}

	}


	public void SetColor ()
	{

		//UpdatePlayerList2 ();

		if (!_Green) 
		{
			_PlayerColor = PlayerColor.Green;
		}

		if (!_Yellow) 
		{
			_PlayerColor = PlayerColor.Yellow;
		}

		if (!_Red) 
		{
			_PlayerColor = PlayerColor.Red;
		}

		if (!_Purple) 
		{
			_PlayerColor = PlayerColor.Purple;
		}
		


		GetComponent<PhotonView>().RPC("SynchroniseColor", PhotonTargets.AllBuffered, _PlayerColor.ToString());



	}

	void OnPhotonPlayerConnected(PhotonPlayer player)
	{

		//GetComponent<PhotonView>().RPC("SynchroniseColor", PhotonTargets.All, _PlayerColor.ToString());
		//UpdatePlayerList2 ();
	}


	[RPC]
	public void SynchroniseColor(string _col)
	{
		_PlayerColor = (PlayerColor) System.Enum.Parse (typeof(PlayerColor), _col);

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
	public void ColorYellow()
	{
		_PlayerColor = PlayerColor.Yellow;
	}


	[RPC]
	public void ColorGreen()
	{
		_PlayerColor = PlayerColor.Green;
	}

	[RPC]
	public void ColorRed()
	{
		_PlayerColor = PlayerColor.Red;
	}

	[RPC]
	public void ColorPurple()
	{
		_PlayerColor = PlayerColor.Purple;
	}



}
