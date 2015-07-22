using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FullScreen : MonoBehaviour {
	void Start () {
		gameObject.GetComponent<RectTransform> ().sizeDelta = new Vector2 (Screen.width, Screen.height);
	}
}
