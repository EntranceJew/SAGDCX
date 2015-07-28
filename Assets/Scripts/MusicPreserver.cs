using UnityEngine;
using System.Collections;

public class MusicPreserver : MonoBehaviour {

	private static MusicPreserver instance = null;
	public static MusicPreserver Instance {
		get { return instance; }
	}

	public AudioSource soundmaker;

	void Awake () {
		if (instance != null && instance != this) {
			Destroy (this.gameObject);
			return;
		} else {
			instance = this;
		}

		DontDestroyOnLoad (this.gameObject);
		if (!soundmaker.isPlaying) {
			soundmaker.Play ();
		}

	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
