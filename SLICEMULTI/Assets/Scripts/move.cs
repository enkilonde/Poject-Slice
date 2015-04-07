using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	public float _speedMultiplier = 1;
	public float _MaxSpeed = 10;
	private Rigidbody  _RB;
	public float _AirControl = 0.5f;
	private float _AirControllActive = 1;

	// Use this for initialization
	void Start () {
	
		_RB = GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{

		if ((Input.GetAxisRaw ("Horizontal") != 0 || Input.GetAxisRaw ("Vertical") != 0)) 
		{
			if (!transform.Find("FootCollider").GetComponent<Jump>()._Grounded)
			{
				_AirControllActive = _AirControl;
			}else
			{
				_AirControllActive = 1;
			}

			Vector3 _direction = transform.right * Input.GetAxisRaw ("Horizontal") + transform.forward * Input.GetAxisRaw ("Vertical");
			_direction *= _speedMultiplier * Time.deltaTime * _AirControllActive;

			_RB.AddForce (_direction, ForceMode.VelocityChange);



			//Vector3 _direction = new Vector3(Input.GetAxis("Horizontal")* SceneController._control._Vitesse * _speedMultiplier, 0, Input.GetAxis("Vertical")* SceneController._control._Vitesse * _speedMultiplier);

			//GetComponent<Rigidbody> ().velocity = new Vector3(_direction.x, GetComponent<Rigidbody> ().velocity.y, _direction.z) * _speedMultiplier * Time.deltaTime * 10;


		} else 
		{
			_RB.velocity = new Vector3(0, _RB.velocity.y , 0);


		}



		Vector2 _vitesseLimiter = new Vector2 (_RB.velocity.x, _RB.velocity.z);

		if (_vitesseLimiter.magnitude > _MaxSpeed) 
		{
			_vitesseLimiter = _vitesseLimiter.normalized * _MaxSpeed;
			_RB.velocity = new Vector3(_vitesseLimiter.x, _RB.velocity.y, _vitesseLimiter.y);
		}


	}
}
