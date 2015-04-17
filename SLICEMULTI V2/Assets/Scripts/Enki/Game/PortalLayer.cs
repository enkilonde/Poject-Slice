using UnityEngine;
using System.Collections;

public class PortalLayer : MonoBehaviour {

	public int _Layer = 1;
	public GameObject _CameraPrefab;
	public GameObject _camera;
	private GameObject _LocalPlayer;


	private Transform _PlayerMainCamera;

	public LayerMask _LM;

	public float _FOVLimite = 60;

	private PortalLayerManager _manager;

	// Use this for initialization
	void Start () {


		_manager = GameObject.Find ("Scripts").GetComponent<PortalLayerManager> ();
		
		if (GameObject.Find ("Scripts").GetComponent<RandomMatchmaker> ()._Player != null) {

			_LocalPlayer = GameObject.Find ("Scripts").GetComponent<RandomMatchmaker> ()._Player;

			_Layer = _manager.CheckLayer(_Layer);

			_Layer = _manager.CheckIfOtherPortal(transform.position, _Layer);
			
			_manager.RegisterPortal(_Layer, this.gameObject);

			_LM ^=(1<<_Layer);

			GetComponent<ignoreCollision>().restoreLayer(_Layer);
			MoveToLayer(transform, _Layer);
			gameObject.layer = 10;

		}

	}
	
	void Update () 
	{

		RaycastHit _hit;

		if (Physics.Raycast (_LocalPlayer.transform.position, transform.position - _LocalPlayer.transform.position, out _hit)) 
		{
			if (_hit.transform.gameObject == this.gameObject)
			{
				GetComponent<ignoreCollision>().SetWallQueue(2020);
			}else
			{
				GetComponent<ignoreCollision>().SetWallQueue(2000);
			}
		}

	}





	void MoveToLayer(Transform root, int layer) {
		root.gameObject.layer = layer;
		foreach(Transform child in root)
			MoveToLayer(child, layer);
	}



}
