using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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
	public Shipment ship;
	public GameObject bigCanvasObject;
	public GameObject canvasTextObject;

	public GraphicRaycaster graphicRaycaster;
	public GameObject failureGUI;
	public GameObject victoryGUI;
	
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
		Debug.Log ("DAY "+PlayerValues.pv.dayNumber+", ORDER " + dayValues.orderNumber + "/" + orders.Length);
		if (dayValues.orderNumber+1 <= orders.Length) {
			getOrder.NewOrder (dayValues.GetNextOrder ());
		} else {
			EndDay();
		}
	}

	public void WinGame(){
		isDayActive = false;
		RefundAllFoodInScene ();
		dayValues.ResetDayValues ();
		shouldBlack = true;
		EnableVictoryGUI ();
	}

	public void LoseDay(){
		// disable spawners
		isDayActive = false;
		// trash food
		DestroyAllFoodInScene ();
		// clear orders
		dayValues.ResetDayValues ();
		// reload player values
		PlayerValues.pv.Load ("autosave_day_"+PlayerValues.pv.dayNumber);
		// blank screen
		shouldBlack = true;
		// @TODO: Silence audio.
		// show the UI
		EnableFailureGUI ();
	}

	public void EnableFailureGUI(){
		failureGUI.SetActive (true);
		graphicRaycaster.enabled = true;
	}

	public void DisableFailureGUI(){
		failureGUI.SetActive (false);
		graphicRaycaster.enabled = false;
	}

	public void EnableVictoryGUI(){
		victoryGUI.SetActive (true);
		graphicRaycaster.enabled = true;
	}
	
	public void DisableVictoryGUI(){
		victoryGUI.SetActive (false);
		graphicRaycaster.enabled = false;
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


		// Fade to black, come back.
		shouldBlack = true;
		StartCoroutine (Fade (false));
	}

	public void EndDay(){
		Debug.Log ("DAY "+PlayerValues.pv.dayNumber+" IS OVER, GO HOME");
		getOrder.TrashLastOrder ();
		RefundAllFoodInScene ();
		isDayActive = false;
		shouldBlack = true;
		PlayerValues.pv.dayNumber++;
		if (PlayerValues.pv.dayNumber > dayValues.values.Count - 1) {
			// we reset to zero so things don't fucking break if we're on a day that isn't valid (lastday+1)
			dayValues.ResetToZero ();
			Debug.Log ("WON GAME BY REACHING LAST DAY");
			WinGame ();
			return;
		}
		StartCoroutine (Fade (true));
	}

	public void RetryDay(){
		DisableFailureGUI ();
		StartCoroutine (Fade (false));
	}

	public void RestartGame(){
		DisableFailureGUI ();
		dayValues.ResetToZero ();
		PlayerValues.pv.Load ("autosave_day_0");
		StartCoroutine (Fade (false));
	}

	public void NewGamePlus(){
		DisableVictoryGUI ();
		dayValues.ResetToZero ();
		StartCoroutine (Fade (true));
	}
	
	public void QuitGame(){
		Debug.Log ("QUIT, QUIT, GET OUT, I WANT TO LEAVE, LET ME SPLIT");
		//Application.Quit ();
		// if we get this far, then that means that we're not a build
		Application.LoadLevel ("SAGDCX");
	}

	public void VictoryQuitGame(){
		Debug.Log ("We made it through the darkness to the light, \nOh, how we fought yet still we won the fight [...]");
		//Application.Quit ();
		// if we get this far, then that means that we're not a build
		Application.LoadLevel ("SAGDCX");
	}

	public void StartDay(bool doSave){
		Debug.Log ("DAY "+PlayerValues.pv.dayNumber+" STARTED, GO HOME");

		shouldBlack = false;
		// @TODO: Maybe save this as an autosave instead of ontop of the existing save?
		if (doSave) {
			PlayerValues.pv.Save ("autosave_day_" + PlayerValues.pv.dayNumber);
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
				PlayerValues.pv.inventory.Add (obj, 1);
				Destroy (obj);
			} else {
				Debug.Log (obj);
			}
		}
	}
	
	public void AddTodaysShipment(){
		Debug.Log ("ADDING DAY " + PlayerValues.pv.dayNumber + "'S SHIPMENT");

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
		List<GameObject> tempList = new List<GameObject> ();
		float waitBase = 3.0f;
		waitBase += ShowEndOfDayText (tempList);

		yield return new WaitForSeconds (waitBase);

		foreach (GameObject obj in tempList) {
			Destroy (obj);
		}
		tempList.Clear ();

		StartDay (doSave);
	}

	float ShowEndOfDayText(List<GameObject> destroyMeLater) {
		int day = PlayerValues.pv.dayNumber-1; //ghetto fix HELLS YEAH!
		//instantiate "Today was day X" text
		GameObject temp = Instantiate (canvasTextObject) as GameObject;
		temp.transform.SetParent (bigCanvasObject.transform);
		temp.name = "TitleText";
		temp.transform.localPosition = new Vector3 (0, 240, 0);
		temp.GetComponent<Text> ().text = "Goodbye day " + (day + 1) + " we will miss you"; //day + 1 because we don't want people to see "day 0" that sounds stupid.
		destroyMeLater.Add (temp);

		int i = 0;
		if (PlayerValues.pv.scores.Count - 1 >= day) {
			int total = PlayerValues.pv.scores [day].scores.Count;
			//Instantiate a text line for each order
			foreach (OrderScore os in PlayerValues.pv.scores[day].scores) {
				temp = Instantiate (canvasTextObject) as GameObject;
				temp.transform.SetParent (bigCanvasObject.transform);
				temp.name = "ScoreRowText" + i;
				temp.transform.localPosition = new Vector3 (0, (48 * i) / 2, 0);
				temp.GetComponent<Text> ().text = "Order " + i + " got " + os.value + " out of " + os.maxValue;
				destroyMeLater.Add (temp);
				i++;
			}
			Debug.Log ("a: "+ ((float)total * 1.5f));
			Debug.Log ("b: "+ ((float)(total * 1.5f)));
			return (float)total * 1.5f;
		} else {
			Debug.LogWarning ("ATTEMPTED TO INDEX SCORES THAT DIDN'T EXIST.");
			return 0.0f;
		}
	}
}