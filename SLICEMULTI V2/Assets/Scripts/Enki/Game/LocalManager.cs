﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalManager : MonoBehaviour 
{

	public bool _IsPaused = false;

	public float _MasterVolume = 5;
	public float _Sensibility = 50;

	public GameObject _PauseUI;

	public static LocalManager _Manager;

	public Resolution[] _resolutions;
	public int _ResolutionIndex = 0;

	public int _PortalNumber = 0;



	void Awake()
	{

		DontDestroyOnLoad (this.gameObject);

		if (_Manager == null) 
		{
				_Manager = this;
		} else 
		{
			Destroy(this.gameObject);
		}


	}
	// Use this for initialization
	void Start () 
	{
		Cursor.visible = false;

		_resolutions = Screen.resolutions;
		_ResolutionIndex = _resolutions.Length - 1;
		Screen.SetResolution(_resolutions[_ResolutionIndex].width, _resolutions[_ResolutionIndex].height, true);
		
		
		
	}


	void OnLevelWasLoaded(int level) 
	{

		Cursor.visible = false;
		
		_resolutions = Screen.resolutions;
		_ResolutionIndex = _resolutions.Length - 1;
		Screen.SetResolution(_resolutions[_ResolutionIndex].width, _resolutions[_ResolutionIndex].height, true);

		switch (level) 
		{
		case 0 : _PauseUI = GameObject.Find("Menu"); break;
		case 1 : _PauseUI = GameObject.Find("Menu"); break;
		case 2 : _PauseUI = GameObject.Find("Menu"); break;
		case 3 : _PauseUI = GameObject.Find("Menu"); break;
		default : _PauseUI = GameObject.Find("Menu"); break;
		}

		
	}
	
	// Update is called once per frame
	void Update () 
	{

		if (Application.loadedLevel == 0) 
		{
			_IsPaused = true;
		}

		if (Input.GetKeyUp(KeyCode.Escape) && Application.loadedLevel !=0) 
		{
			_IsPaused = !_IsPaused;
		}

		_PauseUI.SetActive (_IsPaused);
		Cursor.visible = _IsPaused;
		
	}


	public void UpdateResolution(int _index)
	{
		_ResolutionIndex = _index;
		Screen.SetResolution(_resolutions[_ResolutionIndex].width, _resolutions[_ResolutionIndex].height, true);

	}



}
