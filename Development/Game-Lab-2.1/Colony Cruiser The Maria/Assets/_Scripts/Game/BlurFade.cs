using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class BlurFade : MonoBehaviour {

	public bool mayBlur;
	public bool mayEye;

	public void Start(){
		mayEye = true;
	}

	void FixedUpdate () {	
		if (mayBlur) {
			if (GetComponent<DepthOfField>().focalLength > 0) {
				GetComponent<DepthOfField>().focalLength -= Time.deltaTime * 2f;
			}
			if(GetComponent<DepthOfField>().focalLength <= 1) {
				GetComponent<DepthOfField> ().enabled = false;
			}
		}
	}
}
