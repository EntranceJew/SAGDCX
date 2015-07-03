using UnityEngine;
using System.Collections;

public class DispenseLiquid : MonoBehaviour {
	public string resourceName;
	static GameObject liquidToDispense;
	public GameObject pos;


	// Use this for initialization
	void Start () {
		liquidToDispense = Resources.Load (resourceName) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		Instantiate(liquidToDispense, pos.transform.position, Quaternion.identity);
	}
}
