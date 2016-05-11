using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivorManager : MonoBehaviour {

	public string[] survivorConversation;
	public Text survivorText;
	public GameObject panel;
	public int currentConvo;

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
			DestroySurvivor ();
		}
	}
}
