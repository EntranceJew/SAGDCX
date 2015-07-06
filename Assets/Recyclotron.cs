using UnityEngine;
using System.Collections;

public class Recyclotron : MonoBehaviour {
	public GameObject inventory;

	private Inventory inv;

	// Use this for initialization
	void Start () {
		inv = inventory.GetComponent<Inventory> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col){
		string[] arr = col.gameObject.name.Split('(');
		inv.Add (arr[0], 1);
		Destroy (col.gameObject);
	}
}
