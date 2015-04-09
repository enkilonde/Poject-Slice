using UnityEngine;
using System.Collections;

public class PortalDegradation : MonoBehaviour {

	public float _DegradationSpeed = 0.001f;
	public float _DestructionLimite = 0.001f;
	public float _DegradationCooldown = 1.0f;

	private Transform _localTransform;
	private PhotonView _PhotonView;

	public float _Size = 0;
	 

	public GameObject _CameraPortal;

	// Use this for initialization
	void Start () {
		_localTransform = transform;
		_PhotonView = GetComponent<PhotonView> ();


		//_PhotonView.RPC("InstantiateCamera", PhotonTargets.All, _Size); 

	}
	
	// Update is called once per frame
	void Update () {

		_DegradationCooldown -= Time.deltaTime;


		if (_DegradationCooldown <= 0) 
		{
			Transform t = transform.Find("FireParticle");
			t.GetComponent<ParticleSystem>().Stop();
			_localTransform.localScale = new Vector3 (_localTransform.localScale.x - (_DegradationSpeed), _localTransform.localScale.y, _localTransform.localScale.z - (_DegradationSpeed));
			if (_localTransform.localScale.x <= _DestructionLimite) {
				//Destroy(this.gameObject);

				//GameObject.Find ("Scripts").GetComponent<PortalLayerManager> ().removePortal(this.gameObject, GetComponent<PortalLayer>()._Layer);

				
				if (!GameObject.Find("Scripts").GetComponent<PortalLayerManager>().CheckIfOtherPortalBool(transform.position))
				{
					GetComponent<ignoreCollision>().restoreLayer(8);
					
				}
				Destroy(GetComponent<PortalLayer>()._camera);
				if (_PhotonView.isMine)
				{
					//StartCoroutine("DestroyDelay");
					PhotonNetwork.Destroy(this.gameObject);
				}


			}





		}




	}


	IEnumerator DestroyDelay()
	{
		yield return 0;



	}



	[RPC]
	void InstantiateCamera(float _scale)
	{

		GameObject _PC = Instantiate (_CameraPortal, transform.position, transform.rotation) as GameObject;
		//_PC.transform.localScale *= _scale;
		//_PC.transform.localEulerAngles -=  transform.localEulerAngles;
		//print (transform.localEulerAngles + "      " + _PC.transform.localEulerAngles);

		_PC.transform.SetParent (transform);
		RenderTexture _Textu;
		_Textu = new RenderTexture(500, 500, 16);
		//_PC.transform.GetChild(0).GetComponent<Camera>().targetTexture = _Textu;
		_PC.GetComponent<Renderer>().material.mainTexture = _Textu;
		//transform.localScale *= _scale;
	}





}
