using UnityEngine;
using System.Collections;

public class PortalLayer : MonoBehaviour {

	public int _Layer = 1;
	public GameObject _CameraPrefab;
	public GameObject _camera;
	public GameObject _LocalPlayer;


	private Transform _PlayerMainCamera;

	public LayerMask _LM;

	public float _FOVLimite = 60;

	private PortalLayerManager _manager;

	public Color _FireColor;

	// Use this for initialization
	void Start () {


		_manager = GameObject.Find ("Scripts").GetComponent<PortalLayerManager> ();
		
		if (GameObject.Find ("Scripts").GetComponent<RandomMatchmaker> ()._Player != null) {

			_LocalPlayer = GameObject.Find ("Scripts").GetComponent<RandomMatchmaker> ()._Player;


		} else 
		{

			GameObject[] _players = GameObject.FindGameObjectsWithTag("Player");
			foreach(GameObject _pl in _players)
			{
				if (_pl.GetComponent<PhotonView>().isMine)
				{
					_LocalPlayer = _pl;
				}
			}

		}


		_Layer = _manager.CheckLayer (_Layer);
		
		_Layer = _manager.CheckIfOtherPortal (transform.position, _Layer);
		
		_manager.RegisterPortal (_Layer, this.gameObject);
		
		_LM ^= (1 << _Layer);
		
		//GetComponent<ignoreCollision> ().restoreLayer (_Layer);
		//MoveToLayer (transform, _Layer);
		//gameObject.layer = 10;

		if (GetComponent<PhotonView> ().isMine) 
		{
			GetComponent<PhotonView> ().RPC ("SetFireColor", PhotonTargets.All);
		}

		//transform.Find ("FireParticle").GetComponent<ParticleSystem> ().startColor = _FireColor;
	}
	
	void Update () 
	{

		RaycastHit _hit;



		if (Vector3.Distance (_LocalPlayer.transform.Find ("Camera").position, transform.position) > 1) 
		{
			if (Physics.Raycast (_LocalPlayer.transform.Find ("Camera").position, transform.position - _LocalPlayer.transform.Find ("Camera").position, out _hit)) {
				if (_hit.transform.gameObject == this.gameObject) 
				{
					//GetComponent<ignoreCollision> ().SetWallQueue (2020);
					GetComponent<ignoreCollision> ()._WallActivated = true;
				} else 
				{
					//print(GetComponent<ignoreCollision>().twoWalls[0].transform.position);
					if (!_manager.CheckIfOtherPortalBool(transform.position, GetComponent<ignoreCollision>().twoWalls[0].transform.position))
					{
						GetComponent<ignoreCollision> ()._WallActivated = false;
					}
				}
			}
		} else 
		{
			//GetComponent<ignoreCollision> ().SetWallQueue (2020);
		}
	}


	void LateUpdate()
	{
	}


	void MoveToLayer(Transform root, int layer) {
		root.gameObject.layer = layer;
		foreach(Transform child in root)
			MoveToLayer(child, layer);
	}



	[RPC]
	public void SetFireColor()
	{

		transform.Find ("FireParticle").GetComponent<ParticleSystem> ().startColor = _FireColor;
		transform.Find ("FireParticle 1").GetComponent<ParticleSystem> ().startColor = _FireColor;
		//transform.Find ("FireParticle").GetComponent<ParticleSystem> ().Play ();
	}



}
