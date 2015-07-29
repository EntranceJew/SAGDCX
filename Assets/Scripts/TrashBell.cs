using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrashBell : MonoBehaviour {
	public BurgBuilder burgBuilder;
	public DayManager dayManager;

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		burgBuilder.TrashBurger ();

		dayManager.LoseDay ("You \"git commit -m 'soduko'\"");
	}
}