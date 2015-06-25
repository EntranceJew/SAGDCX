using UnityEngine;
using System.Collections;

public class MorphManager : MonoBehaviour {

	// the extent to which a given blend can be distorted
	public float maxDistortion = 100.0f;
	// bump this up to force things to at least be this messed up
	public float minDistortion = 50.0f;
	// the shortest possible time a blend could take to lerp into position
	public float minDistortionTime = 1.0f;
	// the longest possible time a blend could take to lerp into position
	public float maxDistortionTime = 10.0f;

	private SkinnedMeshRenderer skinnedMeshRenderer;
	private Mesh skinnedMesh;
	private int blendShapeCount;
	private float[] targetValues;
	// note: we could get rid of the starting values and
	// just have a logarithmic approach to the desired value using currentValue
	private float[] startingValues;
	private float[] finishingTimes;
	
	void Awake (){
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}

	void Start (){
		// defaults
		blendShapeCount = skinnedMesh.blendShapeCount; 
		targetValues = new float[blendShapeCount];
		startingValues = new float[blendShapeCount];
		finishingTimes = new float[blendShapeCount];

		for (int i = 0; i < blendShapeCount; i++) {
			ResetBlend(i);
		}
	}
	
	void Update (){
		for(int i = 0; i < blendShapeCount; i++){
			float currentValue = skinnedMeshRenderer.GetBlendShapeWeight(i);

			currentValue = Mathf.Lerp (startingValues[i], targetValues[i], Time.time / finishingTimes[i]);

			// you've been at this for too long, cut it out
			if(Time.time >= finishingTimes[i]){
				ResetBlend(i, currentValue);
			}

			// don't let things get too far out of control
			if( currentValue < minDistortion ){
				ResetBlend(i, minDistortion);
			} else if( currentValue > maxDistortion ){
				ResetBlend(i, maxDistortion);
			}

			// apply the changes
			skinnedMeshRenderer.SetBlendShapeWeight (i, currentValue);
		}
	}

	void ResetBlend(int i, float newVal){
		targetValues[i] = Random.Range (minDistortion, maxDistortion);
		finishingTimes[i] = Time.time + Random.Range (minDistortionTime, maxDistortionTime);

		startingValues [i] = newVal;
	}

	// OH SWEET BABY JESUS I'M OVERRIDING A METHOD.
	void ResetBlend(int i){
		targetValues [i] = Random.Range (minDistortion, maxDistortion);
		finishingTimes [i] = Time.time + Random.Range (minDistortionTime, maxDistortionTime);

		startingValues [i] = skinnedMeshRenderer.GetBlendShapeWeight (i);
	}
}