using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlateMachine : MonoBehaviour {
	public List<GameObject> spawnList;

	private int listPos;

	// Use this for initialization
	void Start () {
		listPos = 0;
	}
	
	// Update is called once per frame
	void Update () {
		foreach(PlateThing plate in this.gameObject.GetComponentsInChildren<PlateThing>()){
			if(plate.SpawnThing(spawnList[listPos])){
				listPos += 1;
				if(listPos >= spawnList.Count){
					listPos = 0;
				}
			}
		}
	}
}
