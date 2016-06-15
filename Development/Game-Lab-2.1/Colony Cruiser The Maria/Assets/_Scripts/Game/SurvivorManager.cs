using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivorManager : MonoBehaviour {

	public string[] survivorConversation;

	public Text survivorText;

	public GameObject panel;
	public GameObject player;
	public GameObject sword;

	private Quaternion lastRotation;

	public int currentConvo;

	public void Start (){
		lastRotation = player.GetComponent<Transform> ().transform.rotation;
	}

	public void DialogeChecker(int counter){
		survivorText.text = survivorConversation[counter];
	}

	public void DestroySurvivor(){
		panel.SetActive (false);
		Destroy (transform.gameObject);
	}

	public void OnTriggerEnter (Collider col){
		if(col.transform.tag == "Player"){
			panel.SetActive (true);
			DialogeChecker (currentConvo);
		}
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
