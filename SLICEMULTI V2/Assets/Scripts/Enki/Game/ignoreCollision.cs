using UnityEngine;
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

				for (int i = 0; i< twoWalls.Count; i++) 
				{
					Physics.IgnoreCollision (_coll.GetComponent<Collider> (), twoWalls [i].GetComponent<Collider> (), false);
				}

				//_coll.transform.position.y < transform.position.y+1 && transform.rotation.eulerAngles.y == 270 
				if ((transform.rotation.eulerAngles.x <1 && transform.rotation.eulerAngles.x > -1) && (transform.rotation.eulerAngles.z < 1 && transform.rotation.eulerAngles.z > -1))
				{
					//_OnlineManager.Fall(_coll.gameObject, GetComponent<PortalIdentifier>()._Owner, _coll.transform.GetComponent<PlayerState>()._IsMouse);
					print(_coll.transform.GetComponent<NetworkCharacter>()._IsMouse);
					Destroy(this.gameObject);
					Fall(_coll.gameObject, _OnlineManager.GetPlayerByID(GetComponent<PortalIdentifier>()._PlayerID), _coll.transform.GetComponent<NetworkCharacter>()._IsMouse);
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

	public void Fall(GameObject _Killed, GameObject _Killer, bool IsMouse)
	{
		
		int _KilledID = _Killed.GetComponent<PhotonView> ().ownerId;
		int _KillerID = _Killer.GetComponent<PhotonView> ().ownerId;
		
		if (_KilledID != _KillerID) 
		{
			if (IsMouse)
			{
				print("SWAP MOUSE");
				_Killed.GetComponent<NetworkCharacter>().GetComponent<PhotonView>().RPC("SwapMouse", PhotonTargets.All, false);
				_Killer.GetComponent<NetworkCharacter>().GetComponent<PhotonView>().RPC("SwapMouse", PhotonTargets.All, true);
			}
			
		} else 
		{
			print("YOU KILLED YOURSELF, NOOB");
		}
	}

}
