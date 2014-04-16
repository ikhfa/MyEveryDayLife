using UnityEngine;
using System.Collections;

public class DialedNumber : MonoBehaviour {

	private static TextMesh _text;
	private static string _teksDisp;

	private static bool _isChange = false;
	private static bool _isStart = false;

	private static RandomNumber _randomNumber;
	private static string _numberToDial;
	private static CountdownScript _countdownScript;
	private static LeapManager _leapManager;

	// Use this for initialization
	void Start () {
		_text = gameObject.GetComponent (typeof(TextMesh)) as TextMesh;
		_leapManager = (GameObject.Find("LeapManager") as GameObject).GetComponent(typeof(LeapManager)) as LeapManager;
		_randomNumber = (GameObject.Find ("randomNumber") as GameObject).GetComponent(typeof(RandomNumber)) as RandomNumber;
		_countdownScript = (GameObject.Find ("countdownTimer") as GameObject).GetComponent(typeof(CountdownScript)) as CountdownScript;
		_numberToDial = _randomNumber.getRandomNumber ();
		_teksDisp = "Start Game";
	}

	public void updateTeks(string teks) {
		if (!_isStart) {
				_teksDisp = "";
				_isStart = true;
				_countdownScript.startCountdown ((_countdownScript.countdownSec));
		}
		_numberToDial = _randomNumber.getRandomNumber();
		_isChange = true;

		_teksDisp += teks;		

		if(_teksDisp.Equals(_numberToDial)) {
			_isStart = false;
			_teksDisp = "Next Level";
			_countdownScript.countdownSec = _countdownScript.countdownSec -2;
			_randomNumber.setRandom((_randomNumber.CurrentLength + 1));
		}
		else if (!_numberToDial.Contains(_teksDisp)) {
			_isStart = false;
			_teksDisp = "Wrong Number";
		}

		if (teks.Equals ("Reset")) {
			_teksDisp = "";
			_isStart = true;
			Debug.Log(_countdownScript.countdownSec);
			_countdownScript.startCountdown ((_countdownScript.countdownSec));
			//_countdownScript.startCountdown ((30));
		}
	}

	public void setTeks(string teks) {
		_teksDisp = teks;
		_isChange = true;
	}

	// Update is called once per frame
	void Update () {
		if (_isChange) {
			_text.text = _teksDisp;
			_isChange = false;
		}
	}

}
