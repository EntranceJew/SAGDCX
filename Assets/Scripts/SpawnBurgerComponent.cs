using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnBurgerComponent : MonoBehaviour {
	public GameObject judgeSpawnPos;
	public List<GameObject> burgToJudge = new List<GameObject>();
	public RawImage burgJudgeCanvas;
	float count = 0;


	void Update() {
		if (burgToJudge.Count-1 > -1) {
			burgJudgeCanvas.enabled = true;
			count += Time.deltaTime;
			if (count > 1) {
				SpawnBurgerGameObject (burgToJudge [0]);
				burgToJudge.RemoveAt (0);
				count = 0;
			}
		} 
	
	}

	public void GetCurrentBurger(List<GameObject> currentBurg) {
		burgToJudge = currentBurg;
	}
	
	public void SpawnBurgerGameObject(GameObject burgObject) {
		GameObject tempBurgObject = Instantiate (burgObject, judgeSpawnPos.transform.position, Quaternion.Euler (new Vector3 (90, Random.Range (0, 360), 0))) as GameObject;

	}


}
