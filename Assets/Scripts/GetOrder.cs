using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetOrder : MonoBehaviour {
	GameObject canvas;
	Text instruction;
	Order order;
	public GameObject example;
	public float upVar;
	
	void Start () {
		//New Order
		NewOrder ();
	}

	public void NewOrder() {
		//Get the order
		OrderCreation ();
		
		//Make the order appear in the box
		MakeOrderExample ();
	}
	
	void OrderCreation() {
		canvas = GameObject.FindGameObjectWithTag("MainCanvas");

		instruction = canvas.transform.Find("OrderText").gameObject.GetComponent<Text> ();
		instruction.text = "Your order is: " + order.toString ();

		MakeOrderExample ();
	}
	
	void MakeOrderExample() {
		//Clean out previous order
		TrashLastOrder ();
		
		
		//Take order
		int ingredient = 0;
		while (ingredient < order.completeOrder.Count) {
			//Find where to drop it.
			Vector3 finalPos = example.transform.position;
			finalPos.y += ingredient * upVar;
			//Instantiate ingredient
			GameObject addedIngredient = (GameObject) Instantiate(order.completeOrder[ingredient], finalPos, Quaternion.identity);
			addedIngredient.transform.SetParent(example.transform);
			addedIngredient.transform.localRotation = Quaternion.Euler(-90,0,0);
			//Repeat
			ingredient++;
		}
	}
	
	void TrashLastOrder() {
		foreach (Transform child in example.transform) {
			if (child != example.transform) {
				//Ok good we aren't deleting the actual example burger gameobject container
				Destroy (child.gameObject);
			}
		}
	}

	public Order CurrentOrder() {
		return order;
	}
}
