using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarScript : MonoBehaviour {

	public GameObject bar;

	public List<float> dataList = new List<float>();

	// Use this for initialization
	void Start () {
		int temp = 0;
		foreach (int data in dataList) {
			float spacing = 15f;
			Vector3 pos = gameObject.transform.position;
			pos.x += (temp * spacing);
			pos.y += (dataList[temp] / 2) * 100;
			GameObject graphedBar = Instantiate(bar, pos, Quaternion.identity) as GameObject;
			graphedBar.transform.SetParent(gameObject.transform);
            graphedBar.GetComponent<RectTransform>().sizeDelta = new Vector2 (100, 100 * dataList[temp]);
			temp++;
		}
	}
}
