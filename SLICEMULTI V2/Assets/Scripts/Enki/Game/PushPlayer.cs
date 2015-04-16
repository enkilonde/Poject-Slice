using UnityEngine;
using System.Collections;

public class PushPlayer : MonoBehaviour 
{


	private PhotonView _PhotonView;

	// Use this for initialization
	void Start () 
	{
		_PhotonView = transform.GetComponent<PhotonView>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Input.GetMouseButtonDown (1) && _PhotonView.isMine) 
		{
			Push();
		}
	
	}


	void Push()
	{
		RaycastHit _hit;
		LayerMask _layer = LayerMask.GetMask ("Player");
		if (Physics.Raycast(transform.position, transform.Find("Camera").Find("Viseur").position - transform.position, out _hit, 100, _layer.value))
		{
			Vector3 _dirPush = transform.Find("Camera").Find("Viseur").position - transform.position;
			//_hit.rigidbody.AddForce(_dirPush * 100);
			//_PhotonView.RPC("Pushed", PhotonTargets.All, _dirPush); 
			_hit.transform.GetComponent<PhotonView>().RPC("Pushed", PhotonTargets.All, _dirPush.normalized); 
		}


	}

	[RPC]
	void Pushed(Vector3 _dirpush)
	{
		if (_PhotonView.isMine) 
		{

			transform.GetComponent<Rigidbody>().AddForce (_dirpush * 100, ForceMode.VelocityChange);


		}
	

	}


}
