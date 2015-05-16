using UnityEngine;
using System.Collections;

public class SoundBehaviour : MonoBehaviour 
{

	public enum SoundType {Music, SFX};
	public SoundType _SoundType;

	public float _LocalVolume;
	public LocalManager _LManager;

	// Use this for initialization
	void Awake () 
	{
		_LManager = GameObject.Find ("Manager").GetComponent<LocalManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}


	public void PlaySound()
	{
		GetComponent<AudioSource> ().volume = _LManager._MasterVolume/100 * _LocalVolume;
		GetComponent<AudioSource>().Play();

	}


}
