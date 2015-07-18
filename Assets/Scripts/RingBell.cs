using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	public BurgBuilder burgBuilder;
	public AltGetOrder getOrder;
	public AltScore score;

	void OnMouseDown() {
		//Check order : THIS SHOULD BE REPLACED WITH ACTUAL CHECK ORDER SCRIPTS, this is just for human evaluation right now.
		score.EvaluateBurger(GetOrder(), GetBurger());
		if (gameObject.GetComponent<SpawnBurgerComponent> () != null) {
			gameObject.GetComponent<SpawnBurgerComponent> ().SpawnJudgeBurger(burgBuilder.gameObject);
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