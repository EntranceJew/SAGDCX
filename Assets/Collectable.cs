using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
	private Rigidbody rb;

	private bool attached;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.freezeRotation = false;
		attached = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter (Collision col) {
		Debug.Log (col.gameObject.name);
		if (col.gameObject.name == "Player") {
			Attach (col.gameObject);
		}
	}

	void Attach(GameObject attachTo){
		if (!attached) {
			gameObject.transform.parent = attachTo.transform.Find("PointEnd");
			rb.isKinematic = true;
			rb.freezeRotation = true;
			attached = true;
		} else {
			Debug.Log ("Tried to attach to something, but was already attached.");
		}
	}
}