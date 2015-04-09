using UnityEngine;
using System.Collections;

public class PortalLayer : MonoBehaviour {

	public int _Layer = 1;
	public GameObject _CameraPrefab;
	public GameObject _camera;
	private GameObject _LocalPlayer;

	public LayerMask _LM;

	public float _FOVLimite = 60;

	private PortalLayerManager _manager;

	// Use this for initialization
	void Start () {
		//GetComponent<PhotonView> ().RPC ("InstantiateCamera", PhotonTargets.All);

		_manager = GameObject.Find ("Scripts").GetComponent<PortalLayerManager> ();
		
		if (GameObject.Find ("Scripts").GetComponent<RandomMatchmaker> ()._Player != null) {
			_LocalPlayer = GameObject.Find ("Scripts").GetComponent<RandomMatchmaker> ()._Player;
			_camera = Instantiate (_CameraPrefab, _LocalPlayer.transform.position + new Vector3(0, 0, 0), _LocalPlayer.transform.rotation) as GameObject;
			_camera.transform.parent = _LocalPlayer.transform;

			RenderTexture _Textu;
			_Textu = new RenderTexture(500, 500, 16);
			_camera.GetComponent<Camera>().targetTexture = _Textu;
			transform.Find("PortalTexture").GetComponent<Renderer>().material.mainTexture = _Textu;

			_Layer = _manager.CheckLayer(_Layer);

			_Layer = _manager.CheckIfOtherPortal(transform.position, _Layer);
			
			_manager.RegisterPortal(_Layer, this.gameObject);



			_LM ^=(1<<_Layer);
			_camera.GetComponent<Camera>().cullingMask = _LM;


			GetComponent<ignoreCollision>().restoreLayer(_Layer);
			//this.gameObject.layer = _Layer;
			//MoveToLayer(transform, _Layer);
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (transform.Find ("Target")) 
		{

			_camera.transform.LookAt (transform.Find ("Target").position);

		}

		_camera.GetComponent<Camera> ().fieldOfView = GetFOV (_camera.transform.position, transform.Find ("PortalExtremity").position, transform.Find ("PortalExtremity2").position);




		//print (GetFOV (_camera.transform.position, transform.Find ("PortalExtremity").position, transform.Find ("PortalExtremity2").position));
	}



	private float GetFOV(Vector3 _PlayerPos, Vector3 _portalHeight, Vector3 _PortalHeight2)
	{
		
		Vector3 _player_down = _PortalHeight2 - _PlayerPos;
		Vector3 _player_extremity = _portalHeight - _PlayerPos;

		float angle = Vector3.Angle (_player_down, _player_extremity);
		if (angle > _FOVLimite) 
		{
			angle = _FOVLimite;
		}

		return angle;
	}



	void MoveToLayer(Transform root, int layer) {
		root.gameObject.layer = layer;
		foreach(Transform child in root)
			MoveToLayer(child, layer);
	}



}
