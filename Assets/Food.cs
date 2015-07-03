using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {
	public bool isFoodPope;
	public BurgBuilder bb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col){
		if (isFoodPope && col.gameObject.tag == "Food" && col.gameObject.transform.parent != bb.gameObject) {
			Debug.Log ("GRANTING NEW OBJECT: "+col.gameObject);
			isFoodPope = false;
			bb.ObtainNewPart(col.gameObject);
		}
	}

	// something to do when we're now property of the burg builder
	public void GetObtained(BurgBuilder newBurgBuilder){
		bb = newBurgBuilder;
		gameObject.transform.parent = bb.gameObject.transform;
		this.isFoodPope = true;
	}
}
