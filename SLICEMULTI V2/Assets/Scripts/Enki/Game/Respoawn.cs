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
			transform.parent.position = new Vector3(Random.Range(-40.0f, 40.0f), 2, Random.Range(-20.0f, 20.0f));
			transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;

		}
	
	}
}
