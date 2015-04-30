using UnityEngine;
using System.Collections;

public class ScroreMultiplier : MonoBehaviour 
{

	public float _ScoreFrequence = 1;
	/*	= 0.3 pour dehors
	 *  = 1 pour etage 1
	 *  = 2 pour etage 2
	 *  = 4 pour etage 3
	 */

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
			_coll.GetComponent<NetworkCharacter>()._FrequenceScore = 0.5f;
			
		}
	}

}
