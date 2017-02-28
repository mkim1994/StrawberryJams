using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {


	GameManager gm;
	Vector3 targetPoint;

	bool changePos;

	//uniqueID starts from 1. 0 is reserved for theOtherCatID when it's "null"
	public int uniqueID;
	public int theOtherCatID;

	public string thename;
	public int happiness;
	public int horniness;

	public int breedingchance;



	private bool canmove;
	public bool doingthings;
	private bool metacat;
	private bool metacustomer;

	NavMeshAgent agent;
	SphereCollider interactionrange;

	Transform target;
	Quaternion _lookRotation;
	Vector3 _direction;

	bool recentlyMet;
	float changePosTime;

	public bool sexytimes;

	public float sexytimesDuration = 5f; //triggers: Walking, Eating, Idle

	Animator anim;
	bool isWalking;
	bool isEating;
	bool isIdle;

	void Start () {

		anim = transform.GetChild (0).gameObject.GetComponent<Animator> ();

		agent = GetComponent<NavMeshAgent> ();
		//agent.avoidancePriority = Random.Range (60, 100);

		interactionrange = GetComponent<SphereCollider> ();
		canmove = true;
		changePos = false;
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (agent.velocity != Vector3.zero && !metacustomer && canmove && !isWalking) {
			anim.SetTrigger ("Walking");

			isWalking = true;
			isIdle = false;
			isEating = false;


		} else if (agent.velocity == Vector3.zero && !metacustomer && !isIdle) {
			anim.SetTrigger ("Idle");
			int animchance = Random.Range (0,3);
			if (animchance == 0) {
				anim.SetTrigger ("Idle1");
			} else if (animchance == 1) {
				anim.SetTrigger ("Idle2");
			} else {
				anim.SetTrigger ("Idle3");
			}
			isWalking = false;
			isIdle = true;
			isEating = false;
		} else {
			if (!isEating && metacustomer) {

				anim.SetTrigger ("Eating");

				isWalking = false;
				isIdle = false;
				isEating = true;
			}
		}
		
		if(!changePos && canmove){
			if (recentlyMet) {
				changePosTime = 0f;
			} else {
				changePosTime = Random.Range (3f, 8f);
			}
			Invoke("ChangePosition", changePosTime);
			recentlyMet = false;
			changePos = true;
		}

		if (doingthings && target != null) {
			faceEachOther ();
			if (metacat) {
				StartCoroutine (StartDoingThingsCat());
			} else if (metacustomer) {
				StartCoroutine (StartDoingThingsCustomer());
			}
		}

	}

	void ChangePosition(){
		if (agent.enabled) {
			targetPoint = gm.catdestinations [Random.Range (0, gm.catdestinations.Count)].position;
			agent.SetDestination (targetPoint);
		}

		changePos = false;
	}

	void eatFood(Food food){
		
	}

	void playWithToy(Toy toy){

	}

	void OnTriggerEnter(Collider other){
		if (!doingthings && other.gameObject.tag == "Cat") {

			theOtherCatID = other.GetComponent<Cat> ().uniqueID;
			doingthings = true;
			metacat = true;
			canmove = false;

		//	agent.avoidancePriority = 99;
			agent.Stop ();

			//agent.enabled = false;
			//obstacle.enabled = true;

			//obstacle.enabled = true;
			interactionrange.enabled = false;

			target = other.transform;
		} else if (!doingthings && other.gameObject.tag == "Customer") {
			doingthings = true;
			metacustomer = true;
			canmove = false;

		//	agent.avoidancePriority = 99;
			agent.Stop ();
		//	agent.enabled = false;
		//	obstacle.enabled = true;

			interactionrange.enabled = false;

			target = other.transform;

		}
	}

	IEnumerator StartDoingThingsCat(){

		sexytimes = true;

	//	GameObject.FindGameObjectWithTag("Censoreds").transform.GetChild(

		transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
		//transform.GetChild (0).gameObject.SetActive (false);

		yield return new WaitForSeconds (sexytimesDuration);

		theOtherCatID = 0;

		sexytimes = false;
		//transform.GetChild (0).gameObject.SetActive (true);
		transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = false;

		canmove = true;
	//	obstacle.enabled = false;
		recentlyMet = true;



		//obstacle.enabled = false;
	//	agent.enabled = true;
		//agent.avoidancePriority = Random.Range(0,30);
		agent.Resume ();



		target = null;

		yield return new WaitForSeconds (10f);
		metacat = false;
		doingthings = false;
		interactionrange.enabled = true;
	}
	IEnumerator StartDoingThingsCustomer(){
		yield return new WaitForSeconds (5f);
		canmove = true;
	//	obstacle.enabled = false;
		recentlyMet = true;

		//obstacle.enabled = false;
		//agent.enabled = true;
	//	agent.avoidancePriority = Random.Range(0,30);
		agent.Resume ();
		target = null;

		yield return new WaitForSeconds (10f);
		metacustomer = false;
		doingthings = false;
		interactionrange.enabled = true;
	}

/*	void pauseNav(){

	}

	void resumeNav(){

	}*/

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

	}
}
