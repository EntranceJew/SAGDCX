using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttractModeBurgSpawn : MonoBehaviour {
	public FoodLookup fl;
	List<GameObject> currentBurg = new List<GameObject>();
	float count = 0;
	public float maxTime;
	public int minToSpawn;
	public int maxToSpawn;

	void Update() {
		count += Time.deltaTime;
		//If there's no burger on screen YA GOTTA MAKE A BURGER
		if (currentBurg.Count == 0) {
			count = 0;
			float upVar = .5f;
			int numToSpawn = Random.Range (minToSpawn,maxToSpawn);

			for (int i = 0; i < numToSpawn; i++) {
				int foodToSpawn = Random.Range (0, fl.lookup.Count);
				Vector3 burgSpawnPosition = transform.position;
				burgSpawnPosition.y += i * upVar;
				GameObject food = Instantiate(fl.lookup[foodToSpawn], burgSpawnPosition, Quaternion.Euler(new Vector3(0,Random.Range(0,360),0))) as GameObject;
				Rigidbody rb = food.GetComponent<Rigidbody>();
				rb.freezeRotation = false;
				rb.mass = 50;
				rb.drag = 1;
				currentBurg.Add(food);
			}
		}

		//Ok they stared at this burger enough time to give them a new one
		if (count > maxTime) {
			foreach (GameObject obj in currentBurg) {
				Destroy(obj);
			}

			currentBurg.Clear ();
		}
	}
}
