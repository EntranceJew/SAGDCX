using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	GameObject lb;
	GameObject burgBuild;

	void Start() {
		lb = GameObject.Find ("Lightbulb");
		burgBuild = GameObject.Find ("BurgBuilder");
	}

	void OnMouseDown() {
		//Get order from the lightbulb
		Order order = lb.GetComponent<GetOrder>().CurrentOrder ();
		//Get list of objects in the burgbuilder
		List<GameObject> burgObjects = new List<GameObject> ();
		foreach (Transform t in burgBuild.transform) {
			if (t.name != "PartZone") {
				burgObjects.Add(t.gameObject);
			}
		}

		//Check order : THIS SHOULD BE REPLACED WITH ACTUAL CHECK ORDER SCRIPTS, this is just for human evaluation right now.
		foreach (GameObject obj in burgObjects) {
			print (obj.name);
		}

		//Trash burger
		foreach (Transform t in burgBuild.transform) {
			if (t.name != "PartZone") {
				Destroy (t.gameObject);
			}
		}

		//Allow new burg to be made
			foreach (Transform t in burgBuild.transform) {
			if (t.name == "PartZone") {
				t.GetComponent<PartZone>().enabled = true;
			}
		}


		//Don't forget to tell the lightbulb that a new order is needed to be gotten.
		lb.GetComponent<GetOrder> ().NewOrder ();
	}
}
