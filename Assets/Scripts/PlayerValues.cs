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

		// @TODO: Try this with the base GameObject and see if this is truly necessary.
		ValuesForPlayer vals = new ValuesForPlayer();
		vals.dayNumber = dayNumber;
		vals.cash = cash;

		List<SerializableInventoryItem> serStock = new List<SerializableInventoryItem> ();
		foreach (InventoryItem item in inventory.stock) {
			serStock.Add (item.Serialize ());
		}
		vals.inStock = serStock;

		if (Application.isWebPlayer) {
			MemoryStream ms = new MemoryStream();
			bf.Serialize(ms, vals);
			string filedata = System.Convert.ToBase64String(ms.ToArray ());
			PlayerPrefs.SetString(filename+".save", filedata);
			Debug.LogError ("SERIAL: "+filedata);
		} else {
			FileStream file = File.Create (Application.persistentDataPath + "/" + filename + ".save");
			bf.Serialize (file, vals);
			file.Close ();
		}
	}

	public void Load(){
		Load (defaultSaveName);
	}

	public bool Load(string filename){
		Debug.Log ("TRYING TO LOAD: " + filename + ".save");

		BinaryFormatter bf = new BinaryFormatter ();
		ValuesForPlayer vals;

		if (Application.isWebPlayer) {
			if (PlayerPrefs.HasKey(filename+".save")){
				string data = PlayerPrefs.GetString(filename+".save");
				//Debug.LogError ("DESERIAL: "+data);
				MemoryStream ms = new MemoryStream(System.Convert.FromBase64String (data));
				vals = (ValuesForPlayer)bf.Deserialize(ms);
			} else {
				Debug.Log ("NO FILE, DINDU NOTHIN'");
				return false;
			}
		} else {
			if (File.Exists (Application.persistentDataPath + "/" + filename + ".save")) {
				FileStream file = File.Open (Application.persistentDataPath+ "/" + filename + ".save", FileMode.Open);
				vals = (ValuesForPlayer)bf.Deserialize(file);
				file.Close();
			} else {
				Debug.Log ("NO FILE, DINDU NOTHIN'");
				return false;
			}
		}

		dayNumber = vals.dayNumber;
		cash = vals.cash;

		List<InventoryItem> unserStock = new List<InventoryItem> ();
		foreach (SerializableInventoryItem item in vals.inStock) {
			unserStock.Add (new InventoryItem(inventory.fl.GetGameObject(item.name), item.quantity));
		}
		inventory.stock = unserStock;

		Debug.Log ("LOADED ALL MY VALUES UP GOOD");
		return true;
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
