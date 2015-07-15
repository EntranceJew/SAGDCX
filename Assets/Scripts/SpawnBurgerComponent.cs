using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnBurgerComponent : MonoBehaviour {
	public GameObject judgeSpawnPos;
	public List<GameObject> burgToJudge = new List<GameObject>();
	public RawImage burgJudgeCanvas;

	public void SpawnJudgeBurger(List<GameObject> inBurger) {
		//Take order
		int ingredient = 0;
		while (ingredient < inBurger.Count) {
			//Find where to drop it.
			Vector3 finalPos = judgeSpawnPos.transform.position;
			finalPos.y += ingredient * .5f;
			//Instantiate ingredient
			GameObject addedIngredient = (GameObject) Instantiate(inBurger[ingredient], finalPos, Quaternion.identity);
			addedIngredient.GetComponent<Food>().isFoodPope = false;
			addedIngredient.transform.parent = null;
			addedIngredient.transform.localScale = Vector3.one;
			addedIngredient.transform.localRotation = Quaternion.Euler(-90,Random.Range (0,360),0);
			//Repeat
			ingredient++;
		}
	}
}
