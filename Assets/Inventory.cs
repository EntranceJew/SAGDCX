using UnityEngine;
using System.Collections;

[System.Serializable]
public struct InventoryItems {
	public string name;
	public int quantity;
}

public class Inventory : MonoBehaviour {

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
}
