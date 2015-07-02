using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetOrder : MonoBehaviour {
	Text instruction;
	Order order;

	public bool rand;

	void Start () {
		if (rand) {
			order = new Order ();
		} else {
			order = new Order ("Normal");
		}
		instruction = GetComponent<Text> ();
		instruction.text = "Your order is: " + order.toString ();
	}
}
