using UnityEngine;
using System.Collections;

public class Respoawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (transform.position.y <= -100) 
		{
			transform.parent.position = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));


		}
	
	}
}
