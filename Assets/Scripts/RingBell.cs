using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	public BurgBuilder burgBuilder;
	public AltGetOrder getOrder;
	public AltScore score;
	public DayManager dayManager;

	void OnMouseDown() {
		//Check order : THIS SHOULD BE REPLACED WITH ACTUAL CHECK ORDER SCRIPTS, this is just for human evaluation right now.
		score.EvaluateBurger(GetOrder(), GetBurger());
		//If it doesn't have this, then it's a debug bell.
		if (gameObject.GetComponent<SpawnBurgerComponent> () != null) {
			// @TODO: Move the plate up.
			// Teleport it.
			gameObject.GetComponent<SpawnBurgerComponent> ().SpawnJudgeBurger(burgBuilder.gameObject);
			// Trash the existing burger.
			burgBuilder.TrashBurger ();
			// @TODO: Move the plate down.
			dayManager.GetNextOrder ();
		}
		AudioSource audio = GetComponent<AudioSource> ();
		audio.Play ();
	}

	List<GameObject> GetOrder() {
		//Get order from the lightbulb
		return getOrder.CurrentOrder().completeOrder;
	}

	List<GameObject> GetBurger() {
		//Get list of objects in the burgbuilder
		return burgBuilder.GetChildParts();
	}
}