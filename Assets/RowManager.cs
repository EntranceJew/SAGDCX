using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RowManager : MonoBehaviour {
	public string itemName;

	public int quantity;
	public float rate;
	public int stock;
	public GameObject represents;

	private Text itemText;
	private Text quantText;
	private Text totalText;
	private Text stockText;

	// Use this for initialization
	void Start () {
		itemText = transform.Find ("ItemName").gameObject.GetComponent<Text>();
		quantText = transform.Find ("Quantity").gameObject.GetComponent<Text>();
		totalText = transform.Find ("Total").gameObject.GetComponent<Text>();
		stockText = transform.Find ("Remaining").gameObject.GetComponent<Text>();

		SetText ();
	}
	
	// Update is called once per frame
	void Update () {
		SetText ();
	}

	public void SetText(){
		itemText.text = itemName;
		quantText.text = quantity.ToString();
		totalText.text = "$"+(quantity * rate).ToString ("F2");
		stockText.text = "( "+stock.ToString()+" )";
	}

	public void Add(){
		quantity++;
	}

	public void Subtract(){
		quantity--;
	}
}
