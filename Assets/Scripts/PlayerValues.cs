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

	private string defaultSaveName = "slot_1";

	// :siren: WATCH OUT IT'S I/O TIME :siren:
	public void Save(){
		Save (defaultSaveName);
	}

	public void Save(string filename){
		Debug.Log ("TRYING TO SAVE: " + filename + ".save");
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath+"/"+filename+".save");

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
		Load (defaultSaveName);
	}

	public void Load(string filename){
		Debug.Log ("TRYING TO LOAD: " + filename + ".save");
		if (File.Exists (Application.persistentDataPath + "/" + filename + ".save")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath+ "/" + filename + ".save", FileMode.Open);
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
