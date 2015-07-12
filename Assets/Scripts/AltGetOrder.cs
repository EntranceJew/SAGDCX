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
	
	public bool rand;

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

		RMenuItem rMenuItem = rMenuElement.menuItems[theIndex];

		order = new Order (rMenuItem.contents);
		instruction = canvas.transform.Find("OrderText").gameObject.GetComponent<Text> ();
		instruction.text = "Your order is: " + "["+rMenuItem.name+"]\n" + order.toString ();

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
