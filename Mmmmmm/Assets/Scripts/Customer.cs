using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Customer : MonoBehaviour {

	GameManager gm;
	Vector3 targetPoint;

	NavMeshAgent agent;

	Transform target;
	Vector3 _direction;
	Quaternion _lookRotation;

	bool changePos;
	bool canmove;
	bool initialpath;

	public bool doingthings;
	bool metacat;
	bool metacustomer;
	SphereCollider interactionrange;

	float changePosTime;
	bool recentlyMet;

	float initializationTime;
	float timeSinceInitialization;
	float durationOfStay = 30f;
	float durationExtension = 5f;

	bool customerExiting;


	// Use this for initialization
	void Start () {
		initializationTime = Time.timeSinceLevelLoad;


		agent = GetComponent<NavMeshAgent> ();

		interactionrange = GetComponent<SphereCollider> ();
		agent.enabled = false;

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
		timeSinceInitialization = Time.timeSinceLevelLoad - initializationTime;

		if (customerExiting && atDestination(gm.customerdestinations[3].position)) {
			agent.enabled = false;
			targetPoint = gm.customerdestinations [2].position;
			iTween.MoveTo (this.gameObject, iTween.Hash (
				"position", targetPoint,
				"time", 3f,
				"oncomplete", "destroyCustomer",
				"oncompletetarget", transform.gameObject
			));
		}

		if (timeSinceInitialization > durationOfStay && canmove && !customerExiting) {
			interactionrange.enabled = false;
			if (agent.enabled) {
				targetPoint = gm.customerdestinations [3].position;
				agent.SetDestination (targetPoint);
				customerExiting = true;
			}

		} else {


			if (initialpath && atDestination (gm.customerdestinations [5].position)) {
				initialpath = false;
			}

			if (!initialpath && !changePos && canmove) {
				if (recentlyMet) {
					changePosTime = 0f;
				} else {
					changePosTime = Random.Range (1f, 10f);
				}
				Invoke ("ChangePosition", changePosTime);
				recentlyMet = false;
				changePos = true;
			}

			if (doingthings && target != null) {
				faceEachOther ();
				if (metacat) {
					StartCoroutine (StartDoingThingsCat ());
				}
			}
		}

	}

	void destroyCustomer(){
		Destroy (gameObject);
	}

	bool atDestination(Vector3 pos){
		return pos.x - 0.1f < transform.position.x && transform.position.x < pos.x + 0.1f;
	}

	void initiateCustomerRoam(){
		agent.enabled = true;

		//obstacle.enabled = false;
		canmove = true;
		initialpath = true;
		agent.SetDestination (gm.customerdestinations [5].position);
	}

	void ChangePosition(){

		if (agent.enabled) {
			targetPoint = gm.customerdestinations [Random.Range (6, gm.customerdestinations.Count)].position;
			agent.SetDestination (targetPoint);
		}
		changePos = false;
	}

	void OnTriggerEnter(Collider other){
		if (!doingthings && other.gameObject.tag=="Cat") {
			doingthings = true;
			metacat = true;
			canmove = false;

		//	agent.avoidancePriority = 99;
			agent.Stop ();

		//	agent.enabled = false;
		//	obstacle.enabled = true;

			interactionrange.enabled = false;

			target = other.transform;

			durationOfStay += durationExtension;
		} 

		/*else if (!doingthings && other.gameObject.tag=="Customer") {
			doingthings = true;
			metacustomer = true;
			canmove = false;
			agent.enabled = false;
			obstacle.enabled = true;
			interactionrange.enabled = false;

			target = other.transform;
		}
*/
	}

	void faceEachOther(){
		//find the vector pointing from our position to the target
		_direction = new Vector3 (target.position.x - transform.position.x,
			0f, target.position.z - transform.position.z);
		//_direction = _direction.normalized;

		//create the rotation we need to be in to look at the target
		_lookRotation = Quaternion.LookRotation (_direction);

		//rotate this over time according to speed until in the required rotation
		transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation,
			Time.deltaTime * 2f);

		/*_direction = new Vector3 (target.position.x, transform.position.y, target.position.z);
		transform.LookAt (_direction);*/

	}

	IEnumerator StartDoingThingsCat(){
		yield return new WaitForSeconds (5f);
		//ChangePosition ();
		canmove = true;



		recentlyMet = true;
		//agent.avoidancePriority = Random.Range(0,30);
	//	obstacle.enabled = false;
	//	agent.enabled = true;
		agent.Resume ();

		target = null;

		yield return new WaitForSeconds (10f);
		metacat = false;
		doingthings = false;
		interactionrange.enabled = true;

	}
}
