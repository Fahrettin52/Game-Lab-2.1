using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

	public AudioMixer myMixer;
	public Slider masterSlider;
	public Slider ambientSlider;
	public Slider sfxSlider;
	public Slider voiceSlider;

	public void ChangeMasterSound(){
		myMixer.SetFloat ("MasterVolume", masterSlider.value);
	}

	public void ChangeAmbientSound(){
		myMixer.SetFloat ("AmbientVolume", ambientSlider.value);
	}

	public void ChangeSFXSound(){
		myMixer.SetFloat ("SFXVolume", sfxSlider.value);
	}

	public void ChangeVoiceSound(){
		myMixer.SetFloat ("VoiceVolume", voiceSlider.value);
	}
}
