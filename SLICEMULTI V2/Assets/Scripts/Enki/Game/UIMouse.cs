using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMouse : MonoBehaviour 
{

	private PlayerState _PS;
	private NetworkCharacter _NC;
	private Image _MouseIMG;


	// Use this for initialization
	void Start () 
	{
		_PS = transform.parent.GetComponent<PlayerState> ();
		_NC = transform.parent.GetComponent<NetworkCharacter> ();
		_MouseIMG = transform.parent.Find ("Canvas").Find ("MouseUI").GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		_MouseIMG.enabled = _NC._IsMouse;

	}
}
