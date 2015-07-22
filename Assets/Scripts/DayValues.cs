using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	// how long it takes for someone to drive to the store
	public float traffic;

	// how long it takes for someone to find everything, multiplied by the number of items ordered
	public float shoppingFatigue;

	// what we will receive at the beginning of each day
	public List<InventoryItem> shipments;

	// how much of each item is projected for consumption this day
	public List<InventoryItem> demand;

	// the menu item that will be ordered 
	public int[] orders;

	// how much each menu item is marked up, determines profit
	// for reference, mcdonalds makes $0.06 profit on each dollar menu item
	public float markup;
}

public class DayValues : MonoBehaviour {
	public List<DayValue> values; // = new List<DayValue>();

	public int day = 0;
	public int orderNumber = 0;

	public void ResetDayValues(){
		orderNumber = 0;
	}

	public int GetNextOrder(){
		return orderNumber++;
	}

	public DayValue GetToday(){
		return values [day];
	}

	public float GetTodaysPaidWage(){
		return values [day].paidWage;
	}

	public float GetTodaysMistakeTolerance(){
		return values [day].mistakeTolerance;
	}

	public float GetTodaysDeductionMultiplier(){
		return values [day].deductionMultiplier;
	}

	public float GetTodaysGasPrice(){
		return values [day].gasPrice;
	}

	public float GetTodaysTraffic(){
		return values [day].traffic;
	}

	public float GetTodaysShoppingFatigue(){
		return values [day].shoppingFatigue;
	}

	public List<InventoryItem> GetTodaysShipment(){
		return values [day].shipments;
	}

	public List<InventoryItem> GetTodaysDemand(){
		return values [day].demand;
	}

	public int GetTodaysDemandFor(GameObject obj){
		foreach (InventoryItem inv in values [day].demand) {
			if(inv.represents == obj){
				return inv.quantity;
			}
		}
		Debug.Log ("COULDN'T FIND DEMAND FOR: " + obj);
		return -1;
	}

	public int[] GetTodaysOrders(){
		return values [day].orders;
	}

	public float GetTodaysMarkup(){
		return values [day].markup;
	}

	public float CalculateValue(float price){
		return values [day].markup+price;
	}
}
