using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AltScore : MonoBehaviour {
	public List<GameObject> theOrder;
	public List<GameObject> theBurg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public float EvaluateBurger(List<GameObject> order, List<GameObject> burg) {
		theOrder = order;
		theBurg = burg;
		return 0.0f;
	}

	float TopsAndBottomsMatch(){
		return 0.0f;
	}
}