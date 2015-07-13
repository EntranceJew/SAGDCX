using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RMenuItem {
	public string name;
	public List<GameObject> contents;
	// the menu price
	public float price;
	// the calculated worth via 
	public float value;

	// get the net worth of all elements that compose the menu item
	public float CalculateValue(){
		foreach (GameObject go in contents) {
			value += go.GetComponent<Food> ().price;
		}

		return value;
	}
}

public class RMenuElement : MonoBehaviour {
	public List<RMenuItem> menuItems;

	// Use this for initialization
	void Start () {
		// calculate the net worth of all menu items
		foreach (RMenuItem rmu in menuItems) {
			rmu.CalculateValue();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
