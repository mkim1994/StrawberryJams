using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {

	public Transform[] catdestinations;
	Vector3 targetPoint;

	bool changePos;

	// Use this for initialization
	void Start () {
		//InvokeRepeating ("ChangePosition", 0f, 10f);
		changePos = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!changePos){
			Invoke("ChangePosition",Random.Range(5f,10f));
			changePos = true;
		}
		
	}

	void ChangePosition(){
		targetPoint = catdestinations [Random.Range (0, catdestinations.Length)].position;
		GetComponent<NavMeshAgent> ().SetDestination (targetPoint);

		changePos = false;
	}
}
