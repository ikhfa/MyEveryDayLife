using UnityEngine;
using System.Collections;

public class CountdownScript : MonoBehaviour {

	private static TextMesh _text;
	private static string _teksDisp;
	
	private static float _curSec = 0;
	private int _countdownSec = 30;
	public int countdownSec {
		get { return _countdownSec;}
		set { _countdownSec = value;}
	}
	private static RandomNumber _randomNumber;
	private static DialedNumber _dialedNumber;

	// Use this for initialization
	void Start () {
		_text = gameObject.GetComponent (typeof(TextMesh)) as TextMesh;
		_teksDisp = "";
		startCountdown (_countdownSec);
		_randomNumber = (GameObject.Find ("randomNumber") as GameObject).GetComponent(typeof(RandomNumber)) as RandomNumber;
		_dialedNumber = (GameObject.Find ("dialedNumber") as GameObject).GetComponent(typeof(DialedNumber)) as DialedNumber;
	}

	public void startCountdown(int seconds) {
		_teksDisp = seconds + "";
		_curSec = seconds;
		_countdownSec = seconds;
	}

	// Update is called once per frame
	void Update () {
		_curSec -= Time.deltaTime;
		//Debug.Log (_countdownSec);
		if (_curSec <= 0) {
			_dialedNumber.setTeks("Telat Nelpon Deh");
		} else {
				_teksDisp = ((int)_curSec) + "";
				_text.text = _teksDisp;
		}

	}
}
