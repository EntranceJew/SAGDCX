using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// from: http://answers.unity3d.com/questions/55406/picking-dragging-objects-with-mouse.html
// and by from I mean EntranceJew had to pick apart that ancient mess AND translate it into C#
// AND figure out what the hell went wrong in the formatting that means "ITALICIZE"
public class Draggable : MonoBehaviour {
	public string tagFilter = "Food";
	public GameObject grabbedObject;
	public float grabDistance = 10.0f;

	public bool debug;
	public List<Ray> debugRayList = new List<Ray> ();
	
	public bool useToggleDrag; // Didn't know which style you prefer.


	public Ray ray;
	public Vector3 position;
	public Plane plane;
	public float distance;
	public Vector3 temp;

	public RaycastHit hit;
	
	void Update () {
		if (useToggleDrag) {
			UpdateToggleDrag ();
		} else {
			//UpdateHoldDrag (); 
		}

		if (debug) {
			Debug.Log ("Debug Ray Size: " + debugRayList.Count);

			foreach (Ray ray in debugRayList) {
				Debug.DrawRay (ray.origin, ray.direction * 50000, Color.cyan);
			}
		}
	}
	
	// Toggles drag with mouse click 
	void UpdateToggleDrag () {
		if (Input.GetButtonDown ("Fire1")) {
			Grab ();
		} else if (grabbedObject != null) {
			Drag (); 
		}
	}
	
	/*// Drags when user holds down button function 
	void UpdateHoldDrag () {
		if (Input.GetButtonDown("Fire1")){
			if (grabbed) {
				Drag();
			} else {
				Grab();
				grabbed = null; 
			}
		}
	}*/
	
	void Grab() {
		if (grabbedObject != null) {
			grabbedObject = null;
		} else {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				if(hit.transform.gameObject.tag == tagFilter){
					if (debug) {debugRayList.Add(ray);}
					grabbedObject = hit.transform.gameObject;
					//DebugPoint(hit.transform, "GrabRaycast");
				}
			}
		}
	}
	
	void Drag() {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		plane = new Plane(-transform.forward, position);
		if (plane.Raycast(ray, out distance)) {
			temp = ray.origin + ray.direction * distance;
			grabbedObject.transform.position = temp;
			//grabbedObject.transform.rotation = transform.rotation;
		}
	}
}