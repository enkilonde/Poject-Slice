using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PauseActions : MonoBehaviour 
{

	public bool _Options = false;
	private float _Volume;

	private LocalManager _LManager;

	private GameObject _MainPauseContainer;
	private GameObject _OptionsContainer;



	private int _ResolutionIndex = 0;


	private GameObject _ResolutionList;
	private Resolution[] _Resolutions;


	void Awake()
	{
		_Resolutions = Screen.resolutions;
		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();
		_MainPauseContainer = transform.parent.Find ("Main").gameObject;
		_OptionsContainer = transform.parent.Find("Options").gameObject;
		_ResolutionList = transform.parent.Find ("Options").Find ("Deroulant").Find ("Resolutions").gameObject;


	}

	// Use this for initialization
	void Start () 
	{



	}
	
	// Update is called once per frame
	void Update () 
	{

		_OptionsContainer.SetActive (_Options);
		_MainPauseContainer.SetActive (!_Options);

		if (_Options) 
		{
			_LManager._MasterVolume = _OptionsContainer.transform.Find("VolumeSlider").GetComponent<Slider>().value;
			_OptionsContainer.transform.Find("Volume").Find("Text").GetComponent<Text>().text = "Volume : " + _LManager._MasterVolume + "%";

			_LManager._Sensibility = _OptionsContainer.transform.Find ("SensibilitySlider").GetComponent<Slider> ().value / 100;
			_OptionsContainer.transform.Find("Sensibility").Find("Text").GetComponent<Text>().text = "Sensibilité : " + _LManager._Sensibility * 100 + "%";

			_OptionsContainer.transform.Find("Resolution").Find("Text").GetComponent<Text>().text = "Resolution : " + _LManager._resolutions[_ResolutionIndex].width + "x" + _LManager._resolutions[_ResolutionIndex].height;
			//print("Resolution : " + _LManager._resolutions[_LManager._ResolutionIndex] + "x" + _LManager._resolutions[_LManager._ResolutionIndex]);


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


		}


	}


	public void Quit()
	{
		PhotonNetwork.Disconnect ();
		Cursor.visible = true;
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


	public void ChangeResolution()
	{

		_ResolutionIndex++;
		if (_ResolutionIndex >= _LManager._resolutions.Length) 
		{
			_ResolutionIndex = 0;
		}
	}

	public void ApplyResolution()
	{

		_LManager.UpdateResolution (_ResolutionIndex);

	}


	public void DeroulerResolutions(GameObject _Prefab)
	{


		for (int i = 0; i < _Resolutions.Length ; i++)
		{



		}


		_ResolutionList.SetActive (!_ResolutionList.activeSelf);

	}



	IEnumerator ResumeGame()
	{
		yield return 0;
		_LManager._IsPaused = false;
	}


}
