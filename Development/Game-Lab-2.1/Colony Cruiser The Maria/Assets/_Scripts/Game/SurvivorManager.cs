using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivorManager : MonoBehaviour {

	public string[] survivorConversation;
	public AudioClip[] survivorAudio;

	public Text survivorText;

	public GameObject panel;
	public GameObject player;
	public GameObject sword;

	private Quaternion lastRotation;

	public int currentConvo;
	public int curAudio;

	public void Start (){
		lastRotation = player.GetComponent<Transform> ().transform.rotation;
	}

	public void DialogeChecker(int counter){
		survivorText.text = survivorConversation[counter];
		GetComponent<AudioSource> ().clip = survivorAudio [curAudio	];
		GetComponent<AudioSource> ().Play();
		curAudio++;
	}

	public void DestroySurvivor(){
		panel.SetActive (false);
		Destroy (transform.gameObject);
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
			player.GetComponent<CameraControl> ().myView = CameraControl.ViewType.Normal;
			player.GetComponent<WeaponManager> ().enabled = true;
			sword.GetComponent<SkinnedMeshRenderer> ().enabled = true;
			DestroySurvivor ();
		}
	}
}
