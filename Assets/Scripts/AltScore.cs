using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class AltScore : MonoBehaviour {
	public GameObject scoreText;
	public GameObject potentialText;

	public List<GameObject> theOrder;
	public List<GameObject> theBurg;

	public bool done = false;

	public int pointsForExactMatch = 1024;
	public int pointsPerEachPartFound = 4;

	private int theScore = 0;
	private int thePotentialScore = 0;

	// Use this for initialization
	void Start () {
		scoreText.GetComponent<Text> ().text = "0";
		potentialText.GetComponent<Text> ().text = "0";
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

		// update UI
		scoreText.GetComponent<Text> ().text = theScore.ToString();

		return theScore;
	}

	// the layout of this method should resemble the one above it (EvaluateBurger)
	public int evaluatePotentialScore(){
		thePotentialScore = 0;

		thePotentialScore += pointsForExactMatch;

		thePotentialScore += pointsPerEachPartFound * theOrder.Count;

		// update UI
		potentialText.GetComponent<Text> ().text = thePotentialScore.ToString();

		return thePotentialScore;
	}

	// SCORING METHODS & FUNCTIONS
	bool isExactMatch(){
		int i = 0;
		foreach (GameObject orderItem in theOrder) {
			if(i > theBurg.Count-1){
				// we done ran out of burg, can't possibly match
				return false;
			}

			if(!AreNamedSimilar(orderItem, theBurg[i])){
				return false;
			}
			i++;
		}
		return true;
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
			if(i > theBurg.Count-1){
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

	/*Forer's concept of how to score
	1. Evaluate if the burger has [the same] ingredient on top and bottom, OR a pair ingredient (bun top & bun bottom]
	2. Take the burger components and see if any of them are perfect matches. If they are remove them from the burger to be evaluated
	3. Find all the components in the order and tally up their % on each vector BUN CHEESE CONDIMENT MEAT VEGETABLE
	4. Find all the components in the given and tally up their % on each vector
	5. Compare how close the % are

	Example:
	[Order]
	BunTop
	Ketchup
	Mustard
	Onion
	Cheese
	Patty
	BunBottom

	[Given]
	Bread
	Mayonaise
	Lettuce
	Patty
	Bread

	1. Given has same top and bottom. + 100 points
	2. Patty is in both, remove from order and given.
	3. Order Tally
	--			BunTop	Ketchup	Mustard	Onion	Cheese	BunBottom
	[Bun] 		1,		0, 		0, 		.3,		.3,		1 			= 2.6 / 6 = 43%
	[Cheese]	.1,		0,		0,		.3,		1,		.1			= 1.5 / 6 = 25%
	[Veggie] 	.1,		.5,		.5,		1,		.3,		.1			= 2.5 / 6 = 41%
	[Condiment] 0,		1,		1,		0,		0,		0			= 2   / 6 = 33%
	[Meat]		.1,		0,		0,		.3,		.1,		.2			= .7  / 6 = 11%

	4. Given Tally
	--			Bread	Mayonaise		Lettuce		Bread
	[Bun]		1,		0,				.5,			1				= 2.5 / 4 = 62%
	[Cheese]	.1,		.5,				.4,			.1				= 1.1 / 4 = 27%
	[Veggie]	.1,		0,				1,			.1				= 1.2 / 4 = 23%
	[Condiment]	0,		1,				0,			0				= 1   / 4 = 25%
	[Meat]		.1,		0,				.5,			0				= .6  / 4 = 15%


	5. Compare how close they are
	Bun: 		abs(43 - 62) = 19
	Cheese: 	abs(25 - 27) = 2
	Veggie: 	abs(41 - 23) = 18
	Condiment: 	abs(33 - 25) = 8
	Meat: 		abs(11 - 15) = 4

	//Start with the score of the burger. Every ingredient you give determines how many points those %'s are worth.
	6/4 = 1.5

	Start from a max of 100 * ingredients
	600 points

	Follow an exponential function to see how quick the points get subtracted from that
	Bun 		((19/2)^2) 	= 90.25
	Cheese 		((2/2)^2)	= 1
	Veggie		((18/2)^2)	= 81
	Condiment 	((8/2)^2)	= 16
	Meat		((4/2)^2)	= 4

	IF final result over 100, Cap at 100

	90.25 + 1 + 81 + 16 + 4 = 192.25

	Floor value
	192

	Subtract from max points
	600 - 192 = 408

	CONGRATULATIONS WE HAVE A BURGER!
	408 out of 600 points

	*/


}