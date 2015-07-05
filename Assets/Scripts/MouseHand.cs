using UnityEngine;
using System.Collections;

public class MouseHand : MonoBehaviour {
	public GameObject grabbedObject;
	public Plane plane;
	public float distance;
	public Vector3 position;
	public Vector3 temp;
	public float floor = 0f;
	void Update() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		plane = new Plane(-transform.forward, position);
		if (plane.Raycast(ray, out distance)) {
			temp = ray.origin + ray.direction * distance;
			if (temp.y < floor){
				temp.y = floor;
			}
			grabbedObject.transform.position = temp;
				
		}
	}
}
