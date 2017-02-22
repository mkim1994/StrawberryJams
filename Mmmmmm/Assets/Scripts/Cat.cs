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

	private InteractionRange interactionrange;

	private bool canmove;
	private bool doingthings;
	private bool metacat;
	private bool metacustomer;

	void Start () {
		canmove = true;
		changePos = false;
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		interactionrange = transform.GetChild (0).gameObject.GetComponent<InteractionRange> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!changePos && canmove){
			Invoke("ChangePosition",Random.Range(1f,20f));
			changePos = true;
		}
		DetectInteractionRange ();

		if (doingthings) {
			if (metacat) {
				StartCoroutine (StartDoingThingsCat());
			} else if (metacustomer) {
				StartCoroutine (StartDoingThingsCustomer());
			}
		}
		
	}

	void ChangePosition(){
		targetPoint = gm.catdestinations [Random.Range (0, gm.catdestinations.Count)].position;
		GetComponent<NavMeshAgent> ().SetDestination (targetPoint);

		changePos = false;
	}

	void eatFood(Food food){
		
	}

	void playWithToy(Toy toy){

	}

	void DetectInteractionRange(){
		if (!doingthings && interactionrange.catsmet) {
			print ("meow");
			doingthings = true;
			metacat = true;
			canmove = false;
		}

		if (!doingthings && interactionrange.customermet) {
			print ("hellohuman");
			doingthings = true;
			canmove = false;
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
