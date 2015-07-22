using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shipment : MonoBehaviour {
	// @TODO: If we slide in with an empty shipment, then we should play a buzzer noise and have a single junk item fall out.
	public Transform idlePos;
	public Transform activePos;

	public bool active = true;
	public float timeToSwitch;

	public Inventory inventory;
	public SpawnArea spawnArea;

	public bool inMotion = false;

	public List<GameObject> notifyWhenDone;

	private Transform goTo;
	private Transform from;

	private float startTime;
	private float journeyLength;
	
	private float shopEndTime = Mathf.NegativeInfinity;

	// Update is called once per frame
	void Update () {
		if (inMotion) {
			float distCovered = (Time.time - startTime) * timeToSwitch;
			transform.position = Vector3.Lerp (from.position, goTo.position, distCovered / journeyLength);
			transform.rotation = Quaternion.Lerp (from.rotation, goTo.rotation, distCovered / journeyLength);
			if(distCovered >= journeyLength){
				inMotion = false;
				if(active){
					MovedTo();
				} else {
					MovedFrom();
				}
			}
		}
		if (shopEndTime != Mathf.NegativeInfinity && Time.time >= shopEndTime) {
			MoveTo ();
			shopEndTime = Mathf.NegativeInfinity;
		}
	}

	public void TogglePos(){
		if(active){
			MoveFrom ();
		} else {
			MoveTo ();
		}
		DoIt();
	}

	public void DoIt(){
		inMotion = true;
		startTime = Time.time;
		journeyLength = Vector3.Distance (from.position, goTo.position);
	}

	public void MoveTo(){
		// This is called after an order arrives
		Debug.Log ("Grocery here!");
		goTo = activePos;
		from = idlePos;
		active = true;
		DoIt ();
	}

	public void MovedTo(){
		spawnArea.shouldSpawn = true;
	}

	public void GoGetIt(float shopTime){
		shopEndTime = Time.time + shopTime;
		Debug.Log ("Grocery run! " + shopEndTime);
	}

	public void MoveFrom(){
		Debug.Log ("Grocery done!");
		spawnArea.shouldSpawn = false;
		goTo = idlePos;
		from = activePos;
		active = false;
		DoIt ();
	}

	public void MovedFrom(){
		foreach (GameObject go in notifyWhenDone) {
			go.SendMessage ("ShipmentDone");
		}
	}
}
