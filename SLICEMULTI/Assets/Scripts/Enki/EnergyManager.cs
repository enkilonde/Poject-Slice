using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour {

	public float _Energy = 0;
	private float _MaxEnnergy = 100;
	private float _RegenRate = 1;

	public float _SecondToGainABarre = 1;

	public int _NombreMaxBarreEnnergy = 5;
	private int ExedentEnnergy{	get	{	return (int)_Energy - (_NombreBarreDispo * _NombreMaxBarreEnnergy);	}	}

	private int _EnergyParBarre{	get	{	return (int)_MaxEnnergy / _NombreMaxBarreEnnergy;	}	}
	
	public int _NombreBarreDispo{	get	{	return (int)_Energy / _EnergyParBarre;	}	set	{	_Energy -= _EnergyParBarre;	}	}

	private Text _UI;

	// Use this for initialization
	void Start () 
	{
		_Energy = _MaxEnnergy;
		_UI = transform.parent.Find ("Canvas").Find ("TextEnergy").GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () 
	{

		_UI.text = "Current Ennergy : " + _Energy + "\nNombres de barres : " + _NombreBarreDispo;

		if (_Energy < _MaxEnnergy) 
		{
			_Energy += _RegenRate;
		}

	
	}

}
