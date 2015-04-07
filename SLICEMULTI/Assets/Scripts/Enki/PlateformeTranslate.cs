using UnityEngine;
using System.Collections;

public class PlateformeTranslate : MonoBehaviour {

	public Vector3 _destination;
	public float _Vitesse = 1;
	private bool _back = true;
	private Vector3 _origine;
	private Vector3 _Direction;

	private bool _init = false;

	// Use this for initialization
	void Start () {
		_origine = transform.position;

		_Direction = _destination - _origine;

	}
	
	// Update is called once per frame
	void Update () {
		if (_init) {

			if (_back) {
				transform.position += _Direction.normalized * _Vitesse/100;
			} else {
				transform.position -= _Direction.normalized * _Vitesse/100;
			}
			if ((Vector3.Distance (transform.position, _destination)<0.1f && _back) || (Vector3.Distance (transform.position, _origine)<0.1f && !_back)) {
				_back = !_back;
			}

				}
			
	
		_init = true;
	}

	void OnTriggerEnter(Collider _coll){

		if (_coll.gameObject.tag == "FootCollider") {
			print("ttt");
				}

		}

}
