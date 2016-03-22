using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIShipManager : MonoBehaviour {

    public string[] currentInfo;
    public Text AI;

    public void Start() {
        DialogeChecker(0);
    }

    public void DialogeChecker(int counter) {
        AI.text = currentInfo[counter];
    }
}
