using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OrderForm : MonoBehaviour {
	/* 
	@TODO: establish links to dayvalues for the current cash amount
	*/
	
	private RowManager[] rows;



	private Color positiveMoney = new Color(0.1953125f,1.0f,0.1953125f);
	private Color negativeMoney = new Color(0.7109375f,0.1640625f,0.0f);

	public MenuAbstractor menuAbs;
	public Shipment ship;
	public DayValues dayValues;

	public Text moneyTotal;
	public Text gasValue;
	public Text totalValue;

	// Use this for initialization
	void Start () {
		rows = gameObject.GetComponentsInChildren<RowManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerValues.pv.cash >= 0.0f) {
			moneyTotal.color = positiveMoney;
		} else {
			moneyTotal.color = negativeMoney;
		}
		moneyTotal.text = "$"+PlayerValues.pv.cash.ToString ("F2");

		gasValue.text = "$" + dayValues.GetTodaysGasPrice ().ToString ("F2");

		totalValue.text = "$" + (GetTotal () + dayValues.GetTodaysGasPrice ()).ToString ("F2");
	}

	// form buttons
	public void Confirm(){
		// buy it
		float totalExpense = GetTotal () + dayValues.GetTodaysGasPrice ();
		if (PlayerValues.pv.CanAfford (totalExpense)) {
			// add the board values to the shipment
			PlayerValues.pv.Spend(totalExpense);
			ship.inventory.ObtainShipment (GetAbstractedShipment ());
			float shopTime = dayValues.GetTodaysTraffic() * 2; // uphill, both ways
			shopTime += ship.inventory.GetTotal() * dayValues.GetTodaysShoppingFatigue();
			ship.GoGetIt(shopTime);
		} else {
			// jk lol
			Debug.Log ("MANAGER YOU CRAZY, I CAN'T AFFORD AN XBOX.");
		}

		// done
		Clear ();
	}

	public void Clear(){
		foreach (RowManager row in rows) {
			row.quantity = 0;
		}
	}

	// helpers
	public List<InventoryItem> GetAbstractedShipment(){
		Dictionary<GameObject, int> absShipment = new Dictionary<GameObject, int> ();
		foreach (RowManager row in rows) {
			for(int i = 0; i < row.quantity; i++){
				List<GameObject> abs = menuAbs.GetPossibilityFor(row.represents);
				foreach(GameObject go in abs){
					int temp;
					if(absShipment.TryGetValue(go, out temp)){
						absShipment[go]++;
					} else {
						Debug.Log ("INITIALIZED ABSTRACT SHIPMENT");
						absShipment.Add (go, 1);
					}
				}
			}
		}

		List<InventoryItem> invShipment = new List<InventoryItem> ();
		foreach (KeyValuePair<GameObject, int> entry in absShipment) {
			invShipment.Add (new InventoryItem(entry.Key, entry.Value));
		}

		return invShipment;
	}

	public List<InventoryItem> GetDesiredShipment(){
		List<InventoryItem> items = new List<InventoryItem>();
		foreach (RowManager row in rows) {
			items.Add(new InventoryItem(row.represents, row.quantity));
		}
		return items;
	}

	public float GetTotal(){
		float total = 0.0f;
		foreach (RowManager row in rows) {
			if(row.quantity > 0){
				total += row.quantity*row.represents.GetComponent<Food>().price;
			}
		}
		return total;
	}
}
