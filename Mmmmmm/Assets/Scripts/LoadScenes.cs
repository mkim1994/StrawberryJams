﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toMainGame() {
		Application.LoadLevel("Main");
	}
}
