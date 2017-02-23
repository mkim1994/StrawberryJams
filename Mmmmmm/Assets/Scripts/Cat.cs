using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {

	GameManager gm;
	Vector3 targetPoint;

	bool changePos;

	public string thename;
	public int happiness;
	public int horniness;

	public int breedingchance;


	private bool canmove;
	public bool doingthings;
	private bool metacat;
	private bool metacustomer;

	NavMeshAgent agent;
	NavMeshObstacle obstacle;
	SphereCollider interactionrange;

	Transform target;
	Quaternion _lookRotation;
	Vector3 _direction;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		obstacle = GetComponent<NavMeshObstacle> ();
		interactionrange = GetComponent<SphereCollider> ();
		obstacle.enabled = false;
		canmove = true;
		changePos = false;
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!changePos && canmove){
			Invoke("ChangePosition",Random.Range(1f,20f));
			changePos = true;
		}

		if (doingthings) {
			faceEachOther ();
		}

	/*	if (doingthings) {
			if (metacat) {
				StartCoroutine (StartDoingThingsCat());
			} else if (metacustomer) {
				StartCoroutine (StartDoingThingsCustomer());
			}
		}*/
		
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
			print ("meow");
			doingthings = true;
			metacat = true;
			canmove = false;
			agent.enabled = false;
			obstacle.enabled = true;
			interactionrange.enabled = false;

			target = other.transform;
		} else if (!doingthings && other.gameObject.tag == "Customer") {
			print ("hellohuman");
			doingthings = true;
			canmove = false;
			agent.enabled = false;
			obstacle.enabled = true;

			interactionrange.enabled = false;

			target = other.transform;

		}
	}

	IEnumerator StartDoingThingsCat(){
		yield return new WaitForSeconds (5f);
		canmove = true;
		yield return new WaitForSeconds (3f);
		metacat = false;
		doingthings = false;
	}
	IEnumerator StartDoingThingsCustomer(){
		yield return new WaitForSeconds (10f);
		canmove = true;
		yield return new WaitForSeconds (3f);
		metacustomer = false;
		doingthings = false;
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
}
