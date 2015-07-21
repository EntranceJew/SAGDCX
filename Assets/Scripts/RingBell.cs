using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	public BurgBuilder burgBuilder;

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		// Lift it up, someone will tell him to come down eventually.
		burgBuilder.Hoist();
	}
}