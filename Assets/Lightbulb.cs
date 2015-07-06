using UnityEngine;
using System.Collections;

public class Lightbulb : MonoBehaviour {
	public Material litUp;
	public Material unLit;
	public Renderer rend;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer>();
		rend.enabled = true;
		UnLight ();
	}
	
	// Update is called once per frame
	void Update () {
		//debug material switching
		//if (Time.time > 5.0f) {
		//	LightUp ();
		//}
	}

	public void LightUp(){
		rend.sharedMaterial = litUp;
	}

	public void UnLight(){
		rend.sharedMaterial = unLit;
	}
}
