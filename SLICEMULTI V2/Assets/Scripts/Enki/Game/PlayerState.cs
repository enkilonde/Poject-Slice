using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour 
{

	public bool _IsMouse = false;
	public PhotonView _PhotonView;
	private LocalManager _LManager;


	// Use this for initialization
	void Awake () 
	{

		_PhotonView = GetComponent<PhotonView> ();
		if (_PhotonView.isMine) 
		{
			_LManager = GameObject.Find("Manager").GetComponent<LocalManager>();

		}

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (_PhotonView.isMine) 
		{

			_PhotonView.RPC("SetMouseState", PhotonTargets.All, _IsMouse);

		}
	}

	[RPC]
	public void SetMouseState(bool _State)
	{

		_IsMouse = _State;

	}
}
