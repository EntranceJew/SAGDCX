using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlateMachine : MonoBehaviour {
	public List<GameObject> spawnList;
	public GameObject inventory;
	public DayManager dayManager;

	private int listPos;
	private Inventory inv;

	// Use this for initialization
	void Start () {
		listPos = 0;
		inv = inventory.GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (dayManager.isDayActive) {
			foreach (PlateThing plate in this.gameObject.GetComponentsInChildren<PlateThing>()) {
				if (inv.Has (spawnList [listPos].name)) {
					if (plate.SpawnThing (spawnList [listPos])) {
						inv.Remove (spawnList [listPos].name, 1);
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
