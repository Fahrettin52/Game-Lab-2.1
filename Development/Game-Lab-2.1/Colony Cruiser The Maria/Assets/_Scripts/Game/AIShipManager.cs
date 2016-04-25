using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIShipManager : MonoBehaviour {

    public string[] currentInfo;
    public Text AI;
	private float timer = 5;

    public void Start() {
        DialogeChecker(0);
    }

    public void DialogeChecker(int counter) {
        AI.text = currentInfo[counter];
		StartCoroutine ("TimerCoversation");
    }

	public IEnumerator TimerCoversation(){
		timer = timer;
		yield return new WaitForSeconds (timer);
		AI.text = " ";
	}
}
