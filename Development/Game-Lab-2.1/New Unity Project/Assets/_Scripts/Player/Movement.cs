﻿using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	public int moveSpd;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0){
			transform.Translate(new Vector3(Input.GetAxis("Horizontal") , 0, Input.GetAxis("Vertical")) * Time.deltaTime * moveSpd);
		}
	}
}
