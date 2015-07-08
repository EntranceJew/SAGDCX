using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Score : MonoBehaviour {
	float bestSum = -100;
	List<GameObject> bestOrder;
	public GameObject fillerFood;

	public void EvaluateBurger(List<GameObject> order, List<GameObject> burg) {
		bestSum = -100;
		bestOrder = new List<GameObject> ();
		if (burg.Count != 0) {
			HighestScoringOrder (order, burg);


			string str = "";


			for (int i = 0; i < order.Count; i++) {
				str += order [i].GetComponent<Food> ().foodName + " filled with " + bestOrder [i].GetComponent<Food> ().foodName + " ";
			}

		
			str += SumResult (order, bestOrder);
		
			Debug.Log (str);
		}
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
		case FoodEnum.Bun:
			return fc.bun;
		case FoodEnum.Cheese:
			return fc.cheese;
		case FoodEnum.Condiment:
			return fc.condiment;
		case FoodEnum.Meat:
			return fc.meat;
		case FoodEnum.Vegetable:
			return fc.vegetable;
		default: 
			return fc.bun;
		}
	}
}
