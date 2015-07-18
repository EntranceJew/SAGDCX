using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnBurgerComponent : MonoBehaviour {
	public GameObject judgeSpawnPos;

	public void SpawnJudgeBurger(GameObject burgBuilder) {
		//Take order
		//Find where to drop it.
		Vector3 finalPos = judgeSpawnPos.transform.position;
		GameObject addedIngredient = (GameObject) Instantiate(burgBuilder, finalPos, burgBuilder.transform.rotation);
		// Disable clone.
		Destroy(addedIngredient.GetComponent<BurgBuilder> ());
		foreach (Transform child in addedIngredient.transform) {
			Food fd = child.GetComponent<Food>();
			if(fd != null){
				fd.isFake = true;
				fd.isFoodPope = false;
			} else {
				Destroy(child.gameObject);
			}
		}
	}
}
