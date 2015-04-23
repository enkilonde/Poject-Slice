﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkCharacter : MonoBehaviour {

	private Vector3 _position;
	private Quaternion _rotation;
	public float _LimiteTP = 5;
	public float _Smoothing = 1;

	private float _TPAmount = 0;
	private float _TPLim = 0;

	public bool _IsMouse = false;

	public Material _NonMouse;
	public Material _Mouse;


	public int _Score = 0;
	private ScoreManager _Scrmanager;

	// Use this for initialization
	void Start () {
		if (GetComponent<PhotonView> ().isMine == true) 
		{
			GetComponent<move> ().enabled = true;
			GetComponent<MouseLook> ().enabled = true;
			GetComponent<CreatePortal> ().enabled = true;
			transform.Find ("Camera").GetComponent<Camera> ().enabled = true;
			transform.Find ("CameraUI").GetComponent<Camera> ().enabled = true;
			transform.Find ("Camera").GetComponent<MouseLook> ().enabled = true;
			transform.Find ("FootCollider").gameObject.SetActive (true);
			transform.Find ("Scripts").gameObject.SetActive (true);
			_Scrmanager = GetComponent<ScoreManager>();
		} else 
		{


		}

		
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

		if (GetComponent<PhotonView> ().isMine == false) 
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


		if (GetComponent<PhotonView> ().isMine) 
		{
			_Scrmanager._LocalScore = _Score;
			

			
		}


		if (_IsMouse) 
		{
			GetComponent<Renderer> ().material = _Mouse;
		} else 
		{
			GetComponent<Renderer> ().material = _NonMouse;
		}



		if (Input.GetKeyDown(KeyCode.Space) && GetComponent<PhotonView> ().isMine)
		{
			_Score++;


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
		}
		else
		{
			// Network player, receive data
			_position = (Vector3) stream.ReceiveNext();
			_rotation = (Quaternion) stream.ReceiveNext();
			_IsMouse = (bool) stream.ReceiveNext();
			_Score = (int) stream.ReceiveNext();
		}
	}



	[RPC]
	public void SwapMouse(bool mouse)
	{

		_IsMouse = mouse;


	}



}
