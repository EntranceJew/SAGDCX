using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AltGetOrder : MonoBehaviour {
	
	GameObject canvas;
	GameObject ingListObject;
	Text instruction;
	Order order;
	public GameObject rMenu;
	public GameObject example;
	public float upVar;

	public RandomGrunts rg;
	
	public bool rand;

	public RMenuItem lastMenuItem;

	private RMenuElement rMenuElement;
		
	void Start () {
		rMenuElement = rMenu.GetComponent<RMenuElement> ();
	}

	public void NewOrder(int orderIndex) {
		//Get the order
		OrderCreation (orderIndex);
	}

	void OrderCreation(){
		OrderCreation (Random.Range (0, rMenuElement.menuItems.Count - 1));
	}

	void OrderCreation(int theIndex) {
		canvas = GameObject.FindGameObjectWithTag("MainCanvas");

		lastMenuItem = rMenuElement.menuItems[theIndex];

		order = new Order (lastMenuItem.contents);
		instruction = canvas.transform.Find("OrderText").gameObject.GetComponent<Text> ();
		instruction.text = "Your order is: " + "["+lastMenuItem.name+"]\n" + order.toString ();

		rg.SetStartTime (Time.time);

		MakeOrderExample ();
	}
	
	void MakeOrderExample() {
		//Clean out previous order
		TrashLastOrder ();

		Debug.Log ("REV UP THOSE FRYERS");
		
		//Take order
		int ingredient = 0;
		while (ingredient < order.completeOrder.Count) {
			//Find where to drop it.
			Vector3 finalPos = example.transform.position;
			finalPos.y += ingredient * upVar;
			//Instantiate ingredient
			GameObject addedIngredient = (GameObject) Instantiate(order.completeOrder[ingredient], finalPos, Quaternion.identity);
			addedIngredient.GetComponent<Food>().isFake = true;
			addedIngredient.transform.SetParent(example.transform);
			addedIngredient.transform.localRotation = Quaternion.Euler(-90,0,0);
			//Repeat
			ingredient++;
		}
	}
	
	public void TrashLastOrder() {
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
