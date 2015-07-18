using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FailBell : MonoBehaviour {
	public int pressState;

	public PlayerValues playerValues;
	public DayManager dayManager;

	void Start(){
		pressState = 0;

		playerValues.Save ();
	}

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		dayManager.FailDay();

		Debug.Log ("YOU ARE DEAD. NOT BIG SURPRISE.");
	}

	public void FuckEverythingUp(){

	}
}