using UnityEngine;
using System.Collections;

public class RandomNumber : MonoBehaviour {

	private static TextMesh _text;
	private static string _teksDisp;

	private static bool _isChange = false;
	private static int _currentLength;
	public int CurrentLength {
		get { return _currentLength; }
	}
	// Use this for initialization
	void Start () {
		_text = gameObject.GetComponent (typeof(TextMesh)) as TextMesh;
		_teksDisp = "";
		setRandom (5);
	}

	public void setRandom(int length) {
		string teks = "";
		//Random.seed = 53123;
		for(int i = 0; i < length; i++) {
			teks += (int) Random.Range(0,9);
		}
		_currentLength = length;
		_isChange = true;
		_teksDisp = teks;
	}

	public string getRandomNumber() {
		return _teksDisp;
	}

	// Update is called once per frame
	void Update () {
		if (_isChange) {
			_text.text = _teksDisp;
			_isChange = false;
		}
	}
}
