using UnityEngine;
using System.Collections;

public class BurgJudgeCatcher : MonoBehaviour {
	public BurgBuilder burgBuilder;
	public AltGetOrder getOrder;
	public AltScore score;
	public PlayerValues playerValues;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Food") {
			Debug.Log ("CAUGHT SOME FOOD, Y'ALL");
			// @TODO: Display scores on the TV, time it out. You know, do what the people like.
			score.EvaluateBurger(getOrder.CurrentOrder().completeOrder, burgBuilder.GetChildParts());
			playerValues.AddScores(score.GetAchievedScore(), score.GetMaxScore());
			// get down from there you rapscallion
			burgBuilder.UnHoist();
		} else {
			Debug.Log ("WHAT TO HECK: " + col.gameObject.name);
		}
	}
}
