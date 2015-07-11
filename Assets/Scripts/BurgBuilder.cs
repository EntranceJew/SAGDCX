using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BurgBuilder : MonoBehaviour {
	public PartZone pz;

	// Use this for initialization
	void Start () {
		pz = gameObject.GetComponentInChildren<PartZone> ();
	}

	public bool EmancipatePart(GameObject part){
		if (BelongsToMe (part)) {
			part.GetComponent<Food>().Emancipate();
			if(transform.childCount == 1){
				pz.enabled = true;
			}
			return true;
		}
		return false;
	}

	public bool ObtainNewPart(GameObject part){
		// We only accept orphans.
		// Originally we only cared if the object's parent wasn't us but that's more effort,
		// because the first condition clashes with the second half and boy am I tired.
		// part.transform.parent != null & part.transform.parent.gameObject != this.gameObject
		if (!BelongsToMe(part)) {
			//Debug.Log ("I just found myself a new " + part.name);
			part.GetComponent<Food> ().GetObtained (this);

			//Vector3 pzt = pz.transform.localPosition;

			//pzt.y += 0.7f;

			//pz.transform.localPosition = pzt;

			//Rigidbody rb = part.GetComponent<Rigidbody> ();
			//rb.useGravity = false;
			//rb.isKinematic = true;
			//rb.freezeRotation = true;
			return true;
		} else {
			return false;
			Debug.Log ("NO, FUCK YOUR " + part.name);
		}
	}

	public bool BelongsToMe(GameObject part){
		if (part.transform.parent != null) {
			if(part.transform.parent != gameObject.transform){
				Debug.Log ("I was its parent.");
				return false;
			} else {
				return true;
			}
		} else {
		//	Debug.Log ("Did not have parent.");
			return false;
		}
	}
	public List<GameObject> GetChildParts(){
		List<GameObject> burgObjects = new List<GameObject> ();
		foreach (Transform t in transform) {
			if (t.name != "PartZone") {
				burgObjects.Add (t.gameObject);
			}
		}
		return burgObjects;
	}

	public void TrashBurger(){
		foreach (Transform t in transform) {
			if (t.name != "PartZone") {
				Destroy (t.gameObject);
			}
		}
		pz.enabled = true;
	}
}
