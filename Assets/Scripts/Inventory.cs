using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class InventoryItem {
	public GameObject represents;
	public int quantity;

	public InventoryItem(GameObject tRepresents, int tQuantity){
		represents = tRepresents;
		quantity = tQuantity;
	}

	public SerializableInventoryItem Serialize(){
		return new SerializableInventoryItem (represents.name, quantity);
	}
}

[System.Serializable]
public class SerializableInventoryItem {
	public string name;
	public int quantity;


	public SerializableInventoryItem(InventoryItem item){
		name = item.represents.name;
		quantity = item.quantity;
	}

	public SerializableInventoryItem(string tName, int tQuantity){
		name = tName;
		quantity = tQuantity;
	}
}

public class Inventory : MonoBehaviour {
	public Inventory(List<InventoryItem> inStock){
		stock = inStock;
	}

	public List<InventoryItem> stock;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int HasHowMany(string name){
		foreach (InventoryItem item in stock) {
			if(item.represents.name == name){
				return item.quantity;
			}
		}
		return 0;
	}

	public bool Has(string name){
		foreach (InventoryItem item in stock) {
			if(item.represents.name == name){
				return item.quantity >= 1;
			}
		}
		return false;
	}

	public int Remove(string name, int quantity){
		int i = 0;
		foreach (InventoryItem item in stock) {
			if(item.represents.name == name){
				int remaining = stock[i].quantity -= quantity;
				if(remaining <= 0){
					stock.Remove(item);
				}
				return remaining;
			}
			i++;
		}
		return -1;
	}

	public bool Add(GameObject tRepresents, int quantity){
		// Don't add nothing.
		if (quantity <= 0) {
			return false;
		}

		string[] arr = tRepresents.name.Split('(');
		string itemName = FoodLookup.fl.GetGameObject (arr [0]).name;

		int i = 0;
		foreach (InventoryItem item in stock) {
			if(item.represents.name == itemName){
				stock[i].quantity += quantity;
				return false;
			}
			i++;
		}
		// shut up
		//Debug.Log ("Added item that was completely new: " + itemName);
		stock.Add (new InventoryItem (FoodLookup.fl.GetGameObject(arr[0]), quantity));
		return true;
	}

	public bool Set(GameObject tRepresents, int quantity){
		string[] arr = tRepresents.name.Split('(');
		string itemName = FoodLookup.fl.GetGameObject (arr [0]).name;

		if (quantity <= 0) {
			return Remove (itemName, quantity) > 0;
		}

		int i = 0;
		foreach (InventoryItem item in stock) {
			if(item.represents.name == itemName){
				stock[i].quantity = quantity;

				return false;
			}
			i++;
		}
		Debug.Log ("Set stock of item that was completely new: " + itemName);
		stock.Add (new InventoryItem (FoodLookup.fl.GetGameObject(arr[0]), quantity));
		return true;
	}

	public void ObtainShipment(List<InventoryItem> incoming){
		foreach (InventoryItem item in incoming) {
			Add (item.represents, item.quantity);
		}
	}

	public int GetTotal(){
		int total = 0;
		foreach(InventoryItem item in stock){
			total += item.quantity;
		}
		return total;
	}
}