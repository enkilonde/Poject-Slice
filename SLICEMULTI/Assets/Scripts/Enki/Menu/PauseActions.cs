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

	private List<string> _Resolutions1 = new List<string>();
	private List<string> _Resolutions2 = new List<string>();


	void Awake()
	{

		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();
		_MainPauseContainer = transform.parent.Find ("Main").gameObject;
		_OptionsContainer = transform.parent.Find("Options").gameObject;

	}

	// Use this for initialization
	void Start () 
	{

		_Resolutions1.Add ("1920");
		_Resolutions1.Add ("1080");


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

			_OptionsContainer.transform.Find("Resolution").Find("Text").GetComponent<Text>().text = "Resolution : " + Screen.resolutions;
			print("Resolution : " + Screen.resolutions);


			if (Input.GetKeyDown(KeyCode.Escape))
			{
				_Options = false;
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
		_Options = true;

	}

	public void BackFromOption()
	{
		_Options = false;
	}








	IEnumerator ResumeGame()
	{
		yield return 0;
		_LManager._IsPaused = false;
	}


}
