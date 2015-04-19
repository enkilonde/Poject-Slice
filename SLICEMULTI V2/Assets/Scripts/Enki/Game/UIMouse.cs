using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMouse : MonoBehaviour 
{

	private LocalManager _LManager;
	private Image _MouseIMG;


	// Use this for initialization
	void Start () 
	{
		_LManager = GameObject.Find("Manager").GetComponent<LocalManager>();
		_MouseIMG = transform.parent.Find ("Canvas").Find ("MouseUI").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		_MouseIMG.enabled = _LManager._IsMouse;

	}
}
