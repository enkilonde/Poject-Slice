using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PortalLayerManager : MonoBehaviour {
	
	public List<Portal> _Portals = new List<Portal>();
	public float _margeD_erreur = 0.01f;

	// Use this for initialization
	void Start () {

		
	}
	
	// Update is called once per frame
	void Update () 
	{

		bool _test = true;
		while (_test) 
		{
			List<int> indexesToRemove = new List<int> ();
			for (int i = 0; i < _Portals.Count; i++) 
			{
				if (!_Portals[i]._PortalGObject)
					indexesToRemove.Add(i);
				break;
			}
			
			foreach(int r in indexesToRemove)
			{
				_Portals.RemoveAt(r);
				print("ttt");
			}
			if (indexesToRemove.Count == 0)
			{
				_test = false;
			}
		}


	}


	public void removePortal(Vector3 _GoPos, int _layer)
	{
		print ("Debug Remove Portal      " + _GoPos);
		int _PortalIndex = 0;

		for (int i = 0; i < _Portals.Count; i++) 
		{
			if (_Portals[i]._PortalGObject.transform.position == _GoPos)
				_PortalIndex = i;
		}

		_Portals.RemoveAt (_PortalIndex);

	}


	public int CheckLayer(int _laye)
	{

		int _layer = _laye;
		int oldLayer = 0;

		while (oldLayer != _layer) 
		{
			oldLayer = _layer;
			foreach (Portal _po in _Portals) 
			{
				
				if (_po._Layer == _layer)
				{
					_layer++;
					break;
				}
				
				
			}
		}


		return _layer;
	}


	public int CheckIfOtherPortal(Vector3 pos, int _laye)
	{


		foreach (Portal _po in _Portals) 
		{
			if (_po._PortalGObject)
			{
				if (((_po._PortalGObject.transform.position.x <= pos.x + _margeD_erreur) && (_po._PortalGObject.transform.position.x >= pos.x - _margeD_erreur)) || ((_po._PortalGObject.transform.position.y <= pos.y + _margeD_erreur) && (_po._PortalGObject.transform.position.y >= pos.y - _margeD_erreur)) || ((_po._PortalGObject.transform.position.z <= pos.z + _margeD_erreur) && (_po._PortalGObject.transform.position.z >= pos.z - _margeD_erreur)))
				{
					/*
					if (_po._PortalGObject.transform.position != pos && _po._Layer == _laye)
					{
						return _po._Layer;
					}*/




				}
			}

		}
		return _laye;
	}


	public bool CheckIfOtherPortalBool(Vector3 pos, Vector3 wallPos)
	{

		GameObject[] _PortalsList = GameObject.FindGameObjectsWithTag ("Portal");
		
		foreach (GameObject _po in _PortalsList) 
		{
			Vector3 _PoPos = _po.transform.position;
			if (_po)
			{
				if (((_PoPos.x <= pos.x + _margeD_erreur) && (_PoPos.x >= pos.x - _margeD_erreur)) || ((_PoPos.y <= pos.y + _margeD_erreur) && (_PoPos.y >= pos.y - _margeD_erreur)) || ((_PoPos.z <= pos.z + _margeD_erreur) && (_PoPos.z >= pos.z - _margeD_erreur)))
				{
					//print("WallPos   " + wallPos);
					//print("WallPos2   " + _po.GetComponent<ignoreCollision>().twoWalls[0].transform.position);
					if (_po.transform.position != pos && ( _po.GetComponent<ignoreCollision>().twoWalls[0].transform.position ==  wallPos))
					{
						return true;
						
					}
				}
			}

		}
		return false;
	}


	public void RegisterPortal(int _Lay, GameObject _gameobj)
	{
		_Portals.Add (new Portal(_Lay, _gameobj));

	}

	[Serializable]
	public class Portal
	{
		public GameObject _PortalGObject;
		public int _Layer;
		public Portal(int PortalLayer, GameObject PortalGameObject)
		{
			_Layer = PortalLayer;
			_PortalGObject = PortalGameObject;
		}
	}



}





