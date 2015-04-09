using UnityEngine;
using System.Collections;

public class PortalCollision : MonoBehaviour {

	public bool collided = false;
	void Start() {
		StartCoroutine(CheckForCollision());
	}
	void OnTriggerEnter() {
		collided = true;
	}
	IEnumerator CheckForCollision() {
		yield return null;
		Debug.Log (collided);
	}

}
