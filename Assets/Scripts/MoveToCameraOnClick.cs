﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveToCameraOnClick : MonoBehaviour {
	public Transform ownPosition;
	public Transform cameraPosition;

	public GameObject theCamera;

	public bool wall = true;
	public float speed;
	float percent = 1;
	Transform goTo;
	Transform from;
	public ArrowSpin arrowSpin;
	public GraphicRaycaster graphicRaycaster;

	// Update is called once per frame
	void Update () {
		Move ();
	}

	void OnMouseDown() {
		if (wall) {
			if (!Camera.main.GetComponent<LookTowards>().attached) {
				Camera.main.GetComponent<LookTowards>().attached = true;
				MoveToCamera ();
			}
		} else {
			Camera.main.GetComponent<LookTowards>().attached = false;
			MoveToWall ();
		}
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

	void MoveToCamera() {
		wall = false;
		if (graphicRaycaster) {
			graphicRaycaster.enabled = true;
		}
		percent = 0;
		if (arrowSpin.GetState()) {
			arrowSpin.SetState(false);
		}
	}

	void MoveToWall() {
		if (graphicRaycaster) {
			graphicRaycaster.enabled = false;
		}
		wall = true;
		percent = 0;
	}
}
