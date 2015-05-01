using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseActions : MonoBehaviour 
{

	public bool _Options = false;
	public bool _RoomListVisible = false;

	public bool _RoomCreation;

	private float _Volume;

	public LocalManager _LManager;

	private GameObject _MainPauseContainer;
	private GameObject _OptionsContainer;
	private GameObject _RoomListContainer;

	private int _ResolutionIndex = 0;


	private GameObject _ResolutionList;
	private Resolution[] _Resolutions;
	public GameObject _PrefabResolution;
	public GameObject _PrefabRoom;

	private string _FSText = "FullScreen";

	void Awake()
	{
		_Resolutions = Screen.resolutions;
		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();
		_MainPauseContainer = transform.parent.Find ("Main").gameObject;
		_OptionsContainer = transform.parent.Find("Options").gameObject;
		_ResolutionList = transform.parent.Find ("Options").Find ("Deroulant").Find ("Resolutions").gameObject;
		_ResolutionList.SetActive (false);

		if (Application.loadedLevel == 0) 
		{
			_RoomListContainer = transform.parent.Find("RoomList").gameObject;
		}


	}
	
	void Start () 
	{

		for (int i = 0; i < _Resolutions.Length ; i++)
		{
			GameObject _Reso = Instantiate(_PrefabResolution, _ResolutionList.transform.position - new Vector3(0, i*28, 0), Quaternion.identity) as GameObject;
			_Reso.transform.parent = _ResolutionList.transform;
			_Reso.transform.localScale = new Vector3(1, 1, 1);
			_Reso.transform.Find("Text").GetComponent<Text>().text = _Resolutions[i].width + "x" + _Resolutions[i].height;
			_Reso.GetComponent<ResolutionIndex>()._ResolutionIndex = i;
			_Reso.GetComponent<Button>().onClick.AddListener(delegate { ChangeResolution(_Reso); });
		}


	}
	
	void Update () 
	{

		if (_LManager == null) 
		{
			_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();
		}


		if (Application.loadedLevel == 0) 
		{
			_RoomListContainer.SetActive (_RoomListVisible);
		}
		_OptionsContainer.SetActive (_Options);
		_MainPauseContainer.SetActive (!_Options && !_RoomListVisible);

		if (_Options) 
		{
			_LManager._MasterVolume = _OptionsContainer.transform.Find("VolumeSlider").GetComponent<Slider>().value;
			_OptionsContainer.transform.Find("Volume").Find("Text").GetComponent<Text>().text = "Volume : " + _LManager._MasterVolume + "%";

			_LManager._Sensibility = _OptionsContainer.transform.Find ("SensibilitySlider").GetComponent<Slider> ().value / 100;
			_OptionsContainer.transform.Find("Sensibility").Find("Text").GetComponent<Text>().text = "Sensibilité : " + _LManager._Sensibility * 100 + "%";

			_ResolutionList.transform.parent.Find("Button").Find("Text").GetComponent<Text>().text = _Resolutions[_ResolutionIndex].width + "x" + _Resolutions[_ResolutionIndex].height;

			_OptionsContainer.transform.Find("FullScreen").Find("Text").GetComponent<Text>().text = _FSText;



			if (Input.GetKeyDown(KeyCode.Escape))
			{
				_Options = false;
			}

			if(_ResolutionIndex == _LManager._ResolutionIndex)
			{
				_OptionsContainer.transform.Find("ApplyResolution").gameObject.SetActive(false);
			}
			else
			{
				_OptionsContainer.transform.Find("ApplyResolution").gameObject.SetActive(true);
			}

			if (Application.loadedLevel == 0)
			{
				_LManager._PlayerName = _OptionsContainer.transform.Find("Name").Find("Text").GetComponent<Text>().text;
			}
		}


		if (_RoomListVisible) 
		{
			_RoomListContainer.transform.Find("RoomCreation").Find("Window").gameObject.SetActive(_RoomCreation);
		}


	}

	public void LoadScene()
	{
		Application.LoadLevel (Application.loadedLevel+1);
	}

	public void ShowRoomList()
	{
		_RoomListVisible = true;

	}

	public void HideRoomList()
	{
		_RoomListVisible = false;

	}


	public void CreateRoom()
	{
		_RoomCreation = true;
	}


	
	public void QuitGame()
	{
		Application.Quit ();
	}

	public void Quit()
	{
		//PhotonNetwork.Disconnect ();
		Cursor.visible = true;
		PhotonNetwork.LeaveRoom ();
		Application.LoadLevel (0);
	}

	public void Resume()
	{

		StartCoroutine (ResumeGame ());
	}

	public void GoToOptions()
	{
		_OptionsContainer.transform.Find ("VolumeSlider").GetComponent<Slider> ().value = _LManager._MasterVolume;
		_OptionsContainer.transform.Find ("SensibilitySlider").GetComponent<Slider> ().value = _LManager._Sensibility * 100;
		_ResolutionIndex = _LManager._ResolutionIndex;
		_Options = true;

	}

	

	public void BackFromOption()
	{
		_Options = false;
	}


	public void ChangeResolution(GameObject _ResoIndex)
	{
		_ResolutionIndex = _ResoIndex.GetComponent<ResolutionIndex> ()._ResolutionIndex;;
		_ResolutionList.SetActive (false);
	}

	public void ApplyResolution()
	{

		_LManager.UpdateResolution (_ResolutionIndex);

	}


	public void DeroulerResolutions()
	{

		_ResolutionList.SetActive (!_ResolutionList.activeSelf);

	}

	public void SetFullScreen()
	{
		_LManager._Fullscreen = !_LManager._Fullscreen;

		if (_LManager._Fullscreen) 
		{
			_FSText = "Fullscreen";
		} else 
		{
			_FSText = "Windowed";
		}

		_LManager.UpdateResolution (_ResolutionIndex);
	}



	IEnumerator ResumeGame()
	{
		yield return 0;
		_LManager._IsPaused = false;
	}


}
