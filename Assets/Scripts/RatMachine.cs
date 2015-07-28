using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RatMachine : MonoBehaviour {
	public List<GameObject> spawnList;
	public DayManager dayManager;
	
	private int listPos;
	
	// Use this for initialization
	void Start () {
		listPos = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (dayManager.isDayActive) {
			foreach (RatThing plate in this.gameObject.GetComponentsInChildren<RatThing>()) {
				if (PlayerValues.pv.ratsInHouse.Has (spawnList [listPos].name)) {
					if (plate.SpawnThing (spawnList [listPos])) {
						PlayerValues.pv.ratsInHouse.Remove (spawnList [listPos].name, 1);
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
