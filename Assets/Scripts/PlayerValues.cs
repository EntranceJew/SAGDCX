using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
class ValuesForPlayer {
	public int dayNumber;
	public float cash;
	public List<SerializableInventoryItem> inStock;
}

public class PlayerValues : MonoBehaviour {
	// if this number becomes bigger than the size of DayValues then all hell will break loose
	public int dayNumber;
	
	// how much money the player has at any time, this will impact purchases and deductions
	public float cash;
	
	// here is where all the everything ever lives
	public Inventory inventory;

	// :siren: WATCH OUT IT'S I/O TIME :siren:
	public void Save(){
		Debug.Log ("TRYING TO SAVE!!!!");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath+"/PlayerValues.dat");

		// @TODO: Try this with the base GameObject and see if this is truly necessary.
		ValuesForPlayer vals = new ValuesForPlayer();
		vals.dayNumber = dayNumber;
		vals.cash = cash;

		List<SerializableInventoryItem> serStock = new List<SerializableInventoryItem> ();
		foreach (InventoryItem item in inventory.stock) {
			serStock.Add (item.Serialize ());
		}
		vals.inStock = serStock;

		bf.Serialize (file, vals);
		file.Close ();
	}

	public void Load(){
		Debug.Log ("TRYING TO LOAD!!!!");
		if (File.Exists (Application.persistentDataPath + "/PlayerValues.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath+"/PlayerValues.dat", FileMode.Open);
			ValuesForPlayer vals = (ValuesForPlayer)bf.Deserialize(file);
			file.Close();

			dayNumber = vals.dayNumber;
			cash = vals.cash;

			List<InventoryItem> unserStock = new List<InventoryItem> ();
			foreach (SerializableInventoryItem item in vals.inStock) {
				unserStock.Add (new InventoryItem(inventory.fl.GetGameObject(item.name), item.quantity));
			}
			inventory.stock = unserStock;

			Debug.Log ("LOADED ALL MY VALUES UP GOOD");
		} else {
			Debug.Log ("NO FILE, DINDU NOTHIN'");
		}
	}

	// IF, FOR WHATEVER REASON WE NEED THIS LOADED BEFORE THE MAIN DAY SCENE, CONSULT:
	// http://unity3d.com/learn/tutorials/modules/beginner/live-training-archive/persistence-data-saving-loading

	public bool CanAfford(float dosh){
		return cash >= dosh;
	}

	public float Spend(float lodsemone){
		if (CanAfford (lodsemone)) {
			Debug.Log ("Bought some stuff for $"+lodsemone.ToString("F2")+"!");
			return cash -= lodsemone;
		} else {
			return cash;
		}
	}
}
