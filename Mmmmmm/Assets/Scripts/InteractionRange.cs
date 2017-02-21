using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionRange : MonoBehaviour {

	public bool catsmet;
	public bool customermet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "CatRange") {
			catsmet = true;
		}

		if (other.gameObject.tag == "CustomerRange") {
			customermet = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "CatRange") {
			catsmet = false;
		}
		if (other.gameObject.tag == "CustomerRange") {
			customermet = false;
		}

	}
}
