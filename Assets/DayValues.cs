using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerValues {
	public string name;
	
	// range 0 to 1;
	public int dayNumber;

	public float cash;

	public InventoryItems[] stock;
}

[System.Serializable]
public class DayValue {
	// this isn't ever used, but it makes me feel better, so here it is
	public string name;

	// how much you got just for being here
	public float paidWage;

	// range 0 to 1, if a burger is below this tolerance we get points off
	public float mistakeTolerance;

	// not the money value, but how much a score difference will scale out to money
	public float deductionMultiplier;

	// expense for every purchase made during the day
	public float gasPrice;

	// what we will receive at the beginning of each day
	public InventoryItems[] shipments;

	// the menu item that will be ordered 
	public int[] orders;
}

public class DayValues : MonoBehaviour {
	public List<DayValue> values; // = new List<DayValue>();

	public int day = 0;
	public int orderNumber = 0;

	public int GetNextOrder(){
		return orderNumber++;
	}

	public int[] GetTodaysOrders(){
		return values [day].orders;
	}

	public InventoryItems[] GetTodaysShipment(){
		return values [day].shipments;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
