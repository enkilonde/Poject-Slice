using UnityEngine;
using System.Collections;




public class Jump : MonoBehaviour {

	public float _JumpHeight = 10;

	public bool _Grounded = false;
	private float _TimerCooldown = 0;

	private LocalManager _LManager;

	void Awake()
	{
		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager> ();
	}

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {

		if (!_LManager._IsPaused) 
		{
			
			_TimerCooldown -= Time.deltaTime;
			
			if (Input.GetKey (KeyCode.Space) && _Grounded && _TimerCooldown<=0) 
			{
				Jumping();
			}


		}

	
	}


	void Jumping(){

		transform.root.GetComponent<Rigidbody>().AddForce (0, _JumpHeight, 0, ForceMode.Impulse);
		_TimerCooldown = 0.2f;
	}


	void OnTriggerEnter(Collider coll){
		if (coll.tag == "Ground") {
			_Grounded = true;
		}
	}


	IEnumerator JumpReset()
	{

		yield return new WaitForSeconds (3.5f);
		_Grounded = true;
	}



	void OnTriggerExit(Collider coll){
		if (coll.tag == "Ground") {
			_Grounded = false;
		}
	}

}
