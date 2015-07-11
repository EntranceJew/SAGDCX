using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServeBell : MonoBehaviour {
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
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		lb.GetComponent<AltGetOrder> ().NewOrder ();
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