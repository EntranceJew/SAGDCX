using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrashBell : MonoBehaviour {
	public GameObject lb;
	public GameObject burgBuild;

	public bool evaluated = false;
	public bool isEvaluating = false;

	private Score score;

	void Start(){
		score = GetComponent<Score> ();
	}

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		TrashBurger ();

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

	void TrashBurger(){
		foreach (Transform t in burgBuild.transform) {
			if (t.name != "PartZone") {
				Destroy (t.gameObject);
			}
		}
		
		//Allow new burg to be made
		foreach (Transform t in burgBuild.transform) {
			if (t.name == "PartZone") {
				t.GetComponent<PartZone> ().enabled = true;
			}
		}
	}
}