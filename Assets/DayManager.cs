using UnityEngine;
using System.Collections;

// DayMan(aaaaAAAAHHHH!!!)ager is meant to keep track of the day progress and stuff.
// I dislike writing "Managers" as much as the next guy but sometimes we need a deity.
public class DayManager : MonoBehaviour {
	public GameObject orderer;
	public GameObject dayvaluer;

	private AltGetOrder getOrder;
	private DayValues dayValues;

	// Use this for initialization
	void Start () {
		// eventually we're going to want to do something that isn't this, but, I'm the king for now
		getOrder = orderer.GetComponent<AltGetOrder> ();
		dayValues = dayvaluer.GetComponent<DayValues> ();

		// @TODO: Wait some sort of magical period for the day to start before assigning first order.
		StartDay ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void GetNextOrder(){
		int[] orders = dayValues.GetTodaysOrders ();
		Debug.Log ("HONK: " + dayValues.orderNumber + "/" + orders.Length);
		if (dayValues.orderNumber+1 <= orders.Length) {
			getOrder.NewOrder (dayValues.GetNextOrder ());
		} else {
			EndDay();
		}
	}

	public void EndDay(){
		Debug.Log ("DAY IS OVER, GO HOME");
		dayValues.day++;
		getOrder.TrashLastOrder ();
		StartCoroutine (Fade ());
	}

	public void StartDay(){
		Debug.Log ("DAY STARTED, GO HOME");
		dayValues.orderNumber = 0;
		GetNextOrder ();
	}

	IEnumerator Fade() {
		Debug.Log ("Waiting politely...");
		yield return new WaitForSeconds(3.0f);
		Debug.Log ("THE WAIT IS OVER!");
		StartDay ();
	}
}
