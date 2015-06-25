using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public GameObject objectToMovePlayerTo;

	//On Trigger Enter
	void OnTriggerEnter(Collider col) {
		//if Player enters the collision box
		if (col.name == "Player") {

			//If there's a game object that says where to move the player to, use that (Maybe we should accept coordinates as well?)
			if (objectToMovePlayerTo != null) { 
				//Move the player to the place that the door is configured to go to
				col.transform.position = objectToMovePlayerTo.transform.position;
			} else {
				Debug.Log ("Whoops someone didn't configure this door correctly");
			}
		}
	}
}
