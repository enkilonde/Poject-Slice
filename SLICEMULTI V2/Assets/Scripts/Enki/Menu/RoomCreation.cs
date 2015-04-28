using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class RoomCreation : MonoBehaviour {

	private PauseActions _pauseAction;
	private InputField _RoomName;
	public Button _createRoomButton;
	public Button _JoinRoomButton;

	public GameObject _PrefabRoom;
	public GameObject _RoomNamesContainer;

	public string _SelectedRoom = "";

	public List<GameObject> _CurrentRooms = new List<GameObject>();

	private RoomInfo[] _Rooms;

	// Use this for initialization
	void Start () 
	{
		_pauseAction = transform.parent.parent.parent.Find ("MenuScripts").GetComponent<PauseActions> ();
		_RoomName = transform.parent.Find("Window").Find ("SetRoomName").GetComponent<InputField> ();

		_createRoomButton = transform.parent.Find("Window").Find ("CreateRoom").GetComponent<Button> ();
		_JoinRoomButton = transform.parent.parent.Find ("Join Room").GetComponent<Button> ();
		_RoomNamesContainer = transform.parent.parent.Find("RoomNamesContainer").gameObject;

		_Rooms = PhotonNetwork.GetRoomList();

		_CurrentRooms = new List<GameObject> ();
		
		for (int i = 0; i < _Rooms.Length; i++)
		{

			GameObject _room = Instantiate(_PrefabRoom, _RoomNamesContainer.transform.position - new Vector3(0, i*28, 0), Quaternion.identity) as GameObject;
			_room.transform.SetParent(transform.parent.parent.Find("RoomNamesContainer"));
			_room.transform.localScale = new Vector3(1, 1, 1);
			_room.transform.Find("Text").GetComponent<Text>().text = _Rooms[i].name;
			_room.GetComponent<Button>().onClick.AddListener(delegate { SelectRoom(_room.transform.Find("Text").GetComponent<Text>().text); });
			_CurrentRooms.Add(_room);

		}

	}
	
	// Update is called once per frame
	void Update () 
	{

		if (_RoomName.text.Length == 0) 
		{
			_createRoomButton.interactable = false;
		} else 
		{
			_createRoomButton.interactable = true;
		}

		RoomInfo[] _OldRooms = PhotonNetwork.GetRoomList();

		if (_OldRooms != _Rooms)
		{
			foreach (GameObject _go in _CurrentRooms) 
			{
				Destroy(_go);
			}
			_Rooms = PhotonNetwork.GetRoomList();

			_CurrentRooms = new List<GameObject> ();
			
			for (int i = 0; i < _Rooms.Length; i++)
			{
				print(" i = " + i);
				GameObject _room = Instantiate(_PrefabRoom, _RoomNamesContainer.transform.position - new Vector3(0, i*28, 0), Quaternion.identity) as GameObject;
				_room.transform.SetParent(transform.parent.parent.Find("RoomNamesContainer"));
				_room.transform.localScale = new Vector3(1, 1, 1);
				_room.transform.Find("Text").GetComponent<Text>().text = _Rooms[i].name;
				_CurrentRooms.Add(_room);
				_room.GetComponent<Button>().onClick.AddListener(delegate { SelectRoom(_room.transform.Find("Text").GetComponent<Text>().text); });

			}

		}



		if (_SelectedRoom.Length == 0) 
		{
			_JoinRoomButton.interactable = false;
		} else 
		{
			_JoinRoomButton.interactable = true;
		}




	}

	public void Back()
	{
		_pauseAction._RoomCreation = false;
	}

	public void CreateRoom()
	{

		PhotonNetwork.CreateRoom (_RoomName.text, true, true, 4);
		print (_RoomName.text);
		Application.LoadLevel (1);
	}

	public void SelectRoom(string _ID)
	{
		/*
		for (int i = 0; i < _CurrentRooms.Count; i++) 
		{
			if (i != _ID)
			{
				//_CurrentRooms[i].GetComponent<Button>().isOn = false;
			}
			else
			{
				_SelectedRoom = _CurrentRooms[i].name;
			}
		}*/

		_SelectedRoom = _ID;

	}


	public void JoinSelectedRoom()
	{
		PhotonNetwork.JoinRoom (_SelectedRoom);
		Application.LoadLevel (1);
	}

}
