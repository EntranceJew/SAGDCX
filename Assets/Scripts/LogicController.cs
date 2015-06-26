using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LogicController : MonoBehaviour {
	public GameObject thingToSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateControls ();
	}

	void UpdateControls(){
		if (Input.GetButtonDown ("Fire1") && !EventSystem.current.IsPointerOverGameObject()) {
			Vector3 mousePos = Input.mousePosition;
			mousePos = Camera.main.ScreenToWorldPoint(mousePos);

			// push the 
			mousePos.z = 0.0f;

			SpawnThingAt(mousePos);
		}
	}

	GameObject SpawnThingAt(Vector3 placeToSpawn){
		GameObject newThing = (GameObject) Instantiate (
			thingToSpawn,
			placeToSpawn,
			Quaternion.identity
		);

		return newThing;
	}
}
