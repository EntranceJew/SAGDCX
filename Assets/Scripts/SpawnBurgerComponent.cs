using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnBurgerComponent : MonoBehaviour {
	public GameObject judgeSpawnPos;
	public Transform burgBuild;
	public List<GameObject> burgToJudge = new List<GameObject>();
	public RawImage burgJudgeCanvas;
	float count = 0;
	float twoCount = 0;

	void Update() {
		if (burgToJudge.Count > 0) {
			burgJudgeCanvas.enabled = true;
			count += Time.deltaTime;
			if (count > 1) {
				SpawnBurgerGameObject (burgToJudge [0]);
				burgToJudge.RemoveAt (0);
				count = 0;
			}
		} 
	
	}

	void OnMouseDown() {

		burgToJudge = GetCurrentBurger ();

	}

	List<GameObject> GetCurrentBurger() {
		List<GameObject> what = new List<GameObject>();

		foreach (Transform t in burgBuild) {
			if (t.name != "PartZone") {
				what.Add (t.gameObject);
			}
		}

		return what;
	}
	
	public void SpawnBurgerGameObject(GameObject burgObject) {
		Instantiate (burgObject, judgeSpawnPos.transform.position, Quaternion.Euler (new Vector3 (90, Random.Range (0, 360), 0)));
	}
}
