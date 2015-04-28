using UnityEngine;
using System.Collections;

public class ScroreMultiplier : MonoBehaviour 
{

	public int _ScoreFrequence = 1;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider _coll)
	{
		if (_coll.tag == "Player") 
		{
			_coll.GetComponent<NetworkCharacter>()._FrequenceScore = _ScoreFrequence;

		}
	}


	void OnTriggerExit(Collider _coll)
	{
		if (_coll.tag == "Player") 
		{
			_coll.GetComponent<NetworkCharacter>()._FrequenceScore = 1;
			
		}
	}

}
