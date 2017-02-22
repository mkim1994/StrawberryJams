using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour { //+hacky EventManager

	[HideInInspector]
	public List<Transform> catdestinations;
	public Transform catdestinationgroup;

	[HideInInspector]
	public List<Transform> customerdestinations;
	public Transform customerdestinationgroup;


	// Use this for initialization
	void Awake () {
		fillDestinations ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	void fillDestinations(){
		foreach (Transform child in catdestinationgroup) {
			catdestinations.Add (child);
		}
		foreach (Transform child in customerdestinationgroup) {
			customerdestinations.Add (child); //0 index is always entrance/exit
		}
	}

}
