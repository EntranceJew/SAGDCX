using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetOrder : MonoBehaviour {

	GameObject canvas;
	Text instruction;
	Order order;

	public bool rand;

	void Start () {
		canvas = GameObject.FindGameObjectWithTag("MainCanvas");

		if (rand) {
			order = new Order ();
		} else {
			order = new Order ("Normal");
		}
		instruction = canvas.transform.Find("OrderText").gameObject.GetComponent<Text> ();
		instruction.text = "Your order is: " + order.toString ();
	}
}
