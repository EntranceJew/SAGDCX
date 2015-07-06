using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IngredientList : MonoBehaviour {
	public List<GameObject> ingredientList = new List<GameObject>();

	GameObject GetIngredientByName(string inName) {
		foreach (GameObject ing in ingredientList) {
			if (ing.transform.name == inName) {
				return ing;
			}
		}

		return ingredientList[0];
	}
}
