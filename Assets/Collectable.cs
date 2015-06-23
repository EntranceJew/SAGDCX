using UnityEngine;
using System.Collections;

public class Collectable : MonoBehaviour {
	private Rigidbody rb;

	private GameObject attached;
	private float force = 2000;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		rb.freezeRotation = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (attached != null) {
			transform.position = attached.transform.FindChild("PointFace").transform.FindChild("PointEnd").transform.position;
		}
	}

	void OnCollisionEnter (Collision col) {
		Debug.Log (col.gameObject.name);
		if (col.gameObject.name == "Player") {
			Attach (col.gameObject);
		}
	}

	void Attach(GameObject attachTo){
		if (attached == null) {
			attached = attachTo;
			rb.isKinematic = true;
			Player pl = attachTo.GetComponent<Player>();
			pl.holdObject(gameObject);
		} else {
			Debug.Log ("Tried to attach to something, but was already attached.");
		}
	}

	public void Throw(Vector3 dir) {
		Debug.Log ("THROWN!");
		rb.isKinematic = false;
		attached = null;
		rb.AddForce(dir * force);
	}
}