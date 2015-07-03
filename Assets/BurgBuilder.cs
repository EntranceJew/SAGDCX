using UnityEngine;
using System.Collections;

public class BurgBuilder : MonoBehaviour {
	public PartZone pz;

	// Use this for initialization
	void Start () {
		pz = gameObject.GetComponentInChildren<PartZone> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ObtainNewPart(GameObject part){
		// We only accept orphans.
		// Originally we only cared if the object's parent wasn't us but that's more effort,
		// because the first condition clashes with the second half and boy am I tired.
		// part.transform.parent != null & part.transform.parent.gameObject != this.gameObject
		if (part.transform.parent == null 
		    || (part.transform.parent != null && part.transform.parent != this.transform)) {
			//Debug.Log ("I just found myself a new " + part.name);
			part.GetComponent<Food>().GetObtained(this);

			//Vector3 pzt = pz.transform.localPosition;

			//pzt.y += 0.7f;

			//pz.transform.localPosition = pzt;

			//Rigidbody rb = part.GetComponent<Rigidbody> ();
			//rb.useGravity = false;
			//rb.isKinematic = true;
			//rb.freezeRotation = true;
		}
	}
}
