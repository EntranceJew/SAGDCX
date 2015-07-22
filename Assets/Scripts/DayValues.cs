using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DayValue {
	// this isn't ever used, but it makes me feel better, so here it is
	public string name;

	// how much you got just for being here
	public float paidWage;

	// how long you work each day
	public float workedHours;

	// range 0 to 1, if a burger is below this tolerance we get points off
	public float mistakeTolerance;

	// range 0 to 1, if a burger is below this tolerance we fail the day immediately
	public float failureTolerance;

	// not the money value, but how much a score difference will scale out to money
	public float deductionMultiplier;

	// how much each menu item is marked up, determines profit
	// for reference, mcdonalds makes $0.06 profit on each dollar menu item
	public float markup;

	// expense for every purchase made during the day
	public float gasPrice;

	// how long it takes for someone to drive to the store
	public float traffic;

	// how long it takes for someone to find everything, multiplied by the number of items ordered
	public float shoppingFatigue;

	// the menu item IDs that will be ordered 
	public int[] orders;

	// what we will receive at the beginning of each day
	public List<InventoryItem> shipments;

	// (calculated at run time) how much of each item is projected for consumption this day
	public List<InventoryItem> demand;
}

public class DayValues : MonoBehaviour {
	// THESE ARE PUBLIC, BUT THEY'RE NOT FOR YOU!
	public RMenuElement myMenuElement;

	void Start(){
		// Calculate all demands for all days.
		Dictionary<GameObject, int> dicDemand = new Dictionary<GameObject, int> ();
		int i = 0;
		foreach (DayValue dayValue in values) {
			foreach (int orderNo in dayValue.orders) {
				RMenuItem item = myMenuElement.menuItems [orderNo];
				foreach (GameObject content in item.contents) {
					int dem = 0;
					dicDemand.TryGetValue (content, out dem);
					if (dem > 0) {
						dicDemand [content]++;
					} else {
						dicDemand.Add (content, 1);
					}
				}
			}

			List<InventoryItem> theDemand = new List<InventoryItem> ();
			foreach (KeyValuePair<GameObject, int> entry in dicDemand) {
				theDemand.Add (new InventoryItem (entry.Key, entry.Value));
			}
			values[i].demand = new List<InventoryItem>(theDemand);
			i++;
		}
	}

	// WE'RE SUPPOSED TO HAVE THESE
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

	public float GetTodaysFailureTolerance(){
		return values [day].failureTolerance;
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
		// shut up for right now
		//Debug.Log ("COULDN'T FIND DEMAND FOR: " + obj);
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
