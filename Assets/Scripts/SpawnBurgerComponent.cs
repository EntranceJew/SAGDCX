using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SpawnBurgerComponent : MonoBehaviour {
	public GameObject judgeSpawnPos;
	public GameObject theSpawnedClone;

	public void SpawnJudgeBurger(GameObject burgBuilder) {
		//Take order
		//Find where to drop it.
		Vector3 finalPos = judgeSpawnPos.transform.position;
		theSpawnedClone = (GameObject) Instantiate(burgBuilder, finalPos, burgBuilder.transform.rotation);
		// Disable new, we are the superior clone.
		Destroy(theSpawnedClone.GetComponent<BurgBuilder> ());
		Destroy(theSpawnedClone.GetComponent<SpawnBurgerComponent> ());
		foreach (Transform child in theSpawnedClone.transform) {
			Food fd = child.GetComponent<Food>();
			if(fd != null){
				fd.isFake = true;
				fd.isFoodPope = false;
			} else {
				Destroy(child.gameObject);
			}
		}
	}

	public void TrashIt(){
		Destroy (theSpawnedClone);
	}
}
