using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using UnityEngine.UI;

public class BlurFade : MonoBehaviour {

	public bool mayBlur;
	public bool white;
	public bool saturation;
	public bool blur;

	public AnimationCurve whiteCurve;
	public AnimationCurve blurCurve;
	public AnimationCurve colorCorrection;

	void FixedUpdate () {	
		if (GetComponent<Tonemapping>().white <= 2 && white == true) {
			GetComponent<Tonemapping>().white = whiteCurve.Evaluate(Time.time * 0.055f);
		}

		if (GetComponent<ColorCorrectionCurves> ().saturation <= 1 && saturation == true) {
			GetComponent<ColorCorrectionCurves> ().saturation = colorCorrection.Evaluate(Time.time * 0.025f);
			//GetComponent<ColorCorrectionCurves> ().saturation = Mathf.Lerp(0.1f, 1, Time.time * 0.02f);
			if (GetComponent<ColorCorrectionCurves> ().saturation > 1) {
				GetComponent<ColorCorrectionCurves> ().saturation = 1;
				saturation = false;
			}
		}

		if (GetComponent<Blur> ().blurSpread > 0 && blur == true) {
			GetComponent<Blur> ().blurSpread = blurCurve.Evaluate(Time.time * 0.03f);
			if (GetComponent<Blur> ().blurSpread <= 0) {
				GetComponent<Blur> ().enabled = false;
			}
		}
		if (mayBlur) {
			if (GetComponent<VignetteAndChromaticAberration>().blur > 0) {
				GetComponent<VignetteAndChromaticAberration>().blur -= Time.deltaTime * 0.015f;
			}
			if(GetComponent<VignetteAndChromaticAberration>().blur <= 0) {
				GetComponent<VignetteAndChromaticAberration>().enabled = false;
			}
			if (GetComponent<DepthOfField>().aperture > 0) {
				GetComponent<DepthOfField>().aperture -= Time.deltaTime * 0.015f;
			}
			if(GetComponent<DepthOfField>().aperture <= 0) {
				GetComponent<DepthOfField>().enabled = false;
			}
		}
	}
}
