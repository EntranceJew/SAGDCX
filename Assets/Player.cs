using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	bool holding = false;
	GameObject heldObject;

	// Update is called once per frame
	void Update () {
		if (holding && Input.GetButton("Fire1")) {
			ThrowObject();
		}
	}

	public void holdObject (GameObject objectToHold) {
		holding = true;
		heldObject = objectToHold;
	}

	void ThrowObject () {
		holding = false;
		heldObject.GetComponent<Collectable>().Throw(transform.forward);
		heldObject = null;
	}
}
