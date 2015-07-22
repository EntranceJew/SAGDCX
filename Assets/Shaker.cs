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

	private Vector3 startPos;
	private float timeOfArrival;

	private float shakeDuration = 1.0f;
	private float shakeElapsed = 0.0f;


	void Update(){
		if (doShake) {
			shakeElapsed += Time.deltaTime;
			Vector3 pos = transform.position;
			float what = Mathf.Sin (Time.time * speed) * amount;
			pos.y += what;
			transform.position = pos;
			if(shakeElapsed >= shakeDuration){
				StopShake();
			}
		} else if(timeOfArrival >= Time.time){
			Vector3 hork = Vector3.Lerp (transform.position, startPos, Time.time/timeOfArrival);
			transform.position = hork;
		}
	}

	public void StartShake(){
		StartShake (shakeDuration);
		aud.Stop ();
		aud.clip = badSound;
		aud.Play ();
	}

	public void StartShake(float duration){
		shakeDuration = duration;
		doShake = true;
		shakeElapsed = 0.0f;
	}

	public void StopShake(){
		doShake = false;
		timeOfArrival = Time.time + timeToReturn;
		aud.Stop ();
	}

	// Use this for initialization
	void Start () {
		startPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		StartShake ();
	}
}
