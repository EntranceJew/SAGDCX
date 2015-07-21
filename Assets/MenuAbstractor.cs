using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// In my defense, I don't have time to worry about Unity's absurd refusal to handle dictionaries during this competition.

[System.Serializable]
public class MenuAbstraction {
	// this is never used, it just helps us figure out where we our in this list shaped hell
	public string name;
	public List<GameObject> items;
}

[System.Serializable]
public class MenuAbstractions {
	// the object we want to be
	public GameObject represents;
	// the list of lists of things we could be
	public List<MenuAbstraction> results;
}

public class MenuAbstractor : MonoBehaviour {
	// @TODO: Due to this being deterministic, the index positions needs to save to PlayerValues.

	public List<MenuAbstractions> abstractions;

	// this is used to lookup an abstraction set from a given gameobject
	private Dictionary<GameObject, MenuAbstractions> objToAbs = new Dictionary<GameObject, MenuAbstractions>();

	// this is used to track the current index of the MenuAbstractions
	private Dictionary<GameObject, int> objToInt = new Dictionary<GameObject, int>();

	// Use this for initialization
	void Start () {
		foreach (MenuAbstractions mAbstractor in abstractions) {
			objToAbs.Add (mAbstractor.represents, mAbstractor);
			objToInt.Add (mAbstractor.represents, 0);
		}
	}

	public List<GameObject> GetPossibilityFor(GameObject menuItem){
		int objIndex = objToInt [menuItem];
		List<GameObject> possibilities = objToAbs[menuItem].results[objIndex].items;
		objToInt [menuItem] = objIndex + 1;
		if (objIndex + 1 > objToAbs [menuItem].results.Count-1) {
			objToInt[menuItem] = 0;
		}
		return possibilities;
	}
}
