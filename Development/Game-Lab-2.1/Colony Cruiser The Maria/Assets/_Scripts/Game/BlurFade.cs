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
			if (GetComponent<VignetteAndChromaticAberration>().blur > 0) {
				GetComponent<VignetteAndChromaticAberration>().blur -= Time.deltaTime * 0.05f;
			}
			if (GetComponent<VignetteAndChromaticAberration>().blur <= 0) {
				GetComponent<VignetteAndChromaticAberration>().enabled = false;
			}
		}
	}
}
