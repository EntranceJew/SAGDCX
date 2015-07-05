using UnityEngine;
using System.Collections;

public class PlateThing : MonoBehaviour {
	public bool canSpawnThing;
	public GameObject spawnedThing;

	public float spawnDelay = 9.0f;
	public float lastSpawnTime = Mathf.NegativeInfinity;

	public float timeExpected;
	public float timePreview;


	// Use this for initialization
	void Start () {
		canSpawnThing = true;
	}
	
	// Update is called once per frame
	void Update () {
		timeExpected = Time.time + spawnDelay;
		timePreview = Time.time;
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject == spawnedThing) {
			canSpawnThing = true;
		}
	}

	public bool SpawnThing(GameObject thingToSpawn){
		if (lastSpawnTime + spawnDelay < Time.time && canSpawnThing) {
			Transform spawnPoint = transform.Find ("PlateSpawn");
			spawnedThing = (GameObject)Instantiate (thingToSpawn, spawnPoint.position, spawnPoint.rotation);
			lastSpawnTime = Time.time;
			canSpawnThing = false;
			return true;
		} else {
			return false;
		}
	}
}
