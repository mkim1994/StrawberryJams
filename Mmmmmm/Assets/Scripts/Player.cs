using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {



	GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void giveFood(GameObject cat){

	}

	void giveToy(GameObject cat){

	}
}
