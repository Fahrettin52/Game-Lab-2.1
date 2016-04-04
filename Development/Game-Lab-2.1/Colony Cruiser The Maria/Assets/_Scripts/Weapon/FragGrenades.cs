using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragGrenades : AbstractGrenades {
	public List<GameObject> targetList = new List<GameObject>();
	public string[] enemyStrings;

	public void Update(){
		TimerToExplode ();
	}

	public override void TimerToExplode(){
		myTimer -= Time.deltaTime;
		if(myTimer < 0){
			gameObject.GetComponent<SphereCollider> ().enabled = true;
			myTimer = 0;
		}
	}

	public override void GrenadeEffect(){
		
	}

	public void OnTriggerEnter(Collider col){
		for(int i = 0; i < enemyStrings.Length; i++){
			if(col.transform.tag == enemyStrings[i] || col.transform.tag == "Player"){
				targetList.Add (col.gameObject);
			}
		}
		print ("Aantal targets hit = " + targetList.Count);
		GrenadeEffect ();
	}
}
