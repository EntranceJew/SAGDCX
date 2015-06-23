using UnityEngine;
using System.Collections;

public class Kitty : MonoBehaviour {

	public int Max = 25;
	public float MoveBy = 0.1f;
	private int blendShapeCount;
	private SkinnedMeshRenderer skinnedMeshRenderer;
	private Mesh skinnedMesh;
	private int[] targetBlend;
	private float[] targetValue;
	private float[] currentValue;

	void Awake ()
	{
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		skinnedMesh = GetComponent<SkinnedMeshRenderer> ().sharedMesh;
	}

	void Start ()
	{
		blendShapeCount = skinnedMesh.blendShapeCount; 
		for(int i = 0; i < 6; i++){
			targetBlend = new int[i];
			targetValue = new float[i];
			currentValue = new float[i];
		}
	}
	
	void Update ()
	{
		for(int i = 0; i < 5; i++){
			if (targetValue[i] - currentValue[i] < MoveBy * 2.1f) {
				targetBlend[i] = Random.Range (0, blendShapeCount);
				targetValue[i] = Random.Range (0, Max);
				currentValue[i] = skinnedMeshRenderer.GetBlendShapeWeight(targetBlend[i]);
			}
			if(targetValue[i] > currentValue[i]){
				currentValue[i] = currentValue[i] + MoveBy;
			}
			else
			{
				currentValue[i] = currentValue[i] - MoveBy;
			}

			skinnedMeshRenderer.SetBlendShapeWeight (targetBlend[i], currentValue[i]);
		}
	}
}