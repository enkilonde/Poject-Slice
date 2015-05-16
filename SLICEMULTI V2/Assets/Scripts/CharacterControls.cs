using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharacterControls : MonoBehaviour {
	
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public bool _Jumping = false;
	public float jumpHeight = 2.0f;
	public float _JumpTime = 1;
	public float _MaxJumpTime = 2;

	public PhotonView _PhotonView;
	public SoundBehaviour _Saut;
	
	void Awake () {
		GetComponent<Rigidbody>().freezeRotation = true;
		GetComponent<Rigidbody>().useGravity = false;
		_PhotonView = GetComponent<PhotonView> ();
	}

	void Update()
	{
		_JumpTime -= Time.deltaTime;

	}

	
	void FixedUpdate () {
		if (true) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = GetComponent<Rigidbody>().velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
			
			// Jump

			canJump = transform.Find("FootCollider").GetComponent<Jump>()._Grounded;


			if (Input.GetButtonUp("Jump"))
			{
				_Jumping = false;
			}

			if (Input.GetButton("Jump") && _Jumping && _JumpTime > 0)
			{
				GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}



			if (canJump == true)
			{
				_Jumping = false;
			}


			if (canJump && Input.GetButton("Jump")) 
			{
				_JumpTime = _MaxJumpTime;
				GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
				_Jumping = true;
				//StartCoroutine(EndJump());
				_PhotonView.RPC("PlaySaut", PhotonTargets.All);
			}
		}
		
		// We apply gravity manually for more tuning control
		GetComponent<Rigidbody>().AddForce(new Vector3 (0, -gravity * GetComponent<Rigidbody>().mass, 0));
		
	}
	

	
	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity * (_JumpTime/_MaxJumpTime));
	}


	IEnumerator EndJump()
	{
		yield return new WaitForSeconds(_JumpTime);
		_Jumping = false;

	}



	[RPC]
	public void PlaySaut()
	{

		_Saut.PlaySound ();

	}



}