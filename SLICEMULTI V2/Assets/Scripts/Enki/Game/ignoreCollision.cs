﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ignoreCollision : MonoBehaviour 
{

	public List<GameObject> twoWalls = new List<GameObject>();

	public static bool _InWall = false;
	public int _layer;

	private PhotonView MyPhotonView;

	private bool _Protection = false;
	public bool _ColliProt = false;

	public bool _WallActivated = false;
	public int _CheckNumberPortal = 0;

	private OnlineManager _OnlineManager;

	// Use this for initialization
	void Start () 
	{

		_OnlineManager = GameObject.Find ("OnlineManager").GetComponent<OnlineManager> ();
		_layer = GetComponent<PortalLayer> ()._Layer + 10;

		Collider[] colli = Physics.OverlapSphere (transform.position, 0.1f);

		foreach (Collider obj in colli) 
		{
			if (obj.tag == "Ground")
			{
				twoWalls.Add(obj.gameObject);
				obj.gameObject.layer = _layer;
				obj.gameObject.GetComponent<Renderer>().material.renderQueue = 2020;
			}
		}

	}


	void Update()
	{

	}


	void LateUpdate()
	{

		if (_WallActivated) 
		{
			SetWallQueue (2020);
		} else 
		{
			SetWallQueue (2000);
		}


	}



	public void restoreLayer(int _lay)
	{

		foreach (GameObject _obj in twoWalls) 
		{
			_obj.layer = _lay;
			//_obj.gameObject.GetComponent<Renderer>().material.renderQueue = 2000;
		}

	}



	public void SetWallQueue(int _Queue)
	{

		foreach (GameObject _obj in twoWalls) 
		{
			_obj.gameObject.GetComponent<Renderer>().material.renderQueue = _Queue;
		}

	}

	void OnTriggerEnter(Collider _coll)
	{




		if (_coll.tag == "Player" && !_Protection) 
		{
			//StartCoroutine(ReverseProtection(true));
			_Protection = true;
			if (!_coll.GetComponent<IsPlayerInPortal>()._InPortal)
			{
				_coll.GetComponent<IsPlayerInPortal>()._InPortal = true;

				for (int i = 0; i< twoWalls.Count; i++)
				{
					Physics.IgnoreCollision(_coll.GetComponent<Collider>(), twoWalls[i].GetComponent<Collider>(), true);
				}
			}

			

			
		}



	}


	void OnTriggerExit(Collider _coll)
	{
		//transform.Find("CollsionProtection").GetComponent<Collider>().bounds.Contains
		if (_coll.tag == "Player" && !GetComponent<Collider> ().bounds.Intersects (_coll.GetComponent<Collider>().bounds) && _Protection) 
		{
			//StartCoroutine(ReverseProtection(false));
			_Protection = false;
			if (_coll.GetComponent<IsPlayerInPortal>()._InPortal)
			{
				_coll.GetComponent<IsPlayerInPortal>()._InPortal = false;


				if (_coll.transform.position.y < transform.position.y && transform.rotation.eulerAngles.y == 270)
				{
					_OnlineManager.Fall(_coll.transform.GetComponent<PhotonView>().ownerId, GetComponent<PortalIdentifier>()._PlayerID);
				}

				for (int i = 0; i< twoWalls.Count; i++) 
				{
					Physics.IgnoreCollision (_coll.GetComponent<Collider> (), twoWalls [i].GetComponent<Collider> (), false);
				}

			}

		}

	}

	


	IEnumerator ReverseProtection(bool _prot)
	{
		yield return null;
		yield return null;
		_Protection = _prot;
		print("protection : " + _Protection);
	}



}
