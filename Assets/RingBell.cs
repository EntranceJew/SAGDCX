using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingBell : MonoBehaviour {
	GameObject lb;
	GameObject burgBuild;
	float bestSum = 0;
	List<GameObject> bestOrder;

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
		HighestScoringOrder(order.completeOrder, burgObjects);

		string str = "";

		for (int i = 0; i < order.completeOrder.Count; i++) {
			str += order.completeOrder[i].GetComponent<Food>().foodName + " filled with " + bestOrder[i].GetComponent<Food>().foodName + " ";
		}

		str += SumResult (order.completeOrder, bestOrder);

		Debug.Log (str);

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

	void HighestScoringOrder(List<GameObject> originalOrder, List<GameObject> originalZone) {
		List<GameObject> available = originalZone;


		while (available.Count < originalOrder.Count) {
			available.Add (fillerFood);
		}
		List<GameObject> empty = new List<GameObject> ();
		Permute (originalOrder, empty, available);
	}

	void Permute(List<GameObject> orderRequested, List<GameObject> usedList, List<GameObject> unusedYet) {
		if (unusedYet.Count == 0) {


			float sum = SumResult(orderRequested, usedList);

			if (sum > bestSum) {
				bestSum = sum;
				bestOrder = usedList;
			}

			return;
		}

		for (int i = 0; i < unusedYet.Count; i++) {
			List<GameObject> tempUsedList = new List<GameObject>(usedList);
			List<GameObject> tempUnusedList = new List<GameObject>(unusedYet);
			tempUsedList.Add(unusedYet[i]);
			tempUnusedList.RemoveAt(i);
			Permute(orderRequested, tempUsedList, tempUnusedList);
		}
	}

	float SumResult(List<GameObject> orderRequested, List<GameObject> orderChecking) {
		float totalScore = 0;
		for (int i = 0; i < orderRequested.Count; i++) {
			GameObject request = orderRequested[i];
			GameObject check = orderChecking[i];
			totalScore += CheckTwoIngredients(request, check);
		}

		return totalScore;
	}

	float CheckTwoIngredients (GameObject requested, GameObject recieved) {
		float score = 0;

		if (requested.GetComponent<Food>().foodName == recieved.GetComponent<Food>().foodName) {
			score = 150;
		} else {
			float percentScore = GetValueGivenEnum(requested.GetComponent<Food>().foodType, recieved);
			score = 100 * percentScore;
		}


		return score;
	}

	float GetValueGivenEnum (FoodEnum givenEnum, GameObject recieved) {
		FoodCategories fc = recieved.GetComponent<Food> ().foodCategories;
		switch (givenEnum) {
		default: 
			return fc.bun;
		}
	}
}