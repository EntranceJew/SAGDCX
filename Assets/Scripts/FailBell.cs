using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FailBell : MonoBehaviour {
	public DayManager dayManager;

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		dayManager.WinGame();
		Debug.Log ("YOU WIN. IS NOT BIG SURPRISE.");
	}
}