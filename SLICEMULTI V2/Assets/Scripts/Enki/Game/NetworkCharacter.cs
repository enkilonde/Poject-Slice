using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class NetworkCharacter : MonoBehaviour {

	private PhotonView _PhotonView;

	public string _PlayerName;

	private Vector3 _position;
	private Quaternion _rotation;
	public float _LimiteTP = 5;
	public float _Smoothing = 1;

	private float _TPAmount = 0;
	private float _TPLim = 0;

	public bool _IsMouse = false;

	public Material _NonMouse;
	public Material _Mouse;
	public int _MouseScoreMultiplier = 1;
	public int _BaseScorePerSecond = 1;

	public int _Score = 0;
	private ScoreManager _Scrmanager;
	public float _FrequenceScore = 0.5f;

	// Use this for initialization
	void Start () {

		_PhotonView = GetComponent<PhotonView> ();

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

		} else 
		{


		}
		_Scrmanager = GetComponent<ScoreManager>();
		StartCoroutine (ScoreIncrement());
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

		if (_PhotonView.isMine == false) 
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



	
	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		transform.Find ("PlayerName").Find ("Text").GetComponent<Text> ().text = _PlayerName;
	}


	[RPC]
	public void SetPlayerName(string _name)
	{
		transform.Find("PlayerName").Find("Text").GetComponent<Text>().text = _name;
		print ("set name");

	}




}
