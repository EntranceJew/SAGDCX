using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FoodCategories {
	public float bun;
	public float vegetable;
	public float meat;
	public float cheese;
	public float condiment;
}

public enum FoodEnum {
	Bun,
	Vegetable,
	Meat,
	Cheese,
	Condiment
}

public class Food : MonoBehaviour {
	// Category Balance Stuff
	public FoodCategories foodCategories;
	public FoodEnum foodType;
	public string foodName; //I'm sorry ejew I can't think of how to do this otherwise (to evaluate if the prefab clone is the same thing as the original or w/e)
	public float price;

	public Dictionary<GameObject, bool> collisions = new Dictionary<GameObject, bool>();

	// working vars
	public bool isFoodPope;
	public BurgBuilder bb;
	public bool isGrabbed;
	public bool isFake = false;
	public bool belongsOnFloor = false;

	// noise
	public AudioClip grabSound;
	public AudioClip releaseSound;

	public AudioClip hitSoftSound;
	public AudioClip hitHardSound;

	private AudioSource soundmaker;
	private float spawnTime;
	private List<Material> materials;

	// Use this for initialization
	void Start () {
		isGrabbed = false;
		soundmaker = GetComponent<AudioSource> ();
		transform.rotation = Quaternion.Euler (new Vector3(-90, Random.Range (0,360), 0));
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PlaySound(AudioClip clipToPlay){
		if (soundmaker != null) {
			soundmaker.Stop ();
			soundmaker.clip = clipToPlay;
			soundmaker.Play ();
		}
	}

	void OnCollisionEnter(Collision col){
		collisions.Add (col.gameObject, true);
		if (ShouldObtainObject(col.gameObject)) {
			//Debug.Log ("GRANTING NEW OBJECT: "+col.gameObject);
			//isFoodPope = false;
			ForceObtain(col.gameObject);
		}
		
		if (soundmaker != null && soundmaker.mute == false) {
			if (!soundmaker.isPlaying){
				PlaySound (hitSoftSound);
			}
		}
	}

	void OnCollisionExit(Collision col){
		collisions.Remove (col.gameObject);
	}

	public bool ShouldObtainObject(GameObject obj){
		if (isFoodPope && obj.tag == "Food" && !isGrabbed && !obj.GetComponent<Food> ().isGrabbed) {
			return true;
		} else {
			return false;
		}
	}

	public void ForceObtain(GameObject obj){
		bb.ObtainNewPart (obj);
	}

	// check to see if we are somehow eligable for foodpope status through inherited collisions
	public bool IsSecretlyFoodPope(){
		foreach (KeyValuePair<GameObject, bool> item in collisions) {
			Food fd = item.Key.GetComponent<Food>();
			if (fd != null && fd.ShouldObtainObject(gameObject)){
				fd.ForceObtain(gameObject);
				Debug.Log (gameObject.name + " GRANTED BY " + item.Key.name);
				//Debug.Log (gameObject.name + " WAS REJECTED BY: " + item.Key.name);
				return true;
			}
		}
		return false;
	}

	// something to do when we're now property of the burg builder
	public void GetObtained(BurgBuilder newBurgBuilder){
		bb = newBurgBuilder;
		gameObject.transform.parent = bb.gameObject.transform;
		this.isFoodPope = true;
		/* disable the following if we're not in the future */
		//BecomeLilMac ();
	}

	public void BecomeLilMac(){
		Debug.Log ("==  WHAT'S YOUR FAVORITE KIND OF FLOWER?");
		materials = new List<Material> ();
		Material lilMat = Resources.Load("ResHighlight", typeof(Material)) as Material;
		List<Renderer> renderers = new List<Renderer> ();
		renderers.AddRange (gameObject.GetComponentsInChildren<Renderer> ());
		// ALL THE RENDERERS GET TICKLED, ALL OF 'EM.
		//Debug.Log (gameObject.GetComponents<Material> ());
		foreach (Renderer renderer in renderers) {
			Material[] newMats = new Material[renderer.materials.Length];
			renderer.materials.CopyTo(newMats, 0);
			//List<Material> newMats = new List<Material>(renderer.materials);
			materials.AddRange(newMats);
			for(int i = 0; i < newMats.Length ; i++){
				Debug.Log ("RENDERER? "+newMats[i]);
				newMats[i] = lilMat;
			}
			renderer.materials = newMats;
		}
	}

	public void UnBecomeLilMac(){
		List<Renderer> renderers = new List<Renderer> ();
		renderers.AddRange (gameObject.GetComponentsInChildren<Renderer> ());
		// ALL THE RENDERERS GET TICKLED, ALL OF 'EM.
		//Debug.Log (gameObject.GetComponents<Material> ());
		int gi = 0;
		foreach (Renderer renderer in renderers) {
			Material[] newMats = new Material[renderer.materials.Length];
			renderer.materials.CopyTo(newMats, 0);
			for(int i = 0; i < newMats.Length; i++){
				newMats[i] = materials[gi];
			}
			renderer.materials = newMats;
			gi++;
		}
		materials = new List<Material> ();
	}

	// @TODO: Make this disentegrate it like the portal fizzler instead.
	public void Emancipate(){
		isFoodPope = false;
		gameObject.transform.parent = null;
		bb = null;
		//UnBecomeLilMac ();
	}

	public void Grabbed(){
		isGrabbed = true;
		if (soundmaker.mute) {
			soundmaker.mute = false;
		}

		if (belongsOnFloor) {
			belongsOnFloor = false;
		}

		PlaySound(grabSound);

		if (bb != null) {
			bb.EmancipatePart(gameObject);
		}
	}

	public void Released(){
		isGrabbed = false;
		IsSecretlyFoodPope ();
	}
}