using UnityEngine;
using System.Collections;

public class MoveToCameraOnClick : MonoBehaviour {
	public Transform ownPosition;
	public Transform cameraPosition;
	public bool wall = true;
	public float speed;
	float percent = 1;
	Transform goTo;
	Transform from;

	// Update is called once per frame
	void Update () {
		Move ();
	}

	void OnMouseDown() {
		wall = !wall;
		percent = 0;
	}

	void Move() {
		percent += Time.deltaTime * speed;



		if (wall) {
			goTo = ownPosition;
			from = cameraPosition;
		} else {
			goTo = cameraPosition;
			from = ownPosition;
		}

		transform.position = Vector3.Lerp (from.position, goTo.position, percent);
		transform.rotation = Quaternion.Lerp (from.rotation, goTo.rotation, percent);

		if (percent > 1) {
			percent = 1;
		}
	}
}
