using UnityEngine;
using System.Collections;

// from: http://answers.unity3d.com/questions/55406/picking-dragging-objects-with-mouse.html
// and by from I mean EntranceJew had to pick apart that ancient mess AND translate it into C#
// AND figure out what the hell went wrong in the formatting that means "ITALICIZE"
public class Draggable : MonoBehaviour {
	public Transform grabbed;
	public float grabDistance = 10.0f;
	
	public bool useToggleDrag; // Didn't know which style you prefer.
	
	void Update () {
		if (useToggleDrag) {
			UpdateToggleDrag ();
		} else {
			//UpdateHoldDrag (); 
		}
	}
	
	// Toggles drag with mouse click 
	void UpdateToggleDrag () {
		if (Input.GetButtonDown ("Fire1")) {
			Grab ();
		} else if (grabbed) {
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
		if (grabbed) {
			grabbed = null;
		} else { 
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (ray, out hit)) {
				grabbed = hit.transform; 
			}
		}
	}
	
	void Drag() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 position = transform.position + transform.forward;
		Plane plane = new Plane(-transform.forward, position);
		float distance;
		if (plane.Raycast(ray, out distance)) {
			Vector3 temp = ray.origin + ray.direction / distance;
			grabbed.position = temp;
			grabbed.rotation = transform.rotation; 
		} 
	}
}