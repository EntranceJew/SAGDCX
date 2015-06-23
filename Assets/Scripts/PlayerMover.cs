using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {
	
	public float speed;
	
	private Rigidbody rb;
	
	void Start (){
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate (){
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(moveHorizontal, moveVertical) * Mathf.Rad2Deg, transform.eulerAngles.z);

		//loRo.y = Mathf.Atan2 (moveHorizontal, moveVertical) * Mathf.Rad2Deg;
		
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
	}
}