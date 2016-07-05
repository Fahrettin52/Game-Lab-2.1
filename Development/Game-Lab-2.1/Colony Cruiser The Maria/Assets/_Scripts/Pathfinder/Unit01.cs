﻿using UnityEngine;
using System.Collections;

public class Unit01 : MonoBehaviour {

	public Transform target;
	public float speed;
	public bool flyable;
	Vector3[] path;
	int targetIndex;

	void Start(){
		PFRequestMng.RequestPath (transform.position, target.position, OnPathFound, flyable);
	}

	public void OnPathFound(Vector3[] newPath, bool pathSuccesful){
		if(pathSuccesful){
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
					yield break;
				}
				currentWaypoint = path [targetIndex];
			}
			transform.position = Vector3.MoveTowards (transform.position, currentWaypoint,speed);
			yield return null;
		}
	}
}
