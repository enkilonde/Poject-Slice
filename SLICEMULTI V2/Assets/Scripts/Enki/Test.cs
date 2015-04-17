using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider _coll)
	{

		if (_coll.tag == "Player") 
		{
			print("Player");

		}

	}

}
