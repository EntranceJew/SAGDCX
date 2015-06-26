using UnityEngine;
using System.Collections;

// from: http://answers.unity3d.com/questions/55406/picking-dragging-objects-with-mouse.html
// and by from I mean EntranceJew had to pick apart that ancient mess AND translate it into C#
// AND figure out what the hell went wrong in the formatting that means "ITALICIZE"
public class Draggable : MonoBehaviour {
	public GameObject grabbedObject;
	public float grabDistance = 10.0f;

	public GameObject debugPoint;
	public GameObject debugArrow;
	
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
				grabbedObject = hit.transform.gameObject;
				//DebugPoint(hit.transform, "GrabRaycast");
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

	void DebugPoint(Vector3 position, string identifier){
		GameObject go = (GameObject) Instantiate (debugPoint, position, Quaternion.identity);
		go.name = "DebugPoint "+identifier+" @ "+Time.time;
	}

	void DebugPoint(Transform transform, string identifier){
		GameObject go = (GameObject) Instantiate (debugPoint, transform.position, transform.rotation);
		go.name = "DebugPoint "+identifier+" @ "+Time.time;
	}
	
	void DebugArrow(Vector3 position, Vector3 direction, string identifier){
		Vector3 newPos = position + direction;
		GameObject go = (GameObject) Instantiate (debugArrow, position, new Quaternion(newPos.x, newPos.y, newPos.z, 0.0f));
		go.name = "DebugArrow "+identifier+" @ "+Time.time;
	}
}