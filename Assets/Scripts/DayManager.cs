using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// DayMan(aaaaAAAAHHHH!!!)ager is meant to keep track of the day progress and stuff.
// I dislike writing "Managers" as much as the next guy but sometimes we need a deity.
public class DayManager : MonoBehaviour {
	public GameObject orderer;
	public GameObject dayvaluer;
	public GameObject inventorer;


	public Image fadePicture;
	public float fadeSpeed;
	public float count = 0;
	public bool pictureBlack = true;
	public bool shouldBlack = true;


	private AltGetOrder getOrder;
	private DayValues dayValues;
	private Inventory inventory;



	// Use this for initialization
	void Start () {
		fadePicture.color = Color.black;
		// eventually we're going to want to do something that isn't this, but, I'm the king for now
		getOrder = orderer.GetComponent<AltGetOrder> ();
		dayValues = dayvaluer.GetComponent<DayValues> ();
		inventory = inventorer.GetComponent<Inventory> ();

		// @TODO: Wait some sort of magical period for the day to start before assigning first order.
		StartDay ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shouldBlack && !pictureBlack) {
			count += Time.deltaTime * fadeSpeed;
			fadePicture.color = Color.Lerp(fadePicture.color, Color.black, count);
			if (count > 1) {
				count = 0;
				pictureBlack = true;
			}
		}

		if (!shouldBlack && pictureBlack) {
			count += Time.deltaTime * fadeSpeed;
			fadePicture.color = Color.Lerp(fadePicture.color, Color.clear, count);
			if (count > 1) {
				count = 0;
				shouldBlack = false;
				pictureBlack = false;
			}
		}
	}

	public void GetNextOrder(){
		int[] orders = dayValues.GetTodaysOrders ();
		Debug.Log ("DAY "+dayValues.day+", ORDER " + dayValues.orderNumber + "/" + orders.Length);
		if (dayValues.orderNumber+1 <= orders.Length) {
			getOrder.NewOrder (dayValues.GetNextOrder ());
		} else {
			EndDay();
		}
	}

	public void FailDay(){
		// @TODO: Make sure TrashLastOrder doesn't do anything unexpected.
		getOrder.TrashLastOrder ();

		GameObject[] got = GameObject.FindGameObjectsWithTag ("Food");
	}

	public void EndDay(){
		Debug.Log ("DAY "+dayValues.day+" IS OVER, GO HOME");
		shouldBlack = true;
		dayValues.day++;
		getOrder.TrashLastOrder ();
		StartCoroutine (Fade ());
	}

	public void StartDay(){
		Debug.Log ("DAY "+dayValues.day+" STARTED, GO HOME");
		shouldBlack = false;
		dayValues.orderNumber = 0;
		GetNextOrder ();
	}

	public void AddTodaysShipment(){
		Debug.Log ("ADDING DAY " + dayValues.day + "'S SHIPMENT");
		inventory.ObtainShipment(dayValues.GetTodaysShipment ());
	}

	IEnumerator Fade() {
		Debug.Log ("Waiting politely...");
		yield return new WaitForSeconds(3.0f);
		Debug.Log ("THE WAIT IS OVER!");
		StartDay ();
	}
}