using UnityEngine;
using System.Collections;

public class SpawnArea : MonoBehaviour {
	public bool shouldSpawn;
	public GameObject spawnObject;
	public float rateOfSpawn;
	private float nextSpawn;

	// Use this for initialization
	void Start () {
		nextSpawn = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn && shouldSpawn) {
			nextSpawn = Time.time + rateOfSpawn;
			SpawnThis ();
		}
	}

	public Object SpawnThis(){
		Vector3 position = getRandomPosition ();
		return Instantiate (spawnObject, position, transform.rotation);
	}

	public void SpawnA(Object objToSpawn){
		Vector3 position = getRandomPosition ();
		Instantiate (objToSpawn, position, transform.rotation);
	}

	public Vector3 getRandomPosition(){
		Vector3 spawnBox = gameObject.transform.localScale;
		Vector3 spawnPos = gameObject.transform.position;
		Vector3 newPos = new Vector3 (
			Random.Range (-spawnBox.x / 2, spawnBox.x / 2),
			Random.Range (spawnPos.y, spawnBox.y + spawnPos.y / 2 ),
			0.0f
		);
		//Debug.Log (newPos);
		return newPos;
	}
}