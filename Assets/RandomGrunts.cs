using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomGrunts : MonoBehaviour {
	public AudioSource audioSource;

	// wait times if we're chill
	public float minCalmTimeToWait;
	public float maxCalmTimeToWait;

	// wait times if we're freaking the fuck out
	public float minStressedTimeToWait;
	public float maxStressedTimeToWait;

	public List<AudioClip> soundsToPlay;
	public int lastPlayedIndex;
	public float nextPlayTime;

	public float burgStartTime;
	// How long before we reach maximum grunt frequency.
	public float burgMaximumGruntTime;

	// We multiply the min/max waits by this 
	public float stressScale;

	public void SetStartTime(float time){
		burgStartTime = time;
	}

	// from: http://stackoverflow.com/a/4229711/4871680
	public float ConvertRange(
		float originalStart, float originalEnd, // original range
		float newStart, float newEnd, // desired range
		float value) // value to convert
	{
		float scale = (float)(newEnd - newStart) / (originalEnd - originalStart);
		return (float)(newStart + ((value - originalStart) * scale));
	}


	void PlaySound(int index){
		audioSource.Stop ();
		audioSource.clip = soundsToPlay [index];
		audioSource.Play ();
	}

	// Use this for initialization
	void Start () {
		SetNextTime ();
	}

	void SetNextTime(){
		// scale how long we've been waiting to max freakout time
		stressScale = (Time.time-burgStartTime) / burgMaximumGruntTime;
		if (stressScale > 1) {
			stressScale = 1;
		}

		// get current min/maxes by scaling the position against their ranges
		float currentMin = ConvertRange (0, 1, minCalmTimeToWait, minStressedTimeToWait, stressScale);
		float currentMax = ConvertRange (0, 1, minCalmTimeToWait, minStressedTimeToWait, stressScale);

		// get the current time in our new relative ranges
		float time = Random.Range (currentMin, currentMax);

		nextPlayTime = Time.time + time;
	}

	int PickASound(){
		int newSound = lastPlayedIndex;
		while (newSound == lastPlayedIndex) {
			newSound = Random.Range (0, soundsToPlay.Count);
		}
		return newSound;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= nextPlayTime) {
			int soundIndex = PickASound();
			PlaySound(soundIndex);
			lastPlayedIndex = soundIndex;
			SetNextTime();
		}

		/*
		// only uncomment this if you intend to peep it in the inspector
		stressScale = (Time.time-burgStartTime) / burgMaximumGruntTime;
		if (stressScale > 1) {
			stressScale = 1;
		}
		*/
	}
}
