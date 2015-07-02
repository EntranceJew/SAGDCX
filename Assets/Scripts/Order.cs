using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order {
	public List<string> completeOrder = new List<string>();

	public List<string> allIngredients = new List<string>() {
		"BunBottom", "Patty", "Cheese", "Lettuce", "Tomato", "Onion", "Pickles", "BunTop"
	};

	//RANDOM order for empty constructor
	public Order () {
		completeOrder = RandomIngredients ();
	}

	public Order (string input) {
		if (input == "Normal") {
			completeOrder.Add ("BunBottom");
			completeOrder.Add ("Patty");
			completeOrder.Add ("Cheese");
			completeOrder.Add ("Lettuce");
			completeOrder.Add ("Onion");
			completeOrder.Add ("Tomato");
			completeOrder.Add ("Pickles");
			completeOrder.Add ("BunTop");
		} else {
			completeOrder = RandomIngredients();
		}
	}

	public List<string> RandomIngredients() {
		//Get a random number of ingredients to use
		List<string> output = new List<string>();
		int ingredientsToUse = Random.Range (3, 7);
		while (ingredientsToUse > 0) {
			output.Add (allIngredients[Random.Range (0,allIngredients.Count)]);
			ingredientsToUse--;
		}

		return output;
	}
	


	public string toString() {
		string output = null;
		foreach (string str in completeOrder) {
			output += str + " ";
		}
		return output;
	}
}
