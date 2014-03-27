using UnityEngine;
using System.Collections;

public class tGUI : MonoBehaviour {

	private static TextMesh _text;
	private static string _teksDisp;

	private static bool _isChange = false;

	// Use this for initialization
	void Start () {
		_text = gameObject.GetComponent (typeof(TextMesh)) as TextMesh;
		_teksDisp = "";

	}

	public static void updateTeks(string teks) {
		_isChange = true;
		_teksDisp += teks;
	}

	// Update is called once per frame
	void Update () {
		if (_isChange) {
			_text.text = _teksDisp;
			_isChange = false;
		}
	}

}
