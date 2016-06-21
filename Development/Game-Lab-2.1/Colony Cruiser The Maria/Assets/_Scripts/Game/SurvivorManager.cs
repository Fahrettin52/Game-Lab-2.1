using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivorManager : MonoBehaviour {

	public string[] survivorConversation;
	public AudioClip[] survivorAudio;

	public Text survivorText;

	public GameObject panel;
	public GameObject player;
	public GameObject playerP;
	public GameObject sword;
	public GameObject hands;

	public int currentConvo;
	public int curAudio;

	public Animator playerAnimator;

	public IEnumerator DialogeChecker(int counter){
		survivorText.text = survivorConversation [counter];
		GetComponent<AudioSource> ().clip = survivorAudio [curAudio];
		GetComponent<AudioSource> ().Play ();
		curAudio++;
		yield return new WaitForSeconds (GetComponent<AudioSource> ().clip.length);
	}

	public void DestroySurvivor(){
		panel.SetActive (false);
		Destroy (transform.gameObject,15f);
	}

	public void Startconvo (){
		panel.SetActive (true);
		StartCoroutine (DialogeChecker(currentConvo));
	}

	public void Continue(){	
		if(currentConvo < survivorConversation.Length && GetComponent<AudioSource>().isPlaying == false){
			currentConvo++;
			StartCoroutine (DialogeChecker(currentConvo));
		}
		if(currentConvo >= survivorConversation.Length){
			player.GetComponent<Movement> ().enabled = true;
			player.GetComponent<WeaponManager> ().enabled = true;
			player.GetComponentInChildren<Animator> ().speed = 1;
			StartCoroutine (PositionReset(6));
			DestroySurvivor ();
		}
	}

	IEnumerator PositionReset(int wait){	
		yield return new WaitForSeconds (wait);
		playerAnimator.SetBool ("SwordEquip", true);
		hands.GetComponent<SkinnedMeshRenderer> ().enabled = true;
		sword.GetComponent<SkinnedMeshRenderer> ().enabled = true;
		player.GetComponent<WeaponManager> ().WeaponSwitch ();
		player.GetComponent<Animator> ().enabled = false;
		player.GetComponent<CameraControl> ().myView = CameraControl.ViewType.Normal; 
	}
}
