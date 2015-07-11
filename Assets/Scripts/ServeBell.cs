using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServeBell : MonoBehaviour {
	public GameObject orderer;

	private AltGetOrder getOrder;

	void Start(){
		getOrder = orderer.GetComponent<AltGetOrder> ();
	}

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		// @TODO: Make this serve something.

		getOrder.NewOrder ();
	}
}