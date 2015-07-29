using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PartZone : MonoBehaviour {
	public BurgBuilder bb;
	
	public Dictionary<GameObject, bool> collisions = new Dictionary<GameObject, bool>();
	
	// Use this for initialization
	void Start () {
		//bb = transform.parent.GetComponent<BurgBuilder> ();
	}

	void Update(){
		List<GameObject> keys = new List<GameObject>(collisions.Keys);
		foreach (GameObject key in keys){
			if(collisions[key] && ShouldObtainObject (key)){
				ForceObtain(key);
			}
		}
	}
	
	void OnCollisionEnter(Collision col){
		Debug.Log ("PLATE FOUND " + col.gameObject);
		collisions.Add (col.gameObject, true);
		if (ShouldObtainObject(col.gameObject)) {
			//Debug.Log ("GRANTING NEW OBJECT: "+col.gameObject);
			//isFoodPope = false;
			ForceObtain(col.gameObject);
		}
	}
	
	void OnCollisionExit(Collision col){
		Debug.Log ("PLATE DIDN'T WANT " + col.gameObject);
		collisions.Remove (col.gameObject);
	}
	
	public bool ShouldObtainObject(GameObject obj){
		if (enabled) {
			Debug.Log ("SOO: WAS ENABLED");
		}
		if (obj.tag == "Food") {
			Debug.Log ("SOO: WAS FOOD");
		}
		if (!obj.GetComponent<Food> ().isGrabbed) {
			Debug.Log ("SOO: WAS NOT GRABBED");
		}
		if (enabled && obj.tag == "Food" && !obj.GetComponent<Food> ().isGrabbed) {
			Debug.Log ("ALLOWED " + obj);
			return true;
		} else {
			Debug.Log ("REJECTED " + obj);
			return false;
		}
	}

	public void ForceObtain(GameObject obj){
		collisions [obj] = false;
		bb.ObtainNewPart (obj);
	}
}
