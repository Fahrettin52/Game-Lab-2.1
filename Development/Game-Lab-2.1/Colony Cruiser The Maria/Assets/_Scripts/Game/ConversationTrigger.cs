using UnityEngine;
using System.Collections;

public class ConversationTrigger : MonoBehaviour {

    public GameObject AI;
    public int arrayCurrent;

    public void Start() {
        AI = GameObject.Find("AIShip");
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.transform.tag == "Player") {
            arrayCurrent++;
            AI.GetComponent<AIShipManager>().DialogeChecker(arrayCurrent);
            Destroy(gameObject);
        }
    }
}