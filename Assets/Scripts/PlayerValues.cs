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
	public bool[] arrows;
	public List<DayOrderScore> scores;
	public int ratsInHouse;
	public List<MenuAbstractionSave> abstractorState;
}

[System.Serializable]
public class OrderScore {
	public OrderScore(int val, int maxVal){
		value = val;
		maxValue = maxVal;
	}

	public int value;
	public int maxValue;
}

[System.Serializable]
public class DayOrderScore {
	public DayOrderScore(int value, int maxValue){
		scores = new List<OrderScore> ();
		scores.Add (new OrderScore (value, maxValue));
	}

	public List<OrderScore> scores;
}

public class PlayerValues : MonoBehaviour {

	public static PlayerValues pv;

	public string[] savesFound;

	void Awake(){
		if (pv == null) {
			DontDestroyOnLoad (gameObject);
			DontDestroyOnLoad (inventory.gameObject);
			DontDestroyOnLoad (ratsInHouse.gameObject);
			pv = this;
		} else if(pv != this){
			// kill yourself squidward
			Debug.Log ("DESTROYED SELF, OVERPOPULATED");
			Destroy (gameObject);
		}
	}

	void Start(){
		List<string> savesToPeep = ScanForSaves (7);
		Debug.Log (savesToPeep);
	}

	public List<string> ScanForSaves(int maxDays){
		List<string> possibleNames = new List<string> ();
		possibleNames.Add (defaultSaveName);
		for (int i = 0; i < maxDays + 1; i++) {
			possibleNames.Add ("autosave_day_" + i.ToString ());
		}

		List<string> foundNames = new List<string> ();
		foreach (string possibleName in possibleNames) {
			if (Application.isWebPlayer) {
				if (PlayerPrefs.HasKey (possibleName + ".save")) {
					//Debug.Log ("FOUND SAVE: "+possibleName+".save");
					foundNames.Add (possibleName);
				}
			} else {
				if (File.Exists (Application.persistentDataPath + "/" + possibleName + ".save")) {
					//Debug.Log ("FOUND SAVE: "+possibleName+".save");
					foundNames.Add (possibleName);
				}
			}
		}

		return foundNames;
	}

	// if this number becomes bigger than the size of DayValues then all hell will break loose
	public int dayNumber;
	
	// how much money the player has at any time, this will impact purchases and deductions
	public float cash;
	
	// here is where all the everything ever lives
	public Inventory inventory;

	// how many rats are around
	public Inventory ratsInHouse;

	// the menu abstractor
	public MenuAbstractor menuAbs;

	// the scores achieved during the day and their maximums
	public List<DayOrderScore> scores;

	//ARROW STUFF!
	//Order: Picture Frame, TV, Clipboard
	public bool[] arrows = new bool[3] {true, true, true};

	private string defaultSaveName = "slot_1";

	// :siren: WATCH OUT IT'S I/O TIME :siren:
	public void DeleteSave(string filename){
		if (Application.isWebPlayer) {
			if (PlayerPrefs.HasKey (filename + ".save")) {
				PlayerPrefs.DeleteKey(filename+".save");
				PlayerPrefs.Save();
			}
		} else {
			if (File.Exists (Application.persistentDataPath + "/" + filename + ".save")) {
				File.Delete(Application.persistentDataPath + "/" + filename + ".save");
			}
		}
	}


	public void Save(){
		Save (defaultSaveName);
	}

	public void Save(string filename){
		Debug.Log ("TRYING TO SAVE: " + filename + ".save");
		BinaryFormatter bf = new BinaryFormatter ();

		// @TODO: Try this with the base GameObject and see if this is truly necessary.
		ValuesForPlayer vals = new ValuesForPlayer();
		Debug.Log ("UH " + dayNumber + "  HOMES?");
		vals.dayNumber = dayNumber;
		vals.cash = cash;
		vals.arrows = arrows;
		vals.scores = scores;
		vals.abstractorState = menuAbs.Save ();

		vals.ratsInHouse = ratsInHouse.HasHowMany ("Rat");

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
			PlayerPrefs.Save();
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
		Debug.Log ("YO " + dayNumber + " HOMES " + vals.dayNumber);
		dayNumber = vals.dayNumber;
		cash = vals.cash;
		arrows = vals.arrows;
		scores = vals.scores;

		menuAbs.Load (vals.abstractorState);

		List<InventoryItem> unserRatsInHouse = new List<InventoryItem> ();
		unserRatsInHouse.Add (new InventoryItem (FoodLookup.fl.GetGameObject ("Rat"), vals.ratsInHouse));
		ratsInHouse.stock = unserRatsInHouse;

		List<InventoryItem> unserStock = new List<InventoryItem> ();
		foreach (SerializableInventoryItem item in vals.inStock) {
			unserStock.Add (new InventoryItem(FoodLookup.fl.GetGameObject(item.name), item.quantity));
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

	public float Earn(float spendoli){
		Debug.Log ("Just earned $"+spendoli.ToString("F2")+"!");
		return cash += spendoli;
	}

	public void AddScores(int obtained, int maximum){
		if (scores.Count < dayNumber + 1) {
			DayOrderScore dos = new DayOrderScore (obtained, maximum);
			scores.Add (dos);
		} else {
			scores[dayNumber].scores.Add (new OrderScore (obtained, maximum));
		}
	}
}
