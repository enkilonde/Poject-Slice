﻿using UnityEngine;
using System.Collections;

public class LocalManager : MonoBehaviour 
{

	public bool _IsPaused = false;

	public float _MasterVolume = 5;
	public float _Sensibility = 50;

	public int _ResolutionHeight = 1280;
	public int _ResolutionLenght = 1080;

	public GameObject _PauseUI;


	void Awake()
	{



	}
	// Use this for initialization
	void Start () 
	{
		Cursor.visible = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyUp(KeyCode.Escape)) 
		{
			_IsPaused = !_IsPaused;
		}

		_PauseUI.SetActive (_IsPaused);
		Cursor.visible = _IsPaused;
		
	}
}
