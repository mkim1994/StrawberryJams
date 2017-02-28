using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRotation : MonoBehaviour {

	Quaternion rotation;

	Vector3 originalRotation;

	//GameObject personalCat;

	// Use this for initialization
	void Start () {
		//personalCat = GameObject.Find ("Cat" + transform.GetSiblingIndex ());
		//rotation = transform.rotation;
		//transform.position = new Vector3 (personalCat.transform.position.x - 8f, transform.position.y, personalCat.transform.position.z + 5.5f);

		originalRotation = transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate (0, transform.rotation.y-transform.parent.rotation.y, 0);

		//Debug.Log (originalRotation.y + ", " + transform.parent.eulerAngles.y);

		transform.eulerAngles = new Vector3(0 , originalRotation.y, 0);


		//transform.position = new Vector3 (personalCat.transform.position.x - 8f, transform.position.y, personalCat.transform.position.z + 5.5f);
	}
}
