using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Grocery {
	public string name;
	public InventoryItems[] result;
	public float value;
	public float costMult;
}

public class RowManager : MonoBehaviour {
	//public string itemName;

	public int quantity;
	public GameObject represents;
	
	private int stock;
	private float rate;

	private Text itemText;
	private Text quantText;
	private Text totalText;
	private Text stockText;
	private Text demandText;

	// Use this for initialization
	void Start () {
		itemText = transform.Find ("ItemName").gameObject.GetComponent<Text>();
		quantText = transform.Find ("Quantity").gameObject.GetComponent<Text>();
		totalText = transform.Find ("Total").gameObject.GetComponent<Text>();
		stockText = transform.Find ("Supply").gameObject.GetComponent<Text>();
		demandText = transform.Find ("Demand").gameObject.GetComponent<Text>();

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
