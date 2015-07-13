using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BarScript : MonoBehaviour {

	public GameObject bar;

	public string yName;
	public string xName;
	public List<float> dataList = new List<float>();

	// Use this for initialization
	void Start () {
		//Set Axis
		Text yText = null;
		Text xText = null;

		foreach (RectTransform t in transform) {
			if (t.name == "YLabel") {
				yText = t.gameObject.GetComponent<Text>();
			}

			if (t.name == "XLabel") {
				xText = t.gameObject.GetComponent<Text>();
			}
		}

		yText.text = yName;
		xText.text = xName;

		int temp = 0;
		foreach (int data in dataList) {
			//Position of start element.
			Vector3 pos = Vector3.zero;
			pos.x += temp * 1.1f;
			pos.y -= data * .5f;

			//Draw bar
			GameObject graphedBar = Instantiate(bar, Vector3.zero, Quaternion.identity) as GameObject;
			RectTransform rt = graphedBar.GetComponent<RectTransform>();

			//Set Parent
			rt.SetParent(gameObject.transform);

			//Set Height
			rt.sizeDelta = new Vector2( 1f, data);

			//Set Position
			rt.localPosition = pos;

			//Set Rotation
			rt.localRotation = Quaternion.identity;

			//Set scale
			rt.localScale = Vector3.one;

			temp++;
		}
	}
}
