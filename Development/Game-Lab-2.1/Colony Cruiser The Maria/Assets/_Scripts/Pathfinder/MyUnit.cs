using UnityEngine;
using System.Collections;

public class MyUnit : MonoBehaviour {
	public enum WhatAmI
	{
		Roomba,
		Chicken
	}
	public WhatAmI whatAmI;
	public bool flyable;
	public float speed;
	Vector3[] path;
	int targetIndex;
	int curTarget;

	public void Awake(){
		if (whatAmI == WhatAmI.Roomba) {
			flyable = false;
		}
		else {
			flyable = true;
		}
	}

	public void RecieveTarget(Transform target){
		PFRequestMng.RequestPath (transform.position, target.position, OnPathFound, flyable);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccesful){
		if(pathSuccesful && newPath.Length > 0 && gameObject != null){
			path = newPath;
			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

	IEnumerator FollowPath(){
		Vector3 currentWaypoint = path [0];
		while (true) {
			if (transform.position == currentWaypoint) {
				targetIndex++;
				if (targetIndex >= path.Length) {
					targetIndex = 0;
					switch (whatAmI) {
					case WhatAmI.Roomba:
						GetComponent<Roomba> ().NextPatrolPoint ();
						break;
					case WhatAmI.Chicken:
						if (GetComponent<Chicken> ().myState == LiveStockEnemy.LiveStockState.Flee) {
							GetComponent<Chicken> ().myState = LiveStockEnemy.LiveStockState.Regenerate;
						}
						else {
							GetComponent<Chicken>().NextPatrolPoint();
						}
						break;
					}
					yield break;
				}
				currentWaypoint = path [targetIndex];
			}
			transform.position = Vector3.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
			yield return null;
		}
	}
}
