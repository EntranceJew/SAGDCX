using UnityEngine;
using System.Collections;

public class CameraPan : MonoBehaviour {
	public float MinAngle = -45f;
	public float MaxAngle = 45f;
	public float Speed = 1;
	private int Direction = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.right * Speed * Direction * Time.deltaTime);
		if (transform.eulerAngles.x < MinAngle) {
			Direction = 1;
		}
		if (transform.eulerAngles.x > MaxAngle) {
			Direction = -1;
		}

	}
}
