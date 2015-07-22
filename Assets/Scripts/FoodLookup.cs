using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class FoodLookup : MonoBehaviour {
	public List<GameObject> lookup;
	public Dictionary<string, GameObject> nameToObject = new Dictionary<string, GameObject>();

	public GameObject GetGameObject(string name){
		GameObject go;
		nameToObject.TryGetValue(name, out go);
		if(!go){
			Debug.Log ("WHAT THE FUCK IS A "+name);
		}
		return go;
	}

	public Food GetFood(string name){
		return nameToObject [name].GetComponent<Food>();
	}

	// we have to do this instead of Start because the timing / execution of certain things were choking without this
	// we're the most important singleton in the game, so, it's only fair
	void Awake () {
		Object[] objs = Resources.LoadAll ("Foods");

		foreach(GameObject obj in objs){
			//Debug.Log("Opening: Assets\\Prefabs\\Foods\\" + obj.name);
			//Debug.Log(obj);
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
