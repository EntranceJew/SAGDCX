using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerValues {
	public string name;
	
	// range 0 to 1;
	public int dayNumber;

	public float cash;

	public InventoryItems[] stock;
}

[System.Serializable]
public class DayValue {
	public string name;

	// range 0 to 1;
	public float mistakeTolerance;

	public InventoryItems[] shipments;
	// projected demands
}

public class DayValues : MonoBehaviour {
	public List<DayValue> values; // = new List<DayValue>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
