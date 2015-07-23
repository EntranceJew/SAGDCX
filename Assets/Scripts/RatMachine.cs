using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RatMachine : MonoBehaviour {
	public List<GameObject> spawnList;
	public Inventory inventory;
	public DayManager dayManager;
	
	private int listPos;
	
	// Use this for initialization
	void Start () {
		listPos = 0;
		if (inventory == null) {
			inventory = PlayerValues.pv.ratsInHouse;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (dayManager.isDayActive) {
			foreach (PlateThing plate in this.gameObject.GetComponentsInChildren<PlateThing>()) {
				if (inventory.Has (spawnList [listPos].name)) {
					if (plate.SpawnThing (spawnList [listPos])) {
						inventory.Remove (spawnList [listPos].name, 1);
						listPos += 1;
					}
				} else {
					listPos += 1;
				}
				if (listPos >= spawnList.Count) {
					listPos = 0;
				}
			}
		}
	}
}
