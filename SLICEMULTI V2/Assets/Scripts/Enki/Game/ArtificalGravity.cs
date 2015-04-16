using UnityEngine;
using System.Collections;

public class ArtificalGravity : MonoBehaviour {

	public Vector3 _PersonalGravity = Physics.gravity;

	// Use this for initialization
	void Start () {
		_PersonalGravity = Physics.gravity;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		GetComponent<Rigidbody>().AddForce (_PersonalGravity);
	
	}
}
