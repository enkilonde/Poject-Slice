using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMouse : MonoBehaviour 
{

	private PlayerState _PS;
	private Image _MouseIMG;


	// Use this for initialization
	void Start () 
	{
		_PS = transform.parent.GetComponent<PlayerState> ();
		_MouseIMG = transform.parent.Find ("Canvas").Find ("MouseUI").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		_MouseIMG.enabled = _PS._IsMouse;

	}
}
