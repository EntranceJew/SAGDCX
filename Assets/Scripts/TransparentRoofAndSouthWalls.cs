using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransparentRoofAndSouthWalls : MonoBehaviour {
	public List<GameObject> toTransparentList = new List<GameObject>();
	public List<GameObject> toTransparentToppers = new List<GameObject>();


	public void OnTriggerEnter (Collider col) {
		if (col.name == "Player") {
			//Make stuff transparent
			HideEverything();
		}
	}

	public void OnTriggerExit (Collider col) {
		if (col.name == "Player") {
			//Make stuff NOT transparent again
			UnhideEverything();
		}
	}

	public void HideEverything(){
		foreach (GameObject obj in toTransparentList) {
			obj.GetComponent<MeshRenderer> ().enabled = false;
		}

		foreach (GameObject obj in toTransparentToppers) {
			foreach(MeshRenderer mesh in obj.GetComponentsInChildren<MeshRenderer>()){
				mesh.enabled = false;
			}
		}
	}

	public void UnhideEverything(){
		foreach (GameObject obj in toTransparentList) {
			obj.GetComponent<MeshRenderer> ().enabled = true;
		}

		foreach (GameObject obj in toTransparentToppers) {
			foreach(MeshRenderer mesh in obj.GetComponentsInChildren<MeshRenderer>()){
				mesh.enabled = true;
			}
		}
	}
}
