using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BurgJudgeCatcher : MonoBehaviour {
	public BurgBuilder burgBuilder;
	public AltGetOrder getOrder;
	public AltScore score;
	public PlayerValues playerValues;
	public DayValues dayValues;
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
			Debug.Log (mitem.value);
			Debug.Log (mitem.price);
			playerValues.Earn (mitem.value * satisfaction);
			moneyText.color = gainMoney;
			moneyText.text = "+$"+(mitem.value*satisfaction).ToString("F2");
			monitorShaker.GoodShake();
		} else {
			Debug.Log ("SATISFACTION: I FUCKING HATED IT!");
			Debug.Log ("VALUE: "+mitem.value);
			Debug.Log ("PRICE: "+mitem.price);
			Debug.Log ("SATISFACTION: "+satisfaction);
			Debug.Log ("FUCKED UP: "+(mitem.value * (1.0f-satisfaction)));
			playerValues.Spend (mitem.value * (1.0f-satisfaction));
			moneyText.color = loseMoney;
			moneyText.text = "-$"+(mitem.value * (1.0f-satisfaction)).ToString("F2");
			monitorShaker.BadShake();
		}
	}
}
