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

	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		obstacle = GetComponent<NavMeshObstacle> ();
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
		if (!doingthings && other.gameObject.tag == "Cat" && !other.gameObject.GetComponent<Cat>().doingthings) {
			print ("meow");
			doingthings = true;
			metacat = true;
			canmove = false;
			agent.enabled = false;
			obstacle.enabled = true;
		} else if (!doingthings && other.gameObject.tag == "Customer" && !other.gameObject.GetComponent<Customer>().doingthings) {
			print ("hellohuman");
			doingthings = true;
			canmove = false;
			agent.enabled = false;
			obstacle.enabled = true;
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
}
