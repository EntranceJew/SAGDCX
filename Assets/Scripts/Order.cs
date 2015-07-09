using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Order {
	public List<GameObject> completeOrder = new List<GameObject>();
	public List<GameObject> flippedOrder = new List<GameObject> ();

	//RANDOM order for empty constructor
	public Order (List<GameObject> inList) {
		Debug.Log (inList);
		// is it really that goddamn difficult to flip a list, really
		// from: http://forum.unity3d.com/threads/reversing-a-list.79972/
		GameObject[] listArray = new GameObject[inList.Count];
		completeOrder = new List<GameObject>(inList);
		inList.CopyTo (listArray);
		completeOrder = new List<GameObject> (listArray);
		completeOrder.Reverse ();
		flippedOrder = new List<GameObject> (listArray);
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
		// Not sure why we do this because we already flipped completeOrder when we made him.
		//completeOrder = completeOrder.Reverse ();
		//List<GameObject> flippedList = completeOrder.Reverse ();
		foreach (GameObject obj in flippedOrder) {
			output += obj.transform.name + ",\n";
		}
		// Trim the newline and comma we don't need.
		output = output.Substring(0, output.Length-2);
		return output;
	}
}
