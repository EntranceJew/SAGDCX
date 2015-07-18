using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FailBell : MonoBehaviour {
	public int pressState;
	public DayManager dayManager;

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		dayManager.FailDay();
		Debug.Log ("YOU ARE DEAD. NOT BIG SURPRISE.");
	}
}