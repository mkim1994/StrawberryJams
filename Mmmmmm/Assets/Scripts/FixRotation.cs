using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {

	Quaternion rotation;

	Vector3 originalRotation;

	//GameObject personalCat;

	// Use this for initialization
	void Start () {

		originalRotation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {

		transform.eulerAngles = new Vector3(0 , originalRotation.y, 0);

	}
}
