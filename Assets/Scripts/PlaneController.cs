using UnityEngine;
using System.Collections;
using Leap;

public class PlaneController : MonoBehaviour {
	public GameObject plane;
	private TextMesh text;
	private LeapManager _leapManager;
	private Quaternion defaultRot;

	// Use this for initialization
	void Start () {
		text = (GameObject.Find("Finger Count") as GameObject).GetComponent(typeof(TextMesh)) as TextMesh;
		_leapManager = (GameObject.Find("LeapManager") as GameObject).GetComponent(typeof(LeapManager)) as LeapManager;
		defaultRot = gameObject.transform.localRotation;
	}
	
	// Update is called once per frame
	void Update () {
		Hand primeHand = _leapManager.frontmostHand();
		Vector3 palmnatural = new Vector3(primeHand.Direction.Pitch,primeHand.Direction.Yaw,primeHand.PalmNormal.Roll);
		palmnatural.x *= -1; //This is the mistake I made, it seems we must flip the x axis for the rotation to look right.
		palmnatural*=90; //Now we rotate it 90 degrees.
		if (primeHand.PalmNormal.x != 0 || primeHand.PalmNormal.y != 0 || primeHand.PalmNormal.z != 0)
			gameObject.transform.localRotation = Quaternion.Euler (palmnatural); //Where cube is the palm gameObject.
		else
			gameObject.transform.localRotation = defaultRot;
		text.text =" tes ";
		Debug.Log (1 + " " + primeHand.Direction);
		Debug.Log ("x " + primeHand.PalmNormal.x);
		Debug.Log ("y " + primeHand.PalmNormal.y);
		Debug.Log ("z " + primeHand.PalmNormal.z);
		Debug.Log ("yaw " + primeHand.PalmNormal.Yaw);
		Debug.Log (3 + " " + gameObject.transform.rotation.x);
		Debug.Log (3 + " " + gameObject.transform.rotation.y);
		Debug.Log (3 + " " + gameObject.transform.rotation.z);
	}
}
