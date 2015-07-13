using UnityEngine;
using System.Collections;

public class OrderForm : MonoBehaviour {
	public GameObject inventorer;
	public GameObject playerer;
	/* 
	@TODO: establish links to dayvalues for the current cash amount
	*/
	
	private RowManager[] rows;

	private Inventory inventory;
	private PlayerValues playerValues;

	// Use this for initialization
	void Start () {
		rows = gameObject.GetComponentsInChildren<RowManager> ();
		inventory = inventorer.GetComponent<Inventory> ();
		playerValues = playerer.GetComponent<PlayerValues> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// form buttons
	public void Confirm(){
		//@TODO: Subtract from player money count.

		// buy it
		if (playerValues.CanAfford (GetTotal ())) {
			// jk lol
			Debug.Log ("MANAGER YOU CRAZY, I CAN'T AFFORD AN XBOX.");
		} else {
			// add the board values to the shipment
			playerValues.Spend(GetTotal ());
			inventory.ObtainShipment (GetDesiredShipment ());
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
	public InventoryItems[] GetDesiredShipment(){
		InventoryItems[] items = new InventoryItems[rows.Length];
		int i = 0;
		foreach (RowManager row in rows) {
			items[i] = new InventoryItems(row.represents.name, row.quantity);
			i++;
		}
		return items;
	}

	public float GetTotal(){
		float total = 0.0f;
		foreach (RowManager row in rows) {
			total += row.quantity*row.rate;
		}
		return total;
	}
}
