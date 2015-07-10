using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AltScore : MonoBehaviour {
	public List<GameObject> theOrder;
	public List<GameObject> theBurg;

	public bool done = false;

	public int pointsForExactMatch = 1024;
	public int pointsPerEachPartFound = 4;

	private int theScore = 0;
	private int thePotentialScore = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// UTILITY BURGSEARCH FUNCTIONS
	bool AreNamedSimilar(GameObject a, GameObject b){
		string aName = a.name.Split('(')[0];
		string bName = b.name.Split('(')[0];

		return (aName == bName);
	}
	
	int WhereIs(List<GameObject> haystack, GameObject needle){
		// amazing lazy-assed function overloading aboard
		return WhereIs (haystack, needle, 0);
	}

	int WhereIs(List<GameObject> haystack, GameObject needle, int start){
		int i = start;
		foreach (GameObject hay in haystack) {
			if(AreNamedSimilar(hay, needle)){
				return i;
			}
		}
		return -1;
	}

	int TotalOccurances(List<GameObject> haystack, GameObject needle){
		int occurances = 0;
		foreach (GameObject hay in haystack) {
			if(AreNamedSimilar(hay, needle)){
				occurances++;
			}
		}
		return occurances;
	}

	int[] FindOccurances(List<GameObject> haystack, GameObject needle){
		List<int> occurances = new List<int> ();
		for(int i = 0; i < haystack.Count; i++){
			if(AreNamedSimilar(haystack[i], needle)){
				occurances.Add(i);
			}
		}
		int[] total = new int[occurances.Count];
		occurances.CopyTo (total);
		return total;
	}

	// THE MAIN PAPA FUNCTION
	public int EvaluateBurger(List<GameObject> order, List<GameObject> burg) {
		theScore = 0;
		theOrder = order;
		theBurg = burg;

		// let everyone else know what is possible
		evaluatePotentialScore ();

		if (isExactMatch ()) {
			theScore += pointsForExactMatch;
		}

		theScore += pointsPerEachPartFound * numPartsFound();

		return theScore;
	}

	// the layout of this method should resemble the one above it (EvaluateBurger)
	public int evaluatePotentialScore(){
		thePotentialScore = 0;

		thePotentialScore += pointsForExactMatch;

		thePotentialScore += pointsPerEachPartFound * theOrder.Count;

		return thePotentialScore;
	}

	// SCORING METHODS & FUNCTIONS
	bool isExactMatch(){
		int i = 0;
		foreach (GameObject orderItem in theOrder) {
			if(theBurg[i] != null){
				if(AreNamedSimilar(orderItem, theBurg[i])){
					return true;
				}
			}
			i++;
		}
		return false;
	}

	int numPartsFound(){
		int i = 0;
		int found = 0;

		// make an array to keep track of which elements have already been allocated
		bool[] used = new bool[theBurg.Count];
		for(int j = 0; j < theBurg.Count; j++){
			used[j] = false;
		}

		foreach (GameObject orderItem in theOrder) {
			if(theBurg[i] == null){
				// we done ran out of burg
				break;
			}

			// find everywhere one of this thing is, then see if we used it yet
			// FindOccurances returns indexes in theBurg
			int[] locations = FindOccurances(theBurg, orderItem);
			foreach(int burgIndex in locations){
				if(!used[burgIndex]){
					// that part wasn't used in something else
					found++;
					used[burgIndex] = true;
					break;
				}
			}

			i++;
		}

		return found;
	}

	// GETTERS AND SETTERS
	public float GetMinScore () {
		return theScore;
	}

	public float GetMaxScore () {
		return thePotentialScore;
	}
}