using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour {
	public int maxNoDays = 0;

	public Text loadFileText;
	public GameObject loadButton;
	public GameObject clearAllButton;

	private List<string> foundSaves;
	private string saveToLoad;

	public void Start(){
		// patrol for saves here
		foundSaves = PlayerValues.pv.ScanForSaves (maxNoDays);
		if (foundSaves.Count > 0) {
			saveToLoad = foundSaves [foundSaves.Count - 1];
			loadFileText.text = loadFileText.text + "\n(" + saveToLoad + ")";
		} else {
			saveToLoad = "";
			loadButton.SetActive(false);
			clearAllButton.SetActive(false);
		}
		
	}

	public void StartGame() {
		Application.LoadLevel ("Day1");
	}

	public void LoadGame(){
		// load via
		PlayerValues.pv.Load (saveToLoad);
		Application.LoadLevel ("Day1");
	}

	public void ClearAllSaves(){
		foreach (string saveToWipe in foundSaves) {
			PlayerValues.pv.DeleteSave (saveToWipe);
		}
		loadButton.SetActive(false);
		clearAllButton.SetActive(false);
	}
}
