using UnityEngine;
using System.Collections;

public class RatFloor : MonoBehaviour {
	public GameObject rat;
	public FoodLookup fl;
	public float powerOfRat = 2.0f;
	public int minimumRats = 2;
	// you can't stop the power of rats, but if you wanted to, this is how it would be done
	public int maximumRats = -1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		Food fd = col.gameObject.GetComponent<Food> ();
		if (fd != null) {
			string[] arr = col.gameObject.name.Split('(');
			string itemName = fl.GetGameObject (arr [0]).name;

			if(itemName == rat.name && fd.belongsOnFloor) {
				Debug.Log ("THIS RAT IS OURS. GOOD.");
			} else if(itemName == rat.name && !fd.belongsOnFloor) {
				Debug.Log ("RAT LOCATED, GLORIOUS RAT.");
				PlayerValues.pv.ratsInHouse.Add (rat, 1);
				Destroy(col.gameObject);
			} else if(itemName != rat.name){
				int rats = PlayerValues.pv.ratsInHouse.HasHowMany(rat.name);
				int newRats = Mathf.RoundToInt(Mathf.Pow (rats, powerOfRat));

				// YOU CAN TRY TO LIMIT US,
				if(maximumRats > -1 && rats+newRats > maximumRats){
					newRats = maximumRats;
				}
				// BUT RATS ARE AN INEVITABILITY, THEY CANNOT BE STOPPED.
				if(newRats <= minimumRats){
					newRats = minimumRats;
				}
				PlayerValues.pv.ratsInHouse.Set(rat, newRats);
				Debug.Log ("FEEDING "+col.gameObject+" TO THE RATS, "+rats.ToString()+"->"+newRats.ToString()+"|"+(newRats-rats).ToString());
				Destroy(col.gameObject);
			} else {
				Debug.Log("NOT SURE WHAT TO DO WITH "+col.gameObject+" AND IT DOESN'T LOOK LIKE A RAT TO ME.");
			}
		}
	}
}