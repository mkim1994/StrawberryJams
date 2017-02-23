using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour {

	GameManager gm;
	Vector3 targetPoint;

	NavMeshAgent agent;
	NavMeshObstacle obstacle;
	//BoxCollider boxcollider;


	bool changePos;
	bool canmove;
	bool initialpath;

	public bool doingthings;
	bool metacat;
	bool metacustomer;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		obstacle = GetComponent<NavMeshObstacle> ();
		agent.enabled = false;
		obstacle.enabled = false;

		//boxcollider = transform.GetComponent<BoxCollider> ();
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		targetPoint = gm.customerdestinations [1].position;
		iTween.MoveTo (this.gameObject, iTween.Hash (
			"position", targetPoint,
			"time", 2f,
			"oncomplete", "initiateCustomerRoam",
			"oncompletetarget", transform.gameObject
		));
	}
	
	// Update is called once per frame
	void Update () {
		if (initialpath && atDestination(gm.customerdestinations[3].position)) {
			initialpath = false;
		}

		if(!initialpath && !changePos && canmove){
			Invoke("ChangePosition",Random.Range(1f,10f));
			changePos = true;
		}

	}

	bool atDestination(Vector3 pos){
		return pos.x - 0.1f < transform.position.x && transform.position.x < pos.x + 0.1f;
	}

	void initiateCustomerRoam(){
		agent.enabled = true;
		obstacle.enabled = false;
		canmove = true;
		initialpath = true;
		agent.SetDestination (gm.customerdestinations [3].position);
	}

	void ChangePosition(){

		if (agent.enabled) {
			targetPoint = gm.customerdestinations [Random.Range (4, gm.customerdestinations.Count)].position;
			agent.SetDestination (targetPoint);
		}
		changePos = false;
	}

	void OnTriggerEnter(Collider other){
		if (!doingthings && other.gameObject.tag=="Cat" && !other.gameObject.GetComponent<Cat>().doingthings) {
			doingthings = true;
			metacat = true;
			canmove = false;
			agent.enabled = false;
			obstacle.enabled = true;
		} else if (!doingthings && other.gameObject.tag=="Customer" && !other.gameObject.GetComponent<Customer>().doingthings) {
			doingthings = true;
			metacustomer = true;
			canmove = false;
			agent.enabled = false;
			obstacle.enabled = true;
		}
	}
}
