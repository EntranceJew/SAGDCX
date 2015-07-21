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
		evaluatePotentialScore (order.Count);

		Debug.Log ("The Score starting out : " + theScore);

		//See if burg has same top and bottom ingredient
		theScore += TopBottomBonus (burg);
		Debug.Log ("The Score after top bottom bonus : " + theScore);

		//See if there are perfect matches, lifting this straight from isExactMatch but since I need the lists to now have those ingredients no longer I need to basically lift the function.
		List<GameObject> orderLeft;
		List<GameObject> burgLeft;
		theScore += FindPerfectIngredients (order, burg, out orderLeft, out burgLeft);
		Debug.Log ("The Score after perfect ingredients : " + theScore);

		//evaluate Foodiness of orderLeft

		FoodCategories orderLeftFoodSum = EvaluateFoodiness (orderLeft);

		//evaluate Foodiness of burgLeft

		FoodCategories burgLeftFoodSum = EvaluateFoodiness (burgLeft);

		//evaluate scores.

		theScore += EvaluateFoodinessScores (orderLeft.Count, orderLeftFoodSum, burgLeftFoodSum);
		Debug.Log ("The Score after evaluation : " + theScore);

		// update UI
		scoreText.GetComponent<Text> ().text = theScore.ToString();

		return theScore;
	}

	// the layout of this method should resemble the one above it (EvaluateBurger)
	public int evaluatePotentialScore(int burgCount){
		thePotentialScore = 0;

		/*
		thePotentialScore += pointsForExactMatch;

		thePotentialScore += pointsPerEachPartFound * theOrder.Count;
		*/

		thePotentialScore  = burgCount * 125; 	//Score for perfect burger substitutions
		thePotentialScore += 50; 				//Top Bottom Bonus Score

		// update UI
		potentialText.GetComponent<Text> ().text = thePotentialScore.ToString();

		return thePotentialScore;
	}

	// SCORING METHODS & FUNCTIONS
	int TopBottomBonus(List<GameObject> burgIn) {
		int bonusScoreTopBottom = 50;
		if (burgIn.Count < 1) {
			//OF COURSE a burger with less than two things has the top and bottom the same, THAT'S CHEATING NO BONUS FOR YOU!
			return 0;
		}

		GameObject top 		= burgIn [0];
		GameObject bottom 	= burgIn [burgIn.Count - 1];


		if (AreNamedSimilar (top, bottom)) {
			//CONGRATULATIONS!
			return bonusScoreTopBottom;
		}

		if (top.GetComponent<Food>().foodName == "Bun" && bottom.GetComponent<Food>().foodName == "Bun") {
			//CONGERATULATON
			return bonusScoreTopBottom;
		}

		//welp, we tried
		return 0;
	}

	FoodCategories EvaluateFoodiness (List<GameObject> evalList) {
		FoodCategories tempFC;
		FoodCategories output = new FoodCategories();
		foreach (GameObject obj in evalList) {
			tempFC = obj.GetComponent<Food>().foodCategories;

			output.bun 			+= tempFC.bun;
			output.cheese 		+= tempFC.cheese;
			output.condiment 	+= tempFC.condiment;
			output.meat 		+= tempFC.meat;
			output.vegetable 	+= tempFC.vegetable;
		}
		return output;
	}

	int EvaluateFoodinessScores (int ListSize, FoodCategories order, FoodCategories burg) {
		int output = ListSize * 100;
		FoodCategories temp = new FoodCategories();

		temp.bun 		= Mathf.Abs(order.bun 		- burg.bun);
		temp.cheese		= Mathf.Abs(order.cheese 	- burg.cheese);
		temp.condiment 	= Mathf.Abs(order.condiment - burg.condiment);
        temp.meat 		= Mathf.Abs(order.meat 		- burg.meat);
        temp.vegetable 	= Mathf.Abs(order.vegetable - burg.vegetable);

		temp.bun 		*= .5f;
		temp.cheese 	*= .5f;
		temp.condiment 	*= .5f;
		temp.meat 		*= .5f;
		temp.vegetable 	*= .5f;

		temp.bun 		*= temp.bun;
		temp.cheese 	*= temp.cheese;
		temp.condiment	*= temp.condiment;
		temp.meat 		*= temp.meat;
		temp.vegetable 	*= temp.vegetable;

		if (temp.bun > 100)				temp.bun = 100;
		if (temp.cheese > 100)			temp.cheese = 100;
		if (temp.condiment > 100)		temp.condiment = 100;
		if (temp.meat > 100)			temp.meat = 100;
		if (temp.vegetable > 100)		temp.vegetable = 100;

		output = Mathf.FloorToInt (temp.bun + temp.cheese + temp.condiment + temp.meat + temp.vegetable);

		return output;
	}

	int FindPerfectIngredients(List<GameObject> inOrder, List<GameObject> inBurg, out List<GameObject> outOrder, out List<GameObject> outBurg) {
		int output = 0;
		int perfectMatchScore = 125;
		outOrder = new List<GameObject> (inOrder);
		outBurg = new List<GameObject> (inBurg);

		foreach (GameObject a in inOrder) {

			foreach (GameObject b in inBurg) {

				if (AreNamedSimilar(a,b)) {

					output += perfectMatchScore;
					outOrder.Remove(a);

					if (outBurg.Contains(b)) {

						outBurg.Remove(b);

					}
				}
			}
		}

		return output;
	}

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