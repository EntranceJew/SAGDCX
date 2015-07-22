using UnityEngine;
using System.Collections;

public class SpawnArea : MonoBehaviour {
	public bool shouldSpawn;
	public float rateOfSpawn;
	public float scaleRange = 2.0f;

	public Shipment ship;
	public Inventory spawnSource;

	private float nextSpawn;

	// Use this for initialization
	void Start () {
		nextSpawn = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextSpawn && shouldSpawn) {
			nextSpawn = Time.time + rateOfSpawn;

			int index = Random.Range(0, spawnSource.stock.Count-1);
			if(spawnSource.stock.Count == 0){
				ship.MoveFrom();
			} else {
				InventoryItem ii = spawnSource.stock[index];
				SpawnA (ii.represents);
				spawnSource.Remove(ii.represents.name, 1);
			}
		}
	}

	public GameObject SpawnA(Object objToSpawn){
		Vector3 position = getRandomPosition ();
		GameObject go = (GameObject) Instantiate (objToSpawn, position, transform.rotation);
		go.GetComponent<Collider> ().isTrigger = true;
		return go;
	}

	public Vector3 getRandomPosition(){
		BoxCollider bc = GetComponent<BoxCollider> ();
		Bounds bound = bc.bounds;
		Vector3 ex = bound.extents;

		Vector3 newPos = new Vector3 (
			Random.Range (-ex.x/scaleRange, ex.x/scaleRange),
			Random.Range (-ex.y/scaleRange, ex.y/scaleRange),
			Random.Range (-ex.z/scaleRange, ex.z/scaleRange)
		);
		//Debug.Log (newPos + transform.position);
		return newPos + transform.position;
	}
}