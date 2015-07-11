using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	public GameObject burger;
	public GameObject scorer;
	public GameObject orderer;

	private BurgBuilder burgBuilder;
	private AltGetOrder getOrder;
	private AltScore score;

	void Start(){
		burgBuilder = burger.GetComponent<BurgBuilder> ();
		score = scorer.GetComponent<AltScore> ();
		getOrder = orderer.GetComponent<AltGetOrder> ();
	}

	void OnMouseDown() {
		//Check order : THIS SHOULD BE REPLACED WITH ACTUAL CHECK ORDER SCRIPTS, this is just for human evaluation right now.
		score.EvaluateBurger(GetOrder(), GetBurger());
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