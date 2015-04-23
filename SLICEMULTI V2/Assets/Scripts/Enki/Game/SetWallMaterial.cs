using UnityEngine;
using System.Collections;

public class SetWallMaterial : MonoBehaviour 
{

	public bool _Light = true;

	public Texture _LightPaper;
	public Texture _DarkPaper;

	// Use this for initialization
	void Start () 
	{

		if (_Light) 
		{
			GetComponent<Renderer>().material.mainTexture = _LightPaper;

		} else 
		{
			GetComponent<Renderer>().material.mainTexture = _DarkPaper;

		}


	}
	
	// Update is called once per frame
	void Update () 
	{




	}
}
