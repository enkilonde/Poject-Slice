using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour 
{

	public bool _IsMouse = false;
	private PhotonView _PhotonView;
	private LocalManager _LManager;


	// Use this for initialization
	void Awake () 
	{

		_PhotonView = GetComponent<PhotonView> ();
		if (_PhotonView.isMine) 
		{
			print("ttt");
			_LManager = GameObject.Find("Manager").GetComponent<LocalManager>();

		}

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (_PhotonView.isMine) 
		{

			_IsMouse = _LManager._IsMouse;

		}


	}

	public void SetMouseState(bool _State)
	{

		_LManager._IsMouse = _State;

	}
}
