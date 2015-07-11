using UnityEngine;
using System.Collections;

// DayMan(aaaaAAAAHHHH!!!)ager is meant to keep track of the day progress and stuff.
// I dislike writing "Managers" as much as the next guy but sometimes we need a deity.
public class DayManager : MonoBehaviour {
	public GameObject orderer;

	private AltGetOrder getOrder;

	// Use this for initialization
	void Start () {
		// eventually we're going to want to do something that isn't this, but, I'm the king for now

		getOrder = orderer.GetComponent<AltGetOrder> ();
		getOrder.NewOrder ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
