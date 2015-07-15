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
	
	private int stock;
	private float rate;

	public Text itemText;
	public Text quantText;
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
		quantText.text = quantity.ToString();
		totalText.text = "$"+(quantity * rate).ToString ("F2");
		stockText.text = stock.ToString();
	}

	public void Add(){
		quantity++;
	}

	public void Subtract(){
		quantity--;
	}
}