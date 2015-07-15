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
		string goal = "Assets/Resources/Foods";
		string shortgoal = "Foods/";
		DirectoryInfo dir = new DirectoryInfo(goal);
		FileInfo[] info = dir.GetFiles("*.prefab");

		for(int j = 0; j < info.Length; j++){
			Debug.Log("Opening: Assets\\Prefabs\\Foods\\" + info[j].Name);
			GameObject obj = (GameObject) Resources.Load(shortgoal+Path.GetFileNameWithoutExtension(info[j].Name));
			Debug.Log(obj);
			lookup.Add(obj);
			nameToObject.Add (
				Path.GetFileNameWithoutExtension(info[j].Name),
				obj
			);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
