using UnityEngine;
using System.Collections;

public class FollowPortalCam : MonoBehaviour {

	public GameObject[] _Cams;

	public GameObject _portalPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		//_Cams = new GameObject[];

		_Cams = GameObject.FindGameObjectsWithTag ("PortalCam");

		foreach (GameObject _camera in _Cams) 
		{
			Transform _camTransform = _camera.transform;
			_camTransform.LookAt(_camTransform.position + (_camTransform.position - transform.position));
			_camTransform.Rotate(0, 0, 180);

			//_camera.GetComponent<Camera>().fieldOfView = (_camTransform.parent.parent.localScale.x / _portalPrefab.transform.localScale.x) / (Vector3.Distance(transform.position, _camTransform.position)/100);

			//_camera.GetComponent<Camera>().fieldOfView = GetFOV(transform.position, _camera.transform.position, _camera.transform.parent.);


		}

	
	}



}
