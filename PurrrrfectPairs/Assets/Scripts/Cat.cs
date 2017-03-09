using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {

	private NavMeshAgent agent;
	private NavMeshObstacle obstacle;

	public GameObject pathfinding;
	public GameObject model;


	public bool metCat;
	public bool metCustomer;
	public bool canDoThings;

	public int otherIndex;
	public int index;

	public int targetDest;

	private Animator anim;
	private bool isIdle, isWalking, isInteracting;

	public Transform interactionTarget;
	private Quaternion _lookRotation;
	private Vector3 _direction;

	// Use this for initialization
	void Awake () {
		pathfinding = transform.GetChild (0).gameObject;
		model = transform.GetChild (1).gameObject;


		agent = pathfinding.GetComponent<NavMeshAgent> ();
		obstacle = pathfinding.gameObject.GetComponent<NavMeshObstacle> ();
		obstacle.enabled = false;

		anim = model.GetComponent<Animator> ();
		canDoThings = true;

	}
	
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

		model.transform.position = Vector3.Lerp (model.transform.position, pathfinding.transform.position, Time.deltaTime * 2);
		model.transform.rotation = pathfinding.transform.rotation;
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

		if (!canDoThings && interactionTarget != null) {
			faceEachOther ();

		}

	}

	public void SetDestination(Vector3 position){
		agent.SetDestination (position);

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
		canDoThings = false;
		agent.enabled = false;
		obstacle.enabled = true;
	}

	public void DisableObstacle(){
		canDoThings = true;
		obstacle.enabled = false;
		agent.enabled = true;
	}
}
