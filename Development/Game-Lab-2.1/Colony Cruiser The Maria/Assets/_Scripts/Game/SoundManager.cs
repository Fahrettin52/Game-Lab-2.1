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

	public AudioSource[] myAudioSource;

	public AudioClip[] myAudioClips;

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

	public void OpenDoor(){
		myAudioSource [1].clip = myAudioClips [0];
		myAudioSource [1].Play ();
	}

	public void RevolverShot(){
		myAudioSource [1].clip = myAudioClips [1];
		myAudioSource [1].Play ();
	}

	public void RevolverReload(){
		myAudioSource [1].clip = myAudioClips [2];
		myAudioSource [1].Play ();
	}

	public void TakingDamage(int randomInt){
		if (randomInt == 0) {
			myAudioSource [2].clip = myAudioClips [3];
			myAudioSource [2].Play ();
		} 
		else {
			myAudioSource [2].clip = myAudioClips [4];
			myAudioSource [2].Play ();
		}
	}

	public void Turretshot(){
		myAudioSource [1].clip = myAudioClips [5];
		myAudioSource [1].Play ();
	}

	public void HoverButton(){
		myAudioSource [3].clip = myAudioClips [6];
		myAudioSource [3].Play ();
	}

	public void ButtonClick(){
		myAudioSource [3].clip = myAudioClips [7];
		myAudioSource [3].Play ();
	}
}
