using UnityEngine;
using System.Collections;

[System.Serializable]
public class FoodCategories {
	public float bun;
	public float vegetable;
	public float meat;
	public float cheese;
}

public enum FoodEnum {
	Bun,
	Vegetable,
	Meat,
	Cheese
}

public class Food : MonoBehaviour {
	public bool isFoodPope;
	public BurgBuilder bb;
	public FoodCategories foodCategories;
	public bool isGrabbed;
	public FoodEnum foodType;
	public string foodName; //I'm sorry ejew I can't think of how to do this otherwise (to evaluate if the prefab clone is the same thing as the original or w/e)

	public AudioClip grabSound;
	public AudioClip releaseSound;

	public AudioClip hitSoftSound;
	public AudioClip hitHardSound;

	private AudioSource soundmaker;

	// Use this for initialization
	void Start () {
		isGrabbed = false;
		soundmaker = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PlaySound(AudioClip clipToPlay){
		soundmaker.Stop ();
		soundmaker.clip = clipToPlay;
		soundmaker.Play ();
	}

	void OnCollisionEnter(Collision col){
		if (isFoodPope && col.gameObject.tag == "Food" && !isGrabbed && !col.gameObject.GetComponent<Food>().isGrabbed) {
			//Debug.Log ("GRANTING NEW OBJECT: "+col.gameObject);
			//isFoodPope = false;
			bb.ObtainNewPart(col.gameObject);
		}

		PlaySound (hitSoftSound);

	}

	// something to do when we're now property of the burg builder
	public void GetObtained(BurgBuilder newBurgBuilder){
		bb = newBurgBuilder;
		gameObject.transform.parent = bb.gameObject.transform;
		this.isFoodPope = true;
	}

	public void Grabbed(){
		isGrabbed = true;

		PlaySound(grabSound);

		if (isFoodPope) {
			isFoodPope = false;
			gameObject.transform.parent = null;
			bb = null;
		}
	}

	public void Released(){
		isGrabbed = false;
	}
}
