using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServeBell : MonoBehaviour {
	public GameObject daymanerer;

	private DayManager dayManager;

	void Start(){
		dayManager = daymanerer.GetComponent<DayManager> ();
	}

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		// @TODO: Make this serve something.

		dayManager.GetNextOrder ();
	}
}