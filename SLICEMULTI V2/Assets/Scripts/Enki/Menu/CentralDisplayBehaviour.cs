using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CentralDisplayBehaviour : MonoBehaviour {

	private Text _Text;
	public float _TimeToDiseapear;
	public float _FadeOutCooldown = 1;
	private bool _CanDisapear = false;

	// Use this for initialization
	void Start () 
	{
		_Text = transform.Find ("Text").GetComponent<Text> ();
		StartCoroutine (Wait());

		Color _SetColor = _Text.material.color;
		_SetColor.a = 1;
		_Text.material.color = _SetColor;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (_CanDisapear) 
		{
			Color _NewColor = _Text.material.color;

			_NewColor.a -= (Time.deltaTime / (_TimeToDiseapear + 0.01f));
			_Text.material.color = _NewColor;
			if (_NewColor.a <= 0)
			{
				_Text.text = "";
				_NewColor.a = 1;
				_Text.material.color = _NewColor;
				Destroy(this.gameObject);
			}
		}
	}

	IEnumerator Wait()
	{
		yield return new WaitForSeconds(_FadeOutCooldown);
		_CanDisapear = true;

	}



}
