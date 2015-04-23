using UnityEngine;
using System.Collections;

public class Respoawn : MonoBehaviour 
{

	private LocalManager _LManager;

	private Vector3 _SpawnPosition;
	private float _SpawnRandom;

	// Use this for initialization
	void Start () 
	{

		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();
		
		_SpawnPosition = _LManager._SpawnPosition;
		_SpawnRandom = _LManager._SpawnRandom;


	}
	
	// Update is called once per frame
	void Update () 
	{

		if (transform.position.y <= -100) 
		{
			transform.parent.position = new Vector3 (_SpawnPosition.x + Random.Range(-_SpawnRandom, _SpawnRandom), _SpawnPosition.y, _SpawnPosition.z + Random.Range(-_SpawnRandom, _SpawnRandom));
			transform.parent.GetComponent<Rigidbody>().velocity = Vector3.zero;

		}
	
	}
}
