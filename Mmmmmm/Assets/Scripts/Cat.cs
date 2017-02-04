using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {

	Vector3 targetPoint;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("ChangePosition", 0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<NavMeshAgent> ().SetDestination (targetPoint);
	}

	void ChangePosition(){
		targetPoint = new Vector3 (Random.Range (-8f, 8f), 0,
			Random.Range (-4.5f, 4.5f));
	}
}
