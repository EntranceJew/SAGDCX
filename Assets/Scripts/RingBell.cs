using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	public BurgBuilder burgBuilder;
	public AudioClip inactiveClip;
	public bool shouldDing = true;

	private AudioClip initClip;
	private AudioSource aud;

	void Start(){
		aud = GetComponent<AudioSource> ();
		initClip = aud.clip;
	}

	void OnMouseDown() {
		if (burgBuilder.CanHoist () && shouldDing) {
			aud.clip = initClip;
			// Lift it up, someone will tell him to come down eventually.
			burgBuilder.Hoist();
		} else {
			aud.clip = inactiveClip;
		}
		aud.Play ();
	}
}