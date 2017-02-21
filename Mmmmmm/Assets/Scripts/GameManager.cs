using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour { //+hacky EventManager

	[HideInInspector]
	public List<Transform> catdestinations;
	public Transform catdestinationgroup;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void fillCatDestinations(){
		foreach (Transform child in catdestinationgroup) {
			catdestinations.Add (child);
		}
	}

}
