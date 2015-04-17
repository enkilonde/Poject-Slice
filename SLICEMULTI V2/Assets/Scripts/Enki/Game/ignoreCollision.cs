using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ignoreCollision : MonoBehaviour 
{

	public List<GameObject> twoWalls = new List<GameObject>();

	public static bool _InWall = false;
	public int _layer;

	private PhotonView MyPhotonView;

	// Use this for initialization
	void Start () 
	{

		Collider[] colli = Physics.OverlapSphere (transform.position, 0.1f);
		_layer = GetComponent<PortalLayer> ()._Layer + 10;

		foreach (Collider obj in colli) 
		{
			if (obj.tag == "Ground")
			{
				twoWalls.Add(obj.gameObject);
				obj.gameObject.layer = _layer;
				obj.gameObject.GetComponent<Renderer>().material.renderQueue = 2020;
			}
		}

	



	}


	public void restoreLayer(int _lay)
	{

		foreach (GameObject _obj in twoWalls) 
		{
			_obj.layer = _lay;
			//_obj.gameObject.GetComponent<Renderer>().material.renderQueue = 2000;
		}

	}



	public void SetWallQueue(int _Queue)
	{

		foreach (GameObject _obj in twoWalls) 
		{
			_obj.gameObject.GetComponent<Renderer>().material.renderQueue = _Queue;
		}

	}

	void OnTriggerEnter(Collider _coll){




		if (_coll.tag == "Player") {



			for (int i = 0; i< twoWalls.Count; i++){


				Physics.IgnoreCollision(_coll.GetComponent<Collider>(), twoWalls[i].GetComponent<Collider>());
				//twoWalls[i].collider.enabled = false;


				//_coll.transform.Find("FootCollider").GetComponent<Jump>()._InWall = true;
				//SceneController._control.transform.Find("Trought").GetComponent<AudioSource>().Play();
			}

			}

		}


	void OnTriggerExit(Collider _coll){
		
		if (_coll.tag == "Player") {

				
				for (int i = 0; i< twoWalls.Count; i++){
					
					Physics.IgnoreCollision(_coll.GetComponent<Collider>(), twoWalls[i].GetComponent<Collider>(), false);
				//_coll.transform.Find("FootCollider").GetComponent<Jump>()._InWall = false;
					
				}

		}
		
	}




}
