using UnityEngine;
using System.Collections;

public class TutorialHandler : MonoBehaviour {
	public bool showTutorials = true;

	//public FoodLookup fl; //maybe if we define it we'll wait until it's done?
	public DayValues dayValues;
	public GameObject lookAt;
	public GameObject mouseObject;

	public RingBell ringBell;

	// Use this for initialization
	void Start () {
		if (PlayerValues.pv.dayNumber == 0) {
			if (showTutorials) {
				// == DISABLE THINGS THAT NEED DISABLING
				// disable the bell until first order is prepared
				ringBell.shouldDing = false;
				// == EMPHASIS PEEPING
				Camera.main.GetComponent<LookTowards> ().SetTarget (lookAt.transform);
			} else {
				PlayerValues.pv.inventory.ObtainShipment (dayValues.GetTodaysShipment ());
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShipmentDone(){
		LookTowards lt = Camera.main.GetComponent<LookTowards> ();
		if (PlayerValues.pv.dayNumber == 0) {
			if (lt.Target == lookAt.transform && showTutorials) {
				lt.SetTarget (mouseObject.transform);
				ringBell.shouldDing = true;
			}
		}
	}
}
