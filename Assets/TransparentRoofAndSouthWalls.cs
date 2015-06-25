using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransparentRoofAndSouthWalls : MonoBehaviour {
	public List<GameObject> toTransparentList = new List<GameObject>();


	void OnTriggerEnter (Collider col) {
		if (col.name == "Player") {
			//Make stuff transparent
			foreach (GameObject obj in toTransparentList) {
				obj.GetComponent<MeshRenderer> ().enabled = false;
			}
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.name == "Player") {
			//Make stuff NOT transparent again
			foreach (GameObject obj in toTransparentList) {
				obj.GetComponent<MeshRenderer> ().enabled = true;
			}
		}
	}
}
