using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetOrder : MonoBehaviour {
	
	GameObject canvas;
	Text instruction;
	Order order;
	public GameObject example;
	public float upVar;
	
	public bool rand;
	
	
	//INGREDIENTS!
	public GameObject BunBottom;
	public GameObject Patty;
	public GameObject Cheese;
	public GameObject Lettuce;
	public GameObject Tomato;
	public GameObject Onion;
	public GameObject Pickles;
	public GameObject BunTop;
	public GameObject Rat;
	
	
	void Start () {
		//Get the order
		OrderCreation ();
		
		//Make the order appear in the box
		MakeOrderExample ();
	}
	
	void OrderCreation() {
		canvas = GameObject.FindGameObjectWithTag("MainCanvas");
		
		if (rand) {
			order = new Order ();
		} else {
			order = new Order ("Normal");
		}
		instruction = canvas.transform.Find("OrderText").gameObject.GetComponent<Text> ();
		instruction.text = "Your order is: " + order.toString ();
	}
	
	void MakeOrderExample() {
		//Clean out previous order
		TrashLastOrder ();
		
		
		//Take order
		int ingredient = 0;
		while (ingredient <= order.completeOrder.Count) {
			//Look at ingredient
			GameObject ing = FindGameObjectForIngredient(order.completeOrder[ingredient]);
			//Find where to drop it.
			Vector3 finalPos = example.transform.position;
			finalPos.y += ingredient * upVar;
			//Instantiate ingredient
			GameObject addedIngredient = Instantiate(ing, finalPos, Quaternion.identity) as GameObject;
			addedIngredient.transform.SetParent(example.transform);
			addedIngredient.transform.localRotation = Quaternion.Euler(-90,0,0);
			//Repeat
			ingredient++;
		}
	}
	
	GameObject FindGameObjectForIngredient(string prefabName) {
		switch (prefabName) {
		case "BunBottom":
			return BunBottom;
		case "Patty":
			return Patty;
		case "Cheese":
			return Cheese;
		case "Lettuce":
			return Lettuce;
		case "Tomato":
			return Tomato;
		case "Onion": 
			return Onion;
		case "Pickles":
			return Pickles;
		case "BunTop":
			return BunTop;
		default:
			return Rat;
		}
	}
	
	void TrashLastOrder() {
		foreach (Transform child in example.transform) {
			if (child != example.transform) {
				//Ok good we aren't deleting the actual example burger gameobject container
				Destroy (child);
			}
		}
	}
}
