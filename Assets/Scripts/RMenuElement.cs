using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class RMenuItem {
	public string name;
	public List<GameObject> contents;
}

public class RMenuElement : MonoBehaviour {
	public List<RMenuItem> menuItems;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
