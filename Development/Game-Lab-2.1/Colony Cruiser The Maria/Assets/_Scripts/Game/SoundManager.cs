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
}
