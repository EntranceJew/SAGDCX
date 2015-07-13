using UnityEngine;
using System.Collections;

[System.Serializable]
public class InventoryItems {
	public string name;
	public int quantity;

	public InventoryItems(string tName, int tQuantity){
		name = tName;
		quantity = tQuantity;
	}
}

public class Inventory : MonoBehaviour {

	public Inventory(InventoryItems[] inStock){
		stock = inStock;
	}

	public InventoryItems[] stock;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool Has(string name){
		foreach (InventoryItems item in stock) {
			if(item.name == name){
				return item.quantity >= 1;
			}
		}
		return false;
	}

	public void Remove(string name, int quantity){
		int i = 0;
		foreach (InventoryItems item in stock) {
			if(item.name == name){
				stock[i].quantity -= quantity;
			}
			i++;
		}
	}

	public void Add(string name, int quantity){
		int i = 0;
		foreach (InventoryItems item in stock) {
			if(item.name == name){
				stock[i].quantity += quantity;
			}
			i++;
		}
	}

	public void ObtainShipment(InventoryItems[] incoming){
		foreach (InventoryItems item in incoming) {
			Add (item.name, item.quantity);
		}
	}
}
