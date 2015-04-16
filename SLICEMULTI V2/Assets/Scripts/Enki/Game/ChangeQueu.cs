using UnityEngine;
using System.Collections;

public class ChangeQueu : MonoBehaviour 
{

	public int _QueueValue = 2000;
	public int _ShaderQueue = 2010;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{

		GetComponent<Renderer> ().material.renderQueue = _QueueValue;


	}
}
