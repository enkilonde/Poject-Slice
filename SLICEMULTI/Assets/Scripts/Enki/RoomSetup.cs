using UnityEngine;
using System.Collections;

public class RoomSetup : MonoBehaviour {

	public bool _SpeedUp = false;

	public float _SpeedValue = 5;
	
	public bool _NewGravity = false;
	public Vector3 _Directions = new Vector3(0, 0, 0);
	
	public bool _Bouncy = false;
	public bool _bounce = false;
	public float _BounceIntensity = 5;
	
	public bool _DeathRoom = false;
	public bool _deathy = false;
	public float _Delay = 5;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
