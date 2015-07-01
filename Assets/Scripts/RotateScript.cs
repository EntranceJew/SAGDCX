using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
	public float rotationSpeed;


	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Rigidbody> ().AddTorque(new Vector3(0,rotationSpeed,0));
	}
}
