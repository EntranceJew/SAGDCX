using UnityEngine;
using System.Collections;

public class TutorialHandler : MonoBehaviour {
	public bool showTutorials = true;

	public DayValues dayValues;
	//public Shipment shipment;
	public GameObject lookAt;
	public GameObject mouseObject;

	// Use this for initialization
	void Start () {
		if (dayValues.day == 0 && showTutorials) {
			//shipment.gameObject.transform.parent
			Camera.main.GetComponent<LookTowards>().SetTarget(lookAt.transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ShipmentDone(){
		LookTowards lt = Camera.main.GetComponent<LookTowards> ();
		if (dayValues.day == 0 && lt.Target == lookAt.transform && showTutorials) {
			lt.SetTarget (mouseObject.transform);
		}
	}
}
