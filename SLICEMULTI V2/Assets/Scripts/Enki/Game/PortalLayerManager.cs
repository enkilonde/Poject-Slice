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


	public bool CheckIfOtherPortalBool(Vector3 pos, int _Layer)
	{

		foreach (Portal _po in _Portals) 
		{
			if (_po._PortalGObject)
			{
				if (((_po._PortalGObject.transform.position.x <= pos.x + _margeD_erreur) && (_po._PortalGObject.transform.position.x >= pos.x - _margeD_erreur)) || ((_po._PortalGObject.transform.position.y <= pos.y + _margeD_erreur) && (_po._PortalGObject.transform.position.y >= pos.y - _margeD_erreur)) || ((_po._PortalGObject.transform.position.z <= pos.z + _margeD_erreur) && (_po._PortalGObject.transform.position.z >= pos.z - _margeD_erreur)))
				{
					if (_po._PortalGObject.transform.position != pos && _po._Layer == _Layer)
					{
						print("TRUE");
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





