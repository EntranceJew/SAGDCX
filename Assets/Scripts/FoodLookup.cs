using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FoodLookup : MonoBehaviour {
	public List<GameObject> lookup;
	public Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();

	public GameObject GetGameObject(string name){
		return nameToObject [name];
	}

	public Food GetFood(string name){
		return nameToObject [name].GetComponent<Food>();
	}

	// Use this for initialization
	void Start () {
		Object[] objs = Resources.LoadAll ("Foods");

		foreach(GameObject obj in objs){
			Debug.Log("Opening: Assets\\Prefabs\\Foods\\" + obj.name);
			Debug.Log(obj);
			lookup.Add((GameObject) obj);
			nameToObject.Add (
				obj.name,
				(GameObject) obj
			);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
