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
		//Check order : THIS SHOULD BE REPLACED WITH ACTUAL CHECK ORDER SCRIPTS, this is just for human evaluation right now.
		GetComponent<Score> ().EvaluateBurger (GetOrder (), GetBurger ());


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
		
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		//Don't forget to tell the lightbulb that a new order is needed to be gotten.
		lb.GetComponent<GetOrder> ().NewOrder ();
	}


	List<GameObject> GetOrder() {
		//Get order from the lightbulb
		return lb.GetComponent<GetOrder> ().CurrentOrder().completeOrder;
	}

	List<GameObject> GetBurger() {
		//Get list of objects in the burgbuilder
		List<GameObject> burgObjects = new List<GameObject> ();
		foreach (Transform t in burgBuild.transform) {
			if (t.name != "PartZone") {
				burgObjects.Add(t.gameObject);
			}
		}

		return burgObjects;
	}

}