using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrashBell : MonoBehaviour {
	public GameObject burger;

	private BurgBuilder burgBuilder;

	void Start(){
		burgBuilder = burger.GetComponent<BurgBuilder> ();
	}

	void OnMouseDown() {
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();

		burgBuilder.TrashBurger ();
	}
}