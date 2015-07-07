using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	GameObject lb;
	GameObject burgBuild;

	public GameObject fillerFood;

	void Start() {
		lb = GameObject.Find ("Lightbulb");
		burgBuild = GameObject.Find ("BurgBuilder");
	}

	void OnMouseDown() {
		//Get order from the lightbulb
		Order order = lb.GetComponent<GetOrder>().CurrentOrder ();
		//Get list of objects in the burgbuilder
		List<GameObject> burgObjects = new List<GameObject> ();
		foreach (Transform t in burgBuild.transform) {
			if (t.name != "PartZone") {
				burgObjects.Add(t.gameObject);
			}
		}

		//Check order : THIS SHOULD BE REPLACED WITH ACTUAL CHECK ORDER SCRIPTS, this is just for human evaluation right now.
		//List<GameObject> highestOverall = HighestScoringOrder(order.completeOrder, burgObjects);

		//Trash burger
		foreach (Transform t in burgBuild.transform) {
			if (t.name != "PartZone") {
				Destroy (t.gameObject);
			}
		}

		//Allow new burg to be made
			foreach (Transform t in burgBuild.transform) {
			if (t.name == "PartZone") {
				t.GetComponent<PartZone>().enabled = true;
			}
		}


		//Don't forget to tell the lightbulb that a new order is needed to be gotten.
		lb.GetComponent<GetOrder> ().NewOrder ();
	}

	List<GameObject> HighestScoringOrder(List<GameObject> originalOrder, List<GameObject> originalZone) {
		List<GameObject> output = new List<GameObject>();
		List<GameObject> available = originalZone;
		List <GameObject> tempAvailable;
		List<GameObject> used;
		float highScore = 0;

		while (available.Count < originalOrder.Count) {
			available.Add (fillerFood);
		}





		return output;
	}

	float SumResult(List<GameObject> orderRequested, List<GameObject> orderChecking) {
		float result = 0f;


		for (int i = 0; i < orderRequested.Count; i++) {
			result += CheckTwoIngredients(orderRequested[i], orderChecking[i]);
		}

		return result;
	}

	float CheckTwoIngredients (GameObject requested, GameObject recieved) {
		float result = 0;
		float MaxScore = 100f;
		float PerfectBonus = 50f;

		if (requested.name == recieved.name) {
			result = MaxScore + PerfectBonus;
		} else {
			result = MaxScore * GetValueGivenEnum(requested.GetComponent<Food>().foodType, recieved);
		}

		return result;
	}

	float GetValueGivenEnum (FoodEnum givenEnum, GameObject recieved) {
		FoodCategories fc = recieved.GetComponent<Food> ().foodCategories;
		switch (givenEnum) {
		case FoodEnum.Cheese:
			return fc.cheese;
		case FoodEnum.Meat:
			return fc.patty;
		case FoodEnum.Vegetable:
			return fc.lettuce;
		default: 
			return fc.bunBottom;
		}
	}
}