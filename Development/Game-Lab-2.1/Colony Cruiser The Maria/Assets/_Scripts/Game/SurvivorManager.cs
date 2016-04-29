using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SurvivorManager : MonoBehaviour {

	public string[] survivorConversation;
	public Text survivorText;

	public void DialogeChecker(int counter){
		survivorText.text = survivorConversation[counter];
	}

	public void DestroySurvivor(){
		
	}

	public void OnTriggerEnter (Collider col){
		if (col.transform.tag == "Player"){
			DialogeChecker (0);
		}
	}
}
