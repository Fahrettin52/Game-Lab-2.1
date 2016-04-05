using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FragGrenades : AbstractGrenades {
	public List<GameObject> targetList = new List<GameObject>();
	public string[] enemyStrings;
	public Rigidbody rigidBody;
	public float throwingPower;
	public float maxRange;
	public bool colliderEnabled;

	public void OnEnable(){
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.AddForce (transform.forward * throwingPower);
		StartCoroutine("TimerToExplode");
	}

	public void Update(){
		gameObject.GetComponent<SphereCollider> ().enabled = colliderEnabled;
	}

	public override IEnumerator TimerToExplode(){
		yield return new WaitForSeconds (myTimer);	
		colliderEnabled = true;
	}

	public override void GrenadeEffect(){
		for(int i = 0; i < targetList.Count; i++){
			float distance = Vector3.Distance (transform.localPosition, targetList [i].GetComponent<Transform> ().localPosition);
			print ("distance = " + distance);
			print ("Target = " + targetList[i].GetComponent<Transform>().tag);
		}
	}

	public void OnTriggerEnter(Collider col){
		colliderEnabled = false;
		for(int i = 0; i < enemyStrings.Length; i++){
			if(col.transform.tag == enemyStrings[i] || col.transform.tag == "Player"){
				targetList.Add (col.gameObject);
				break;
			}
		}
		print ("Aantal targets hit = " + targetList.Count);
		GrenadeEffect ();
		StopCoroutine ("TimerToExplode");
	}
}
