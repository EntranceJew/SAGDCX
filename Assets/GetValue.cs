using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public enum ValuesToGet {
	MinimumValue,
	MaximumValue
}

public class GetValue : MonoBehaviour {
	public ValuesToGet getThis;
	public GameObject bell;

	GameObject canvas;

	void Start () {
		canvas = GameObject.FindGameObjectWithTag("MainCanvas");
	}

	void Update () {
		GetComponent<Text> ().text = ValFromEnum (getThis) + "";
	}

	float ValFromEnum (ValuesToGet e) {
		switch (e) {
		case ValuesToGet.MaximumValue:
			return bell.GetComponent<AltScore>().GetMaxScore();
		case ValuesToGet.MinimumValue:
			return bell.GetComponent<AltScore>().GetMinScore();
		default:
			return 666;
		}
	}
}
