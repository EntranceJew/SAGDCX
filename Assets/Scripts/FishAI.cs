using UnityEngine;
using System.Collections;

public class FishAI : MonoBehaviour {
	GameObject foodTarget;
	//float smooth = 2.0f;
	//float tiltAngle = 30.0f;
	Rigidbody rb;
	float startDrag;

	public float inWaterDrag = 10.0f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		startDrag = rb.drag;
	}
	
	void Update () {
		FindFood ();

		if (foodTarget != null) {
			Quaternion newRotation = Quaternion.LookRotation (transform.position - foodTarget.transform.position, -Vector3.forward);
			newRotation.x = 0.0f;
			newRotation.y = 0.0f;
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 8);
			rb.AddForce ((foodTarget.transform.position - transform.position) * 8);
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Food") {
			Eat(col.gameObject);
		}
	}

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Water") {
			rb.useGravity = false;
			rb.drag = inWaterDrag;
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Water") {
			rb.useGravity = true;
			rb.drag = startDrag;
		};
	}

	public void FindFood (){
		string tagToTarget = "Food";
		
		Transform[] foods = UnityEngine.Object.FindObjectsOfType<Transform>();
		Transform bestTarget = null;
		float closestDistanceSqr = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		foreach(Transform potentialTarget in foods){
			Vector3 directionToTarget = potentialTarget.position - currentPosition;
			float dSqrToTarget = directionToTarget.sqrMagnitude;
			if(dSqrToTarget < closestDistanceSqr && potentialTarget.gameObject.tag == tagToTarget){
				closestDistanceSqr = dSqrToTarget;
				bestTarget = potentialTarget;
			}
		}

		if (bestTarget != null) {
			SetFoodTarget (bestTarget.gameObject);
		}
	}

	public void SetFoodTarget(GameObject food){
		foodTarget = food;
	}

	void Eat(GameObject food){
		Debug.Log ("FUCK YOUR PANCAKES.");
		Destroy (food);
	}
}
