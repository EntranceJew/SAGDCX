using UnityEngine;
using System.Collections;

public class Recyclotron : MonoBehaviour {
	public GameObject inventory;
	public FoodLookup fl;

	private Inventory inv;

	// Use this for initialization
	void Start () {
		inv = inventory.GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		inv.Add (col.gameObject, 1);
		Destroy (col.gameObject);
	}
}
