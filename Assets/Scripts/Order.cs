using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order {
	public List<GameObject> completeOrder = new List<GameObject>();

	//RANDOM order for empty constructor
	public Order (List<GameObject> inList) {
		Debug.Log (inList);
		completeOrder = inList;
		completeOrder.Reverse();
	}
	
	public List<GameObject> RandomIngredients(List<GameObject> allList) {
		//Get a random number of ingredients to use
		List<GameObject> output = new List<GameObject>();
		int ingredientsToUse = Random.Range (3, 7);
		while (ingredientsToUse > 0) {
			output.Add (allList[Random.Range (0,allList.Count)]);
			ingredientsToUse--;
		}

		return output;
	}
	


	public string toString() {
		string output = null;
		foreach (GameObject obj in completeOrder) {
			output += obj.transform.name + " ";
		}
		return output;
	}
}
