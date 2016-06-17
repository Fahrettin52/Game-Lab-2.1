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

	public void DialogeChecker(int counter){
		survivorText.text = survivorConversation[counter];
		GetComponent<AudioSource> ().clip = survivorAudio [curAudio];
		GetComponent<AudioSource> ().Play();
		curAudio++;
	}

	public void DestroySurvivor(){
		panel.SetActive (false);
		Destroy (transform.gameObject,15f);
	}

	public void Startconvo (){
		panel.SetActive (true);
		DialogeChecker (currentConvo);
	}

	public void Continue(){	
		currentConvo++;
		if(currentConvo < survivorConversation.Length){
			DialogeChecker (currentConvo);
		}
		if(currentConvo >= survivorConversation.Length){
			player.GetComponent<Movement> ().enabled = true;
			player.GetComponent<WeaponManager> ().enabled = true;
			sword.GetComponent<SkinnedMeshRenderer> ().enabled = true;
			player.GetComponentInChildren<Animator> ().speed = 1;
			StartCoroutine (PositionReset(10));
			DestroySurvivor ();
		}
	}

	IEnumerator PositionReset(int wait){	
		yield return new WaitForSeconds (wait);
		player.GetComponent<Animator> ().enabled = false;
//		playerP.transform.rotation = Quaternion.Euler (0, 90, 0);
//		player.transform.rotation = Quaternion.Euler (0, 90, 0);
		player.GetComponent<CameraControl> ().myView = CameraControl.ViewType.Normal; 
	}
}
