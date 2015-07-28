using UnityEngine;
using System.Collections;

public class Recyclotron : MonoBehaviour {
	void OnTriggerEnter(Collider col){
		PlayerValues.pv.inventory.Add (col.gameObject, 1);
		Destroy (col.gameObject);
		AudioSource audio = GetComponent<AudioSource> ();
		if (!audio.isPlaying) {
			audio.pitch = Random.Range(0.85f, 1.15f);
			audio.Play ();
		}
	}
}