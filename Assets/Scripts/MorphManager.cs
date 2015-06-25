using UnityEngine;
using System.Collections;

public class MorphManager : MonoBehaviour {

	// the extent to which a given blend can be distorted
	public float maxDistortion = 100.0f;
	// bump this up for more of a lava-lamp sorta look
	public float minDistortion = 50.0f;
	// how much to distort something if it isn't near its target goal
	public float distortAmount = 0.5f;
	// the if the distance between the target value and its current value is smaller than this, it will not be modified 
	//public float minDistortionDelta = 2.1f;
	// how many blends get modified in a given cycle, too high and the screen starts to shake
	public int blendsToModify = 5;

	private SkinnedMeshRenderer skinnedMeshRenderer;
	private Mesh skinnedMesh;
	private int blendShapeCount;

	void Awake (){
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}

	void Start (){
		// defaults
		blendShapeCount = skinnedMesh.blendShapeCount; 
	}
	
	void Update (){
		for(int i = 0; i < blendsToModify; i++){
			int targetBlend = Random.Range (0, blendShapeCount);
			float currentValue = skinnedMeshRenderer.GetBlendShapeWeight(targetBlend);

			//float targetValue = Random.Range (minDistortion, maxDistortion);

			currentValue += Random.Range (-distortAmount, distortAmount);

			// only modify if the target is X distance from the current position
			//if( minDistortionDelta < Mathf.Abs (currentValue - targetValue)){
				// but only modify it by this amount
			//	if(targetValue > currentValue){
			//		currentValue -= distortAmount;
			//	} else if(targetValue < currentValue) {
			//		currentValue -= distortAmount;
			//	}
			//}

			// don't let things get too far out of control
			if( currentValue < minDistortion ){
				currentValue = minDistortion;
			} else if( currentValue > maxDistortion ){
				currentValue = maxDistortion;
			}

			// apply the changes
			skinnedMeshRenderer.SetBlendShapeWeight (targetBlend, currentValue);
		}
	}
}