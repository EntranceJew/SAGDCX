using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// DayMan(aaaaAAAAHHHH!!!)ager is meant to keep track of the day progress and stuff.
// I dislike writing "Managers" as much as the next guy but sometimes we need a deity.
public class DayManager : MonoBehaviour {
	public Image fadePicture;
	public float fadeSpeed;
	public float count = 0;
	public bool pictureBlack = true;
	public bool shouldBlack = true;


	public AltGetOrder getOrder;
	public DayValues dayValues;
	public Inventory inventory;
	public PlayerValues playerValues;
	
	// Use this for initialization
	void Start () {
		fadePicture.color = Color.black;
		// eventually we're going to want to do something that isn't this, but, I'm the king for now

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
		/* CHECKLIST OF SHIT TO DO ON DAY RESET
		 * [ ] Destroy all active food
		 * [ ] Clear the order.
		 * [ ] Load State.
		 * [ ] 
		*/
		// @TODO: Make sure TrashLastOrder doesn't do anything unexpected.

		/*
		// Trash all food, refund the real ones. (Invalid, won't work with incoming shipments.)
		foreach (GameObject obj in got) {
			Food fd = obj.GetComponent<Food>();
			if(fd != null && !fd.isFake){
				inventory.Add(obj, 1);
			} else if(fd == null){
				Debug.Log (obj);
			}
			Destroy(obj);
		}
		*/

		// Trash all food, everywhere.
		getOrder.TrashLastOrder ();
		GameObject[] got = GameObject.FindGameObjectsWithTag ("Food");
		foreach (GameObject obj in got) {
			Destroy (obj);
		}

		// Reset inventory and such.
		playerValues.Load ("autosave_day_"+dayValues.day);

		// Clear the order.
		dayValues.ResetDayValues ();
		getOrder.NewOrder (dayValues.GetNextOrder ());

		// Fade to black, come back.
		shouldBlack = true;
		StartCoroutine (Fade ());
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
		// @TODO: Maybe save this as an autosave instead of ontop of the existing save?
		playerValues.Save ("autosave_day_"+dayValues.day);
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