using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {

	GameManager gm;
	Vector3 targetPoint;

	bool changePos;

	void Start () {
		changePos = false;
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!changePos){
			Invoke("ChangePosition",Random.Range(5f,10f));
			changePos = true;
		}
		
	}

	void ChangePosition(){
		targetPoint = gm.catdestinations [Random.Range (0, gm.catdestinations.Length)].position;
		GetComponent<NavMeshAgent> ().SetDestination (targetPoint);

		changePos = false;
	}
}
