using UnityEngine;
using System.Collections;
using Leap;

public class ButtonBehav : MonoBehaviour {
	public Sprite _normal;
	public Sprite _selected_path;
	public string _selectionAction; //Game Action to be performed when this item is selected.
	public GameObject _selectedSprite;
	public GameObject _stinger_type;
	public string value;

	public enum State { NORMAL, SELECTED };

	private State _currentState = State.NORMAL;
	private bool _selected = false;
	
	private GameObject _backing;
	private GameObject _fillBar;
	private TextMesh _text;
	private GameObject _stinger;
	private LeapManager _leapManager;
	//private tGUI _tGUI;
	
	// Use this for initialization
	void Start () {
		//_tGUI = (GameObject.Find ("tGUI") as GameObject).GetComponent(typeof(tGUI)) as tGUI;
		_backing = 	gameObject.transform.GetChild(0).gameObject;
		_fillBar = 	gameObject.transform.GetChild(1).gameObject;
		_text = 	gameObject.transform.GetChild(2).gameObject.GetComponent(typeof(TextMesh)) as TextMesh;
		_text.text = value + "";
		if(_selectionAction != "") { Debug.LogWarning("Button with both selection action and linkage detected."); }
		_leapManager = (GameObject.Find("LeapManager") as GameObject).GetComponent(typeof(LeapManager)) as LeapManager;
		gameObject.SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {

		GestureList gestures = _leapManager.currentFrame.Gestures();
		InteractionBox interactionBox = _leapManager.currentFrame.InteractionBox;

		if (gestures.Count > 0) {

			for (int i = 0; i < gestures.Count; i++) {
				Gesture gesture = gestures[i];
				Debug.Log(gesture.Type);
				switch (gesture.Type) {
					case Gesture.GestureType.TYPESCREENTAP:
						ScreenTapGesture screenTap = new ScreenTapGesture(gesture);
						// Debug.Log("Position: " + screenTap.Position + ", Normalized: " + interactionBox.NormalizePoint(screenTap.Position));
						if (containsPoint(screenTap.Position.ToUnityTranslated())) 
							changeState(State.SELECTED);
						// Debug.Log("Position: " + screenTap.Position + ", Direction: " + screenTap.Direction);
						break;
					default:
						Debug.Log("Unknown gesture type.");
						break;
				}
			}
		} else {
			changeState(State.NORMAL);
		}

		/*
		normal -> containsPoint -> selected
		-> normal -> not containsPoint -> normal
		if (_currentState == State.NORMAL) {
			if(containsPoint(_leapManager.pointerPositionWorld)) {
				changeState(State.SELECTED);
			}
		} else {
			changeState(State.NORMAL);
			if(!containsPoint(_leapManager.pointerPositionWorld)) {
				changeState(State.NORMAL);
			}
		}
		*/
	}

	private bool containsPoint(Vector3 point)
	{
		Rect box = new Rect(
			gameObject.transform.GetChild(0).position.x+gameObject.transform.GetChild(0).renderer.bounds.size.x/2, 
			gameObject.transform.GetChild(0).position.y-gameObject.transform.GetChild(0).renderer.bounds.size.y/2, 
			gameObject.transform.GetChild(0).renderer.bounds.size.x, 
			gameObject.transform.GetChild(0).renderer.bounds.size.y);
		Debug.Log (	point.x <= box.x + box.width && point.x >= box.x &&
		           point.y >= box.y + (-1*(box.height)) && point.y <= box.y &&
		           point.z > 0 );
		if(	point.x <= box.x + box.width && point.x >= box.x &&
		   point.y >= box.y + (-1*(box.height)) && point.y <= box.y &&
		   point.z > 0 )
		{
			return true;
		}
		
		return false;
	}

	public void changeState(State newState)
	{
		if (newState != _currentState)
		{
			switch(newState)
			{
				case State.NORMAL:
					//(_backing.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = _normal;
					_backing.SetActive(true);
					_fillBar.SetActive(false);
					break;
				case State.SELECTED:
					_fillBar.SetActive(true);
					_backing.SetActive(false);
					tGUI.updateTeks(value);
					//_fillBar.transform.localScale = new Vector3(1, 1, 1);
					//(_backing.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite = _normal;
					break;
				default:
					Debug.LogError("Unrecognized State passed to changeState(State newState)");
					return;
					break;
			}

			_currentState = newState;


		}
	}

	public State currentState
	{
		get { return _currentState; }
		set { _currentState = value; }
	}

	public bool selected
	{
		get { return _selected; }
	}
}
