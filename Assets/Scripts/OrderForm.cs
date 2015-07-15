using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class OrderForm : MonoBehaviour {
	public GameObject inventorer;
	public GameObject playerer;
	public GameObject dayvaluer;
	/* 
	@TODO: establish links to dayvalues for the current cash amount
	*/
	
	private RowManager[] rows;

	private Inventory inventory;
	private PlayerValues playerValues;
	private DayValues dayValues;

	private Color positiveMoney = new Color(0.1953125f,1.0f,0.1953125f);
	private Color negativeMoney = new Color(0.7109375f,0.1640625f,0.0f);

	public GameObject moneyer;
	public GameObject gaserer;
	public GameObject totaler;

	private Text moneyTotal;
	private Text gasValue;
	private Text totalValue;

	// Use this for initialization
	void Start () {
		rows = gameObject.GetComponentsInChildren<RowManager> ();
		inventory = inventorer.GetComponent<Inventory> ();
		playerValues = playerer.GetComponent<PlayerValues> ();
		dayValues = dayvaluer.GetComponent<DayValues> ();

		moneyTotal = moneyer.GetComponent<Text>();
		gasValue = gaserer.GetComponent<Text>();
		totalValue = totaler.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		if (playerValues.cash >= 0.0f) {
			moneyTotal.color = positiveMoney;
		} else {
			moneyTotal.color = negativeMoney;
		}
		moneyTotal.text = "$"+playerValues.cash.ToString ("F2");

		gasValue.text = "$" + dayValues.GetTodaysGasPrice ().ToString ("F2");

		totalValue.text = "$" + (GetTotal () + dayValues.GetTodaysGasPrice ()).ToString ("F2");
	}

	// form buttons
	public void Confirm(){
		// buy it
		float totalExpense = GetTotal () + dayValues.GetTodaysGasPrice ();
		if (playerValues.CanAfford (totalExpense)) {
			// add the board values to the shipment
			playerValues.Spend(totalExpense);
			inventory.ObtainShipment (GetDesiredShipment ());
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
			total += row.quantity*row.represents.GetComponent<Food>().price;
		}
		return total;
	}
}
