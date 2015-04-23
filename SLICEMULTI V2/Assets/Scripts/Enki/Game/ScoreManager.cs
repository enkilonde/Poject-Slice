using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{

	public int _LocalScore = 0;
	public Text _ScoreText;

	// Use this for initialization
	void Start () 
	{
		_ScoreText = transform.Find ("Canvas").Find ("Score").GetComponent<Text> ();


	}
	
	// Update is called once per frame
	void Update () 
	{
		_ScoreText.text = _LocalScore.ToString ();


	}
}
