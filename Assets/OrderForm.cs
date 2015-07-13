using UnityEngine;
using System.Collections;

public class OrderForm : MonoBehaviour {
	/* 
	@TODO: establish links to dayvalues for the current cash amount
	*/
	
	private RowManager[] rows;

	// Use this for initialization
	void Start () {
		rows = gameObject.GetComponentsInChildren<RowManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// form buttons
	public void Confirm(){
		Debug.Log ("PURCHASED A BUNCH OF STUFF FOR $"+GetTotal().ToString());
		Clear ();
	}

	public void Clear(){
		foreach (RowManager row in rows) {
			row.quantity = 0;
		}
	}

	// helpers
	public float GetTotal(){
		float total = 0.0f;
		foreach (RowManager row in rows) {
			total += row.quantity*row.rate;
		}
		return total;
	}
}
