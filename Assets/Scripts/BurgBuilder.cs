using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BurgBuilder : MonoBehaviour {
	public PartZone pz;
	public DayManager dayManager;
	public SpawnBurgerComponent spawnBurgerComponent;
	public BurgJudgeCatcher burgJudgeCatcher;

	public Transform idlePos;
	public Transform activePos;

	public bool active = true;
	public float timeToSwitch;

	public bool inMotion = false;

	public float timeBeforeForciblyUnHoist = 4.0f;

	private Transform goTo;
	private Transform from;
	
	private float startTime;
	private float journeyLength;

	void Update(){
		if (inMotion) {
			float distCovered = (Time.time - startTime) * timeToSwitch;
			transform.position = Vector3.Lerp (from.position, goTo.position, distCovered / journeyLength);
			transform.rotation = Quaternion.Lerp (from.rotation, goTo.rotation, distCovered / journeyLength);
			if(distCovered >= journeyLength){
				inMotion = false;
				if(active){
					DeliverPayload();
				} else {
					DeliverReturnPayload();
				}
			}
		}
	}

	public void DeliverPayload(){
		Debug.Log ("I'M HERE, FUCKERS");
		spawnBurgerComponent.SpawnJudgeBurger (gameObject);
		TrashBurger ();
		StartCoroutine ("LifeAlert");
	}

	public IEnumerator LifeAlert() {
		yield return new WaitForSeconds(timeBeforeForciblyUnHoist);
		Debug.LogWarning ("HAD TO FORCIBLY UNHOIST");
		burgJudgeCatcher.ChewOnThis ();
		UnHoist ();
	}

	public void DeliverReturnPayload(){
		Debug.Log ("I'M BACK, HONKIES");
		// @TODO: Make the scoring sequence handle this, this is just in case we have a rapid burger builder on our hands.
		spawnBurgerComponent.TrashIt ();

		// Prepare the next order.
		dayManager.GetNextOrder ();
	}

	public void DoIt(){
		inMotion = true;
		startTime = Time.time;
		journeyLength = Vector3.Distance (from.position, goTo.position);
	}

	public bool CanHoist(){
		if (active) {
			// we can't hoist if we're hoisted
			return false;
		}
		if (inMotion) {
			// we can't hoist if we're (un)hoisting
			return false;
		}
		if (transform.childCount <= 1) {
			// we can't hoist, we have nothing to hoist
			return false;
		}
		return true;
	}

	// elevate me to the heavens
	public void Hoist(){
		Debug.Log ("LATER, PLEBS");
		foreach(GameObject go in GetChildParts()){
			Food fd = go.GetComponent<Food>();
			fd.UnBecomeLilMac();
		}
		// @TODO: FREEZE AND WELD ALL FOOD. YEAH, WE GMOD NOW.
		goTo = activePos;
		from = idlePos;
		active = true;
		DoIt ();
	}

	// return me, mortals
	public void UnHoist(){
		StopCoroutine ("LifeAlert");
		Debug.Log ("BACK, PLEBS");

		// Trash the existing burger as soon as we're out of frame.
		TrashBurger ();

		goTo = idlePos;
		from = activePos;
		active = false;
		DoIt ();
	}
	
	public bool EmancipatePart(GameObject part){
		Debug.Log ("WHY AM I EMANCIPATING " + part);
		if (BelongsToMe (part)) {
			part.GetComponent<Food>().Emancipate();
			if(transform.childCount == 1){
				// we used to enable here, now we don't
			}
			return true;
		}
		return false;
	}

	public bool ObtainNewPart(GameObject part){
		// We only accept orphans.
		// Originally we only cared if the object's parent wasn't us but that's more effort,
		// because the first condition clashes with the second half and boy am I tired.
		// part.transform.parent != null & part.transform.parent.gameObject != this.gameObject
		if (!BelongsToMe(part)) {
			//Debug.Log ("I just found myself a new " + part.name);
			part.GetComponent<Food> ().GetObtained (this);

			//Vector3 pzt = pz.transform.localPosition;

			//pzt.y += 0.7f;

			//pz.transform.localPosition = pzt;

			//Rigidbody rb = part.GetComponent<Rigidbody> ();
			//rb.useGravity = false;
			//rb.isKinematic = true;
			//rb.freezeRotation = true;
			return true;
		} else {
			Debug.Log ("NO, FUCK YOUR " + part.name);
			return false;
		}
	}

	public bool BelongsToMe(GameObject part){
		if (part.transform.parent != null) {
			if(part.transform.parent != gameObject.transform){
				Debug.Log ("I was its parent.");
				return false;
			} else {
				return true;
			}
		} else {
		//	Debug.Log ("Did not have parent.");
			return false;
		}
	}
	public List<GameObject> GetChildParts(){
		List<GameObject> burgObjects = new List<GameObject> ();
		foreach (Transform t in transform) {
			if (t.name != "PartZone" && t.name != "Plate") {
				burgObjects.Add (t.gameObject);
			}
		}
		return burgObjects;
	}

	public void TrashBurger(){
		foreach (Transform t in transform) {
			if (t.name != "PartZone" && t.name != "Plate") {
				Destroy (t.gameObject);
			}
		}
	}
}
