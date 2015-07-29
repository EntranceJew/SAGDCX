using UnityEngine;
using System.Collections;

// adapted from: http://forum.unity3d.com/threads/shake-an-object-from-script.138382/#post-941968
public class Shaker : MonoBehaviour {
	//how fast it shakes
	public float speed = 20.0f; 
	//how much it shakes
	public float amount = 1.0f; 

	// how long it takes to get back into spawnpos
	public float timeToReturn = 1.0f;

	public bool doShake = false;

	public AudioSource aud;
	public AudioClip badSound;
	public AudioClip goodSound;
	public bool isShakeGood = false;

	public MoveToCameraOnClick mtcoc;
	
	private Vector3 startShakePos;
	private float timeOfArrival = Mathf.NegativeInfinity;

	private float shakeDuration = 1.0f;
	private float shakeElapsed = 0.0f;
	private AudioClip clipToPlay;


	void Update(){
		if (doShake) {
			shakeElapsed += Time.deltaTime;
			Vector3 pos = transform.position;
			float what = Mathf.Sin (Time.time * speed) * amount;
			if(isShakeGood){
				pos.y += what;
			} else {
				Vector3 xpos = transform.right;
				// it looks really bad when we don't do it like this, so we half the transform
				pos += what * xpos * 0.5f;
			}
			transform.position = pos;
			if(shakeElapsed >= shakeDuration){
				StopShake();
			}
		} else if(timeOfArrival >= Time.time){
			Vector3 hork = Vector3.Lerp (transform.position, startShakePos, Time.time/timeOfArrival);
			transform.position = hork;
		}
	}

	public void StartShake(){
		StartShake (shakeDuration);
	}

	public void StartShake(float duration){
		if (mtcoc && mtcoc.inMotion) {
			return;
		}
		startShakePos = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		shakeDuration = duration;
		doShake = true;
		shakeElapsed = 0.0f;
		aud.Stop ();
		aud.clip = clipToPlay;
		aud.Play ();
	}

	public void StopShake(){
		doShake = false;
		timeOfArrival = Time.time + timeToReturn;
		aud.Stop ();
		/*
		if (mtcoc.inPosition) {
			timeOfArrival = Mathf.NegativeInfinity;
		}
		*/
	}

	public void GoodShake(){
		GoodShake (shakeDuration);
	}

	public void GoodShake(float duration){
		isShakeGood = true;
		clipToPlay = goodSound;
		StartShake (duration);
	}

	public void BadShake(){
		BadShake (shakeDuration);
	}

	public void BadShake(float duration){
		isShakeGood = false;
		clipToPlay = badSound;
		StartShake (duration);
	}
}
