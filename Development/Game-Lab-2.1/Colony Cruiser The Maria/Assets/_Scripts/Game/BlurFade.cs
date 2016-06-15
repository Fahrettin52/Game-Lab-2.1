using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class BlurFade : MonoBehaviour {

	public bool mayBlur;
	public bool mayEye;

	public void Start(){
		mayEye = true;
	}

	void FixedUpdate () {	
//		StartCoroutine (EyesOpen());
		if (mayBlur) {
			if (GetComponent<VignetteAndChromaticAberration>().blur > 0) {
				GetComponent<VignetteAndChromaticAberration>().blur -= Time.deltaTime * 0.1f;
			}
			if (GetComponent<VignetteAndChromaticAberration>().blur <= 0) {
				GetComponent<VignetteAndChromaticAberration>().enabled = false;
			}
		}
	}

//	IEnumerator EyesOpen(){
//		while (true) {
//			if (GetComponent<VignetteAndChromaticAberration> ().intensity >= 0f && mayEye == true) {
//				GetComponent<VignetteAndChromaticAberration> ().intensity -= Time.deltaTime * 0.005f;
//			} 
//			else{
//				GetComponent<VignetteAndChromaticAberration> ().intensity += Time.deltaTime * 0.001f;
//				mayEye = false;
//			}
//			yield return new WaitForSeconds (0.01f);
//			if (GetComponent<VignetteAndChromaticAberration> ().intensity >= 1f) {
//				mayEye = true;
//			} 
//		}
//	}
}
