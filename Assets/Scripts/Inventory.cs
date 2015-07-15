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
	public FoodLookup fl;

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

	public bool Has(string name){
		foreach (InventoryItem item in stock) {
			if(item.represents.name == name){
				return item.quantity >= 1;
			}
		}
		return false;
	}

	public void Remove(string name, int quantity){
		int i = 0;
		foreach (InventoryItem item in stock) {
			if(item.represents.name == name){
				stock[i].quantity -= quantity;
			}
			i++;
		}
	}

	public bool Add(GameObject tRepresents, int quantity){
		string[] arr = tRepresents.name.Split('(');
		string itemName = fl.GetGameObject (arr [0]).name;

		int i = 0;
		foreach (InventoryItem item in stock) {
			if(item.represents.name == itemName){
				stock[i].quantity += quantity;
				return false;
			}
			i++;
		}
		Debug.Log ("Added item that was completely new: " + itemName);
		stock.Add (new InventoryItem (fl.GetGameObject(arr[0]), quantity));
		return true;
	}

	public void ObtainShipment(List<InventoryItem> incoming){
		foreach (InventoryItem item in incoming) {
			Add (item.represents, item.quantity);
		}
	}
}
