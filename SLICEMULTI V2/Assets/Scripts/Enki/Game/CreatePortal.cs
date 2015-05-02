using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CreatePortal : MonoBehaviour {



	public GameObject _FakePortalPrefab;

	public float _PortalSize = 1;
	
	private float _Charge = 1;

	private Vector3 _PortalPos = Vector3.zero;
	private Quaternion _PortalRot = Quaternion.identity;

	private bool _portalPossible = false;

	private EnergyManager _EnergyManager;

	private LocalManager _LManager;

	private int _PLayerID;

	private OnlineManager _OManager;

	private NetworkCharacter _NCharacter;

	public Color _YellowGradient;
	public Color _RedGradient;
	public Color _PurpleGradient;
	public Color _GreenGradient;


	void Awake()
	{
		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();
		_OManager = GameObject.Find ("OnlineManager").GetComponent<OnlineManager> ();
		_NCharacter = GetComponent<NetworkCharacter> ();
	}

	// Use this for initialization
	void Start () {
	
		_EnergyManager = transform.Find ("Scripts").GetComponent<EnergyManager> ();
		_PLayerID = GetComponent<PhotonView> ().ownerId;

	}



	
	// Update is called once per frame
	void Update () 
	{
		if (!_LManager._IsPaused) 
		{

			_portalPossible = false;

			GetPortalPos ();

			if (_portalPossible) {
					MakeFakePortal ();
			}


			if (Input.GetMouseButton (0)) {
					//_Charge += Time.deltaTime;

			}


			if (Input.GetMouseButtonUp (0) && _portalPossible && _EnergyManager._NombreBarreDispo >= 1) {
					LaunchPortal (_Charge);
					_Charge = 1;

					_EnergyManager._NombreBarreDispo--;
			}

		}
			



	
	}

	public void LaunchPortal(float _Size)
	{
		GameObject _portal = PhotonNetwork.Instantiate("Portal", _PortalPos, _PortalRot, 0) as GameObject;
		_portal.GetComponent<PortalDegradation> ()._Size = _Size * _PortalSize;
		_LManager._PortalsContainer.Add (_portal);
		_portal.GetComponent<PortalIdentifier> ()._PlayerID = _PLayerID;
		_portal.GetComponent<PhotonView> ().RPC ("SetID", PhotonTargets.All, _PLayerID);
		_portal.GetComponent<PortalIdentifier> ()._OwnerIndex = _OManager._Players.IndexOf (this.gameObject);

		switch (_NCharacter._PlayerColor) 
		{
		case NetworkCharacter.PlayerColor.Green : _portal.GetComponent<PortalLayer> ()._FireColor = _GreenGradient; break;
		case NetworkCharacter.PlayerColor.Yellow : _portal.GetComponent<PortalLayer> ()._FireColor = _YellowGradient; break;
		case NetworkCharacter.PlayerColor.Red : _portal.GetComponent<PortalLayer> ()._FireColor = _RedGradient; break;
		case NetworkCharacter.PlayerColor.Purple : _portal.GetComponent<PortalLayer> ()._FireColor = _PurpleGradient; break;
		default : break;


		}



		//print (_PLayerID);
	}



	private void GetPortalPos()
	{

		RaycastHit _hit;
		LayerMask _layer = LayerMask.GetMask ("Ground", "Portal", "Ignored 1", "Ignored 2", "Ignored 3", "Ignored 4", "Ignored 5", "Ignored 6", "Ignored 7", "Ignored 8", "Ignored 9", "Ignored 10");
		if (Physics.Raycast(transform.Find ("Camera").position, transform.Find("Camera").Find("Viseur").position - transform.Find ("Camera").position, out _hit, Mathf.Infinity, _layer.value))
		{
			if (_hit.transform.tag == "Ground" || _hit.transform.tag == "Portaillable" || _hit.transform.tag == "SautEtPortaillable")
			{
				_PortalPos = _hit.point;
				_PortalRot = _hit.transform.rotation;
				_portalPossible = true;
			}
			else if (_hit.transform.tag == "Portal")
			{
				/*
				RaycastHit _hit2;
				LayerMask _layer2 = LayerMask.GetMask ("Ground", "Ignored 1", "Ignored 2", "Ignored 3", "Ignored 4", "Ignored 5", "Ignored 6", "Ignored 7", "Ignored 8", "Ignored 9", "Ignored 10");
				_layer2 ^= (1 << _hit.transform.GetChild(0).gameObject.layer);
				if (Physics.Raycast(transform.Find ("Camera").position, transform.Find("Camera").Find("Viseur").position - transform.Find ("Camera").position, out _hit2, Mathf.Infinity, _layer2.value))
				{
					if (_hit2.transform.tag == "Ground" || _hit2.transform.tag == "Portaillable" || _hit2.transform.tag == "SautEtPortaillable")
					{
						_PortalPos = _hit2.point;
						_PortalRot = _hit2.transform.rotation;
						_portalPossible = true;
					}

				}
				*/
			}
		}
	}


	private void MakeFakePortal()
	{

		GameObject Fake = Instantiate (_FakePortalPrefab, _PortalPos, _PortalRot) as GameObject;
		Fake.transform.localScale *= _Charge * _PortalSize;
		StartCoroutine (deleteOldFake(Fake));
	}

	private IEnumerator deleteOldFake(GameObject _fake)
	{
		yield return 0;
		Destroy (_fake);
	}




	[RPC]
	void InstantiateCamera()
	{

		//print (_LocalPlayer.transform.position);


	}


}
