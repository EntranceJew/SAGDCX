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

	public bool holdObject (GameObject objectToHold) {
		if (holding) {
			return false;
		} else {
			holding = true;
			heldObject = objectToHold;
			return true;
		}
	}

	void ThrowObject () {
		holding = false;
		heldObject.GetComponent<Collectable>().Throw(transform.forward);
		heldObject = null;
	}
}
