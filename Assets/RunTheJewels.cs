using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RunTheJewels : MonoBehaviour {
	public RMenuElement rmenu;
	public DayValues dayValues;

	// exposed helpers
	public List<InventoryItem> demand;

	private RMenuElement myMenuElement;

	// Use this for initialization
	void Start () {
		// get actual price of every menu item
		myMenuElement = gameObject.AddComponent<RMenuElement> ();
		myMenuElement.menuItems = new List<RMenuItem> (rmenu.menuItems);

		// get actual demand of each item
		Dictionary<GameObject, int> dicDemand = new Dictionary<GameObject, int> ();
		demand = new List<InventoryItem> ();
		foreach (int orderNo in dayValues.GetTodaysOrders()) {
			RMenuItem item = myMenuElement.menuItems[orderNo];
			foreach(GameObject content in item.contents){
				int dem = 0;
				dicDemand.TryGetValue(content, out dem);
				if( dem > 0 ){
					dicDemand[content]++;
				} else {
					dicDemand.Add (content, 1);
				}
			}
		}
		foreach (KeyValuePair<GameObject, int> entry in dicDemand) {
			demand.Add(new InventoryItem(entry.Key, entry.Value));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
