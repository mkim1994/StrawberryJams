﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour {

	private FSM<Customer> _fsm; //apply states to Cat

	private NavMeshAgent agent;
	private NavMeshObstacle obstacle;

	public GameObject pathfinding;
	public GameObject model;

	public bool metCat;
	public bool canDoThings;

	public bool interacting;
	public int otherIndex;
	public int index;

	private int targetDest;

	private Animator anim;
	private bool isWalking, isIdle, isInteracting;

	public Transform interactionTarget;
	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Awake () {
		pathfinding = transform.GetChild (0).gameObject;
		model = transform.GetChild (1).gameObject;
		agent = pathfinding.GetComponent<NavMeshAgent> ();
		obstacle = pathfinding.GetComponent<NavMeshObstacle> ();
		obstacle.enabled = false;

		anim = model.GetComponent<Animator> ();

		canDoThings = true;

		_fsm = new FSM<Customer> (this);
		_fsm.TransitionTo<EnteringCafe> ();

		/*targetDest = FindNearestCat ().transform.position;
		SetDestination (currDest);*/
	}

	/*
	
	// Update is called once per frame
	void Update () {


		if (agent.enabled && !agent.pathPending) {
			if (agent.remainingDistance <= agent.stoppingDistance) {
				if (agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
					ChangeDestination ();
				}
			}
		}

		CheckAnimationStates ();

		if (!canDoThings && interactionTarget != null) {
			faceEachOther ();

		}

		model.transform.position = Vector3.Lerp (model.transform.position, pathfinding.transform.position, Time.deltaTime * 2);
		model.transform.rotation = pathfinding.transform.rotation;

	}

	void faceEachOther(){
		//find the vector pointing from our position to the target
		_direction = new Vector3 (interactionTarget.position.x - pathfinding.transform.position.x,
			0f, interactionTarget.position.z - pathfinding.transform.position.z);
		//_direction = _direction.normalized;

		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation (_direction);

		//rotate this over time according to speed until in the required rotation
		pathfinding.transform.rotation = Quaternion.Slerp (pathfinding.transform.rotation, _lookRotation,
			Time.deltaTime * 2f);

	}

	void CheckAnimationStates(){
		if (agent.enabled && agent.velocity!=Vector3.zero && !isWalking && canDoThings) {
			anim.SetTrigger ("Walk");
			isWalking = true;
			isIdle = false;
			isInteracting = false;
		} else if (obstacle.enabled && !canDoThings && !isInteracting) {
			anim.SetTrigger ("Interact");
			isWalking = false;
			isIdle = false;
			isInteracting = true;
		} else if (!isIdle && agent.velocity == Vector3.zero && canDoThings){
			anim.SetTrigger ("Idle");
			isWalking = false;
			isIdle = true;
			isInteracting = false;
		}

	}

	public void ChangeDestination(){
		AgentManager.instance.GetTargetDestinations () [targetDest].taken = false;
		targetDest = Random.Range (0, AgentManager.instance.GetTargetDestinations ().Length);
		//Debug.Log ("cat #: " + index + ", "+ "targetDest: "+targetDest);
		while (AgentManager.instance.GetTargetDestinations () [targetDest].taken) {
			//Debug.Log ("while loop - cat #: " + index);
			targetDest = Random.Range (0, AgentManager.instance.GetTargetDestinations ().Length);
		}
		AgentManager.instance.GetTargetDestinations () [targetDest].taken = true;
		SetDestination (AgentManager.instance.GetTargetDestinations()[targetDest].position);

	}

	public void SetDestination(Vector3 position){
		agent.SetDestination (position);

	}

	GameObject FindNearestCat(){
		List<GameObject> cats = AgentManager.instance.GetCats ();
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject cat in cats) {
			Vector3 diff = cat.transform.position - position;
			float curDistance = diff.sqrMagnitude;
			if(curDistance < distance){
				closest = cat;
				distance = curDistance;
			}
		}
		return closest;
	}

	public void EnableObstacle(){
		agent.enabled = false;
		obstacle.enabled = true;
	}

	public void DisableObstacle(){
		canDoThings = true;
		obstacle.enabled = false;
		agent.enabled = true;
	}
	*/

	private class CustomerState : FSM<Customer>.State{}

	private class EnteringCafe : CustomerState{

	}

	private class Idling : CustomerState{ //totally uninterested

	}

	private class Walking : CustomerState {

	}

	private class Sitting : CustomerState {

	}

	private class InteractWithCat : CustomerState {

	}

	private class ExitingCafe : CustomerState {

	}


}
