using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Grocery {
	public string name;
	public List<InventoryItem> result;
	public float value;
	public float costMult;
}

public class RowManager : MonoBehaviour {
	//public string itemName;

	public int quantity;
	public GameObject represents;
	public Inventory inventory;
	public InputField inputField;
	
	private int stock;
	private float rate;

	public Text itemText;
	public Text totalText;
	public Text stockText;
	public Text demandText;

	// Use this for initialization
	void Start () {
		rate = represents.GetComponent<Food> ().price;

		SetText ();
	}
	
	// Update is called once per frame
	void Update () {
		SetText ();
	}

	public void SetText(){
		itemText.text = represents.name;
		inputField.text = quantity.ToString();
		totalText.text = "$"+(quantity * rate).ToString ("F2");

		int amtLeft = inventory.HasHowMany (represents.name);
		if (amtLeft <= 0) {
			stockText.text = "None!";
		} else {
			stockText.text = amtLeft.ToString ();
		}

		demandText.text = "???";
	}

	public void Add(){
		quantity++;
	}

	public void Subtract(){
		quantity--;
	}

	public void SetQuantity(){
		string quant = inputField.text;
		int.TryParse (quant, out quantity);
	}
}