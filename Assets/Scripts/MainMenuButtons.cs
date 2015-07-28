using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenuButtons : MonoBehaviour {
	public int maxNoDays = 0;

	public Text loadFileText;
	public GameObject loadButton;
	public GameObject clearAllButton;

	public AudioMixerSnapshot title;
	public AudioMixerSnapshot game;

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
		game.TransitionTo (1.0f);
		Application.LoadLevel ("Day1");
	}

	public void LoadGame(){
		// load via
		game.TransitionTo (1.0f);
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
