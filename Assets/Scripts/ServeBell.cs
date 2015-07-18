using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServeBell : MonoBehaviour {
	public DayManager dayManager;

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		dayManager.GetNextOrder ();
	}
}