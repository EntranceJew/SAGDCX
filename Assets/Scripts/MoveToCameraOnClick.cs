using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MoveToCameraOnClick : MonoBehaviour {
	public Transform ownPosition;
	public Transform cameraPosition;

	public bool inPosition = true;
	public float timeToSwitch;
	public bool inMotion = false;

	public bool blockClickThrough = false;
	Transform goTo;
	Vector3 fromPos;
	Quaternion fromAngle;
	public ArrowSpin arrowSpin;
	public GraphicRaycaster graphicRaycaster;

	private float startTime;
	private float journeyLength;

	private Collider col;
	private Shaker shaker;
	void Start(){
		col = gameObject.GetComponent<Collider> ();
		shaker = gameObject.GetComponent<Shaker> ();
	}

	// Update is called once per frame
	void Update () {
		if (inMotion) {
			float distCovered = (Time.time - startTime) * timeToSwitch;
			transform.position = Vector3.Lerp (fromPos, goTo.position, distCovered / journeyLength);
			transform.rotation = Quaternion.Lerp (fromAngle, goTo.rotation, distCovered / journeyLength);
			if(distCovered >= journeyLength){
				inMotion = false;
				if(inPosition){
					MovedToCamera();
				} else {
					MovedToWall();
				}
			}
		}
	}

	void OnMouseDown() {
		if (blockClickThrough && EventSystem.current.IsPointerOverGameObject ()) {
			return;
		}

		if(shaker && shaker.doShake){
			return;
		}

		if (inPosition) {
			Camera.main.GetComponent<LookTowards>().attached = false;
			MoveToWall ();
		} else {
			if (!Camera.main.GetComponent<LookTowards>().attached) {
				// don't get closer if we're shaking

				Camera.main.GetComponent<LookTowards>().attached = true;
				MoveToCamera ();
			}
		}
	}

	void DoIt(){
		col.isTrigger = true;
		fromPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		fromAngle = new Quaternion (transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		inMotion = true;
		startTime = Time.time;
		journeyLength = Vector3.Distance (fromPos, goTo.position);
	}

	public void MoveToCamera(){
		goTo = cameraPosition;
		inPosition = true;
		DoIt ();
	}

	public void MoveToWall(){
		goTo = ownPosition;
		inPosition = false;
		DoIt ();
	}

	void MovedToCamera() {
		col.isTrigger = false;
		if (graphicRaycaster) {
			blockClickThrough = true;
			graphicRaycaster.enabled = true;
		}
		if (arrowSpin.GetState()) {
			arrowSpin.SetState(false);
		}
	}

	void MovedToWall() {
		col.isTrigger = false;
		if (graphicRaycaster) {
			blockClickThrough = false;
			graphicRaycaster.enabled = false;
		}
	}
}
