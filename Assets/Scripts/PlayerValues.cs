using UnityEngine;
using System.Collections;

[System.Serializable]
public class PlayerValues : MonoBehaviour {
	// the player name won't be used, it's just here as a formality.
	public string name;
	
	// if this number becomes bigger than the size of DayValues then all hell will break loose
	public int dayNumber;

	// how much money the player has at any time, this will impact purchases and deductions
	public float cash;

	// here is where all the everything ever lives
	public Inventory inventory;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool CanAfford(float dosh){
		return cash >= dosh;
	}
}
