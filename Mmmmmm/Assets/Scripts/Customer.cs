using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour {

	GameManager gm;
	Vector3 targetPoint;

	NavMeshAgent agent;
	BoxCollider boxcollider;

	private InteractionRange interactionrange;

	bool changePos;
	bool canmove;
	bool initialpath;

	bool doingthings;
	bool metacat;

	// Use this for initialization
	void Start () {
		agent = transform.GetComponent<NavMeshAgent> ();
		boxcollider = transform.GetComponent<BoxCollider> ();
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		interactionrange = transform.GetChild (0).gameObject.GetComponent<InteractionRange> ();
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
		//boxcollider.enabled = true;
		canmove = true;
		initialpath = true;
		agent.SetDestination (gm.customerdestinations [3].position);
	}

	void ChangePosition(){
		targetPoint = gm.customerdestinations [Random.Range (4, gm.customerdestinations.Count)].position;
		agent.SetDestination (targetPoint);

		changePos = false;
	}

	void DetectInteractionRange(){
		if (!doingthings && interactionrange.catsmet) {
			print ("meow");
			doingthings = true;
			metacat = true;
			canmove = false;
		}

		/*if (!doingthings && interactionrange.customermet) {
			print ("hellohuman");
			doingthings = true;
			canmove = false;
		}*/
	}
}
