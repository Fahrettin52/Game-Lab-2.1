using UnityEngine;
using System.Collections;

public class StimPack : MonoBehaviour {
	public GameObject player;
	public int maxStimPack;

	public void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
	}

//	public void OnCollisionEnter(Collision col){
//		if(col.transform.tag == player.tag){
//			AddPack ();
//		}
//	}

	public void AddPack(){
		if (player.GetComponent<PackManager> ().stimPackCount < maxStimPack) {
			player.GetComponent<PackManager> ().stimPackCount++;
		}
	}
}
