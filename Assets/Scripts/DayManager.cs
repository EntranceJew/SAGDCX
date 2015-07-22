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

	public bool isDayActive = false;


	public AltGetOrder getOrder;
	public DayValues dayValues;
	public Inventory inventory;
	public PlayerValues playerValues;
	public Shipment ship;

	public GraphicRaycaster graphicRaycaster;
	public GameObject failureGUI;
	
	// Use this for initialization
	void Start () {
		fadePicture.color = Color.black;
		// eventually we're going to want to do something that isn't this, but, I'm the king for now

		// @TODO: Wait some sort of magical period for the day to start before assigning first order.
		StartDay (true);
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

	public void LoseDay(){
		// disable spawners
		isDayActive = false;
		// trash food
		DestroyAllFoodInScene ();
		// clear orders
		dayValues.ResetDayValues ();
		// blank screen
		shouldBlack = true;
		// @TODO: Silence audio.
		// show the UI
		failureGUI.SetActive (true);
		// enable clicking on UI
		graphicRaycaster.enabled = true;

	}

	public void EnableFailureGUI(){

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

		*/
		// Disable food spawning.
		isDayActive = false;

		// Trash all food, everywhere.
		DestroyAllFoodInScene ();

		// Clear the order.
		dayValues.ResetDayValues ();
		//getOrder.NewOrder (dayValues.GetNextOrder ());

		// Reset inventory and such.
		playerValues.Load ("autosave_day_"+dayValues.day);

		// Fade to black, come back.
		shouldBlack = true;
		StartCoroutine (Fade (false));
	}

	public void EndDay(){
		Debug.Log ("DAY "+dayValues.day+" IS OVER, GO HOME");
		isDayActive = false;
		shouldBlack = true;
		dayValues.day++;
		getOrder.TrashLastOrder ();
		StartCoroutine (Fade (true));
	}

	public void StartDay(bool doSave){
		Debug.Log ("DAY "+dayValues.day+" STARTED, GO HOME");

		shouldBlack = false;
		// @TODO: Maybe save this as an autosave instead of ontop of the existing save?
		if (doSave) {
			playerValues.Save ("autosave_day_" + dayValues.day);
		}
		dayValues.orderNumber = 0;
		AddTodaysShipment ();
		GetNextOrder ();
		isDayActive = true;
	}

	public void DestroyAllFoodInScene(){
		getOrder.TrashLastOrder ();
		GameObject[] got = GameObject.FindGameObjectsWithTag ("Food");
		foreach (GameObject obj in got) {
			Destroy (obj);
		}
	}

	public void RefundAllFoodInScene(){
		GameObject[] got = GameObject.FindGameObjectsWithTag ("Food");
		foreach (GameObject obj in got) {
			Food fd = obj.GetComponent<Food> ();
			if (fd != null && !fd.isFake) {
				inventory.Add (obj, 1);
				Destroy (obj);
			} else {
				Debug.Log (obj);
			}
		}
	}
	
	public void AddTodaysShipment(){
		Debug.Log ("ADDING DAY " + dayValues.day + "'S SHIPMENT");

		ship.inventory.ObtainShipment (dayValues.GetTodaysShipment());
		ship.GoGetIt(0.0f);
	}

	IEnumerator Fade(bool doSave) {
		Debug.Log ("Waiting politely...");
		yield return new WaitForSeconds(3.0f);
		Debug.Log ("THE WAIT IS OVER!");
		// we do this here so that we're sure the scene is faded before we execute
		RefundAllFoodInScene ();
		Debug.Log ("FADING DAY IN...");
		StartDay (doSave);
	}
}