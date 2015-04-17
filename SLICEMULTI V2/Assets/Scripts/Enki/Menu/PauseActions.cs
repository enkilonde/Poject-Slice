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
	public GameObject _PrefabResolution;


	void Awake()
	{
		_Resolutions = Screen.resolutions;
		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();
		_MainPauseContainer = transform.parent.Find ("Main").gameObject;
		_OptionsContainer = transform.parent.Find("Options").gameObject;
		_ResolutionList = transform.parent.Find ("Options").Find ("Deroulant").Find ("Resolutions").gameObject;
		_ResolutionList.SetActive (false);

	}

	// Use this for initialization
	void Start () 
	{

		for (int i = 0; i < _Resolutions.Length ; i++)
		{
			GameObject _Reso = Instantiate(_PrefabResolution, _ResolutionList.transform.position - new Vector3(0, i*28, 0), Quaternion.identity) as GameObject;
			_Reso.transform.parent = _ResolutionList.transform;
			_Reso.transform.Find("Text").GetComponent<Text>().text = _Resolutions[i].width + "x" + _Resolutions[i].height;
			_Reso.GetComponent<ResolutionIndex>()._ResolutionIndex = i;
			_Reso.GetComponent<Button>().onClick.AddListener(delegate { ChangeResolution(i-1); });
		}


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

			//_OptionsContainer.transform.Find("Resolution").Find("Text").GetComponent<Text>().text = "Resolution : " + _LManager._resolutions[_ResolutionIndex].width + "x" + _LManager._resolutions[_ResolutionIndex].height;
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


	public void ChangeResolution(int _ResoIndex)
	{
		print ("Index = " + _ResoIndex);
		_ResolutionIndex = _ResoIndex;
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



	IEnumerator ResumeGame()
	{
		yield return 0;
		_LManager._IsPaused = false;
	}


}
