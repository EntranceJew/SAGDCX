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
	public DayValues dayValues;
	public InputField inputField;
	
	private int stock;
	private float rate;

	public Text itemText;
	public Text totalText;
	public Text stockText;
	public Text demandText;

	private AudioSource aud;

	void PlaySound (){
		if (aud != null) {
			aud.Stop ();
			aud.Play ();
		}
	}

	// Use this for initialization
	void Start () {
		aud = GetComponent<AudioSource> ();
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

		int amtLeft = PlayerValues.pv.inventory.HasHowMany (represents.name);
		if (amtLeft <= 0) {
			stockText.text = "None!";
		} else {
			stockText.text = amtLeft.ToString ();
		}

		int dmndValue = dayValues.GetTodaysDemandFor (represents);

		if (dmndValue <= 0) {
			demandText.text = "None!";
		} else {
			demandText.text = dmndValue.ToString ();
		}
	}

	public void Add(){
		PlaySound ();
		quantity++;
	}

	public void Subtract(){
		if (quantity > 0) {
			PlaySound ();
			quantity--;
		}
	}

	public void SetQuantity(){
		PlaySound ();
		string quant = inputField.text;
		int.TryParse (quant, out quantity);
		if(quantity < 0){
			quantity = 0;
		}
	}
}