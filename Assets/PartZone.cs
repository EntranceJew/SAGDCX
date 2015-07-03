using UnityEngine;
using System.Collections;

public class PartZone : MonoBehaviour {
	public BurgBuilder bb;

	// Use this for initialization
	void Start () {
		bb = transform.parent.GetComponent<BurgBuilder> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Food") {
			//Debug.Log (col.gameObject);
			col.gameObject.GetComponent<Food>().isFoodPope = true;
			bb.ObtainNewPart(col.gameObject);
			Destroy (this.gameObject);
		}
	}
}
