using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatePortal : MonoBehaviour {



	public GameObject _FakePortalPrefab;

	public float _PortalSize = 1;
	
	private float _Charge = 1;

	private Vector3 _PortalPos = Vector3.zero;
	private Quaternion _PortalRot = Quaternion.identity;

	private bool _portalPossible = false;

	private EnergyManager _EnergyManager;

	private LocalManager _LManager;



	void Awake()
	{
		_LManager = GameObject.Find ("Scripts").GetComponent<LocalManager> ();
	}

	// Use this for initialization
	void Start () {
	
		_EnergyManager = transform.Find ("Scripts").GetComponent<EnergyManager> ();


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

	}



	private void GetPortalPos()
	{

		RaycastHit _hit;
		LayerMask _layer = LayerMask.GetMask ("Ground", "Portal", "Ignored 1", "Ignored 2", "Ignored 3", "Ignored 4", "Ignored 5", "Ignored 6", "Ignored 7", "Ignored 8", "Ignored 9", "Ignored 10");
		Ray _Ray = new Ray (transform.position, transform.Find ("Camera").Find ("Viseur").position - transform.position);
		if (Physics.Raycast(transform.position, transform.Find("Camera").Find("Viseur").position - transform.position, out _hit, Mathf.Infinity, _layer.value))
		{
			if (_hit.transform.tag == "Ground" || _hit.transform.tag == "Portaillable" || _hit.transform.tag == "SautEtPortaillable")
			{
				_PortalPos = _hit.point;
				_PortalRot = _hit.transform.rotation;
				_portalPossible = true;
			}
			else if (_hit.transform.tag == "Portal")
			{
				/*RaycastHit _hit2;
				Vector3 _addedVector = _hit.point - transform.position;
				if(Physics.Raycast(_hit.point + (_addedVector.normalized), _hit.point + _hit.point - transform.position  , out _hit2, _layer.value))
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
