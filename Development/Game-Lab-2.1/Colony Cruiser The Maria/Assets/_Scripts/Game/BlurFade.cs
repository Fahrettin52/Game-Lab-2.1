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
				GetComponent<DepthOfField>().focalLength -= Time.deltaTime * 2.5f;
			}
			if(GetComponent<DepthOfField>().focalLength <= 1) {
				GetComponent<DepthOfField> ().enabled = false;
			}
			if (GetComponent<VignetteAndChromaticAberration>().blur > 0) {
				GetComponent<VignetteAndChromaticAberration>().blur -= Time.deltaTime * 0.03f;
			}
			if(GetComponent<VignetteAndChromaticAberration>().blurSpread <= 1) {
				GetComponent<VignetteAndChromaticAberration>().enabled = false;
			}
		}
	}
}
