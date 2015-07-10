using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	public GameObject lb;
	public GameObject burgBuild;

	public bool evaluated = false;
	public bool isEvaluating = false;

	private AltScore score;

	void Start(){
		score = GetComponent<AltScore> ();
	}

	void OnMouseDown() {
		//Check order : THIS SHOULD BE REPLACED WITH ACTUAL CHECK ORDER SCRIPTS, this is just for human evaluation right now.
		if (!isEvaluating) {
			isEvaluating = true;
			score.EvaluateBurger(GetOrder(), GetBurger());
		}
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
	}

	void Update(){
		if (isEvaluating) {
			evaluated = score.done;
		}
		if(evaluated) {
			isEvaluating = false;



			evaluated = false;
		}
	}


	List<GameObject> GetOrder() {
		//Get order from the lightbulb
		return lb.GetComponent<AltGetOrder> ().CurrentOrder().completeOrder;
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