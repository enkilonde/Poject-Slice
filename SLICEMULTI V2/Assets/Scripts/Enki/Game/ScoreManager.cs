using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour 
{

	public int _LocalScore = 0;
	public Text _ScoreText;
	public Image _ScoreCadre;
	public NetworkCharacter _NCharacter;
	public bool _Ismouse;


	private PhotonView _PhotonView;

	public Sprite _MouseRed;
	public Sprite _MouseYellow;
	public Sprite _MouseGreen;
	public Sprite _MousePurple;



	// Use this for initialization
	void Start () 
	{
		_NCharacter = GetComponent<NetworkCharacter> ();
		_PhotonView = GetComponent<PhotonView> ();

		if (_PhotonView.isMine) 
		{
			_ScoreText = transform.Find ("Canvas").Find("ScoreContainer").Find ("Score").GetComponent<Text> ();
			_ScoreCadre = transform.Find ("Canvas").Find("ScoreCadre").GetComponent<Image>();
		}


	}
	
	// Update is called once per frame
	void Update () 
	{
		_Ismouse = _NCharacter._IsMouse;
		if (_PhotonView.isMine) 
		{
			_ScoreText.text = _LocalScore.ToString ();

   			switch (_NCharacter._Equipe) 
			{
			case LocalManager.Equipe.Green : _ScoreCadre.sprite = _MouseGreen; break;
			case LocalManager.Equipe.Red : _ScoreCadre.sprite = _MouseRed; break;
			case LocalManager.Equipe.Yellow : _ScoreCadre.sprite = _MouseYellow; break;
			case LocalManager.Equipe.Blue : _ScoreCadre.sprite = _MousePurple; break;
			}


		}





		if (_Ismouse) 
		{

		} else 
		{

		}




	}
}
