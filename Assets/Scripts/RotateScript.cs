using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour {
	public float rotationSpeed = 1;


	// Update is called once per frame
	void Update () {
		transform.Rotate (0, rotationSpeed, 0);
	}
}
