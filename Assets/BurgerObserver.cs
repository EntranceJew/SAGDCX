using UnityEngine;
using System.Collections;

public class BurgerObserver : MonoBehaviour {
	public Vector3 lastPosition;

	// Use this for initialization
	void Start () {
		lastPosition = Vector3.up;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay(Camera.main.transform.position, Vector3.up);

		if (Input.GetButtonDown ("Fire1")) {
			Vector3 mousePos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
			Vector3 point = Camera.main.ScreenToWorldPoint (mousePos);
			
			
			
			//Ray rayToCameraPos = new Ray(Camera.main.transform.position, point);
			RaycastHit hit;
			bool didIt = Physics.Raycast (Camera.main.transform.position, Camera.main.transform.position - mousePos, out hit, 1000);
			if (didIt) {
				Debug.Log (hit.collider.name + ", " + hit.collider.tag);
			} else {
				Debug.Log ("HORSEHOCKEY");
				Debug.Log (Camera.main.transform.position);
				Debug.Log ("YUKKOMUKKO");
				Debug.Log (mousePos);
				Debug.Log ("SPAGHET");
				Debug.Log (point);
				Debug.Log ("WATASHIWA");
				Debug.Log (Camera.main.transform.position - point);
			}
		}
	}
}
