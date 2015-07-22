using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BurgJudgeCatcher : MonoBehaviour {
	public BurgBuilder burgBuilder;
	public AltGetOrder getOrder;
	public AltScore score;
	public PlayerValues playerValues;
	public DayValues dayValues;
	public DayManager dayManager;
	public Text moneyText;
	public Shaker monitorShaker;

	public Color gainMoney = new Color( 83.0f/255.0f, 255.0f/255.0f, 77.0f/255.0f);
	public Color loseMoney = new Color(255.0f/255.0f,  77.0f/255.0f, 77.0f/255.0f);

	// Use this for initialization
	void Start () {
		moneyText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Food") {
			Debug.Log ("CAUGHT SOME FOOD, Y'ALL");
			// @TODO: Display scores on the TV, time it out. You know, do what the people like.
			score.EvaluateBurger(getOrder.CurrentOrder().completeOrder, burgBuilder.GetChildParts());
			playerValues.AddScores(score.GetAchievedScore(), score.GetMaxScore());
			GetDollarDollarBillsYall();
			// get down from there you rapscallion
			burgBuilder.UnHoist();
		} else {
			Debug.Log ("WHAT TO HECK: " + col.gameObject.name);
		}
	}

	void GetDollarDollarBillsYall(){
		float satisfaction = (float)score.GetAchievedScore () / (float)score.GetMaxScore ();

		RMenuItem mitem = getOrder.lastMenuItem;

		if (satisfaction > dayValues.GetTodaysMistakeTolerance ()) {
			Debug.Log ("SATISFACTION: I LIKED IT!");
			Debug.Log ("VALUE: "+ mitem.value);
			Debug.Log ("PRICE: "+mitem.price);
			Debug.Log ("MARKUP: " + dayValues.GetTodaysMarkup());
			Debug.Log ("SATISFACTION: " + satisfaction);

			float baseItemValue = mitem.value + dayValues.GetTodaysMarkup();

			playerValues.Earn (baseItemValue * satisfaction);
			moneyText.color = gainMoney;
			moneyText.text = "+$" + (baseItemValue * satisfaction).ToString ("F2");
			monitorShaker.GoodShake ();
		} else if (satisfaction > dayValues.GetTodaysFailureTolerance ()) {
			Debug.Log ("SATISFACTION: I FUCKING HATED IT!");
			Debug.Log ("VALUE: " + mitem.value);
			Debug.Log ("PRICE: " + mitem.price);
			Debug.Log ("MARKUP: " + dayValues.GetTodaysMarkup());
			Debug.Log ("SATISFACTION: " + satisfaction);

			float baseItemValue = mitem.value + dayValues.GetTodaysMarkup();

			// get the amount the burger is worth minus dissatisfaction
			float fuckupAmount = baseItemValue * (1.0f - satisfaction);
			Debug.Log ("FUCKUP PASS1: " + fuckupAmount);
			// flip it to see how much our dissatisfaction cost
			fuckupAmount = baseItemValue - fuckupAmount;
			Debug.Log ("FUCKUP PASS2: " + fuckupAmount);
			// multiply it by penalization
			fuckupAmount *= dayValues.GetTodaysDeductionMultiplier();
			Debug.Log ("FUCKUP PASS3: " + fuckupAmount);
			playerValues.Spend (fuckupAmount);
			moneyText.color = loseMoney;
			moneyText.text = "-$" + (fuckupAmount).ToString ("F2");
			monitorShaker.BadShake ();
		} else {
			Debug.Log ("YOU FUCKED UP SO BAD");
			// we lose
			dayManager.LoseDay();
			moneyText.color = loseMoney;
			moneyText.text = "YOU'RE FIRED";
			monitorShaker.BadShake();
		}
	}
}
