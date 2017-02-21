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

	void Start () {
		changePos = false;
		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
		interactionrange = transform.GetChild (0).gameObject.GetComponent<InteractionRange> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!changePos){
			Invoke("ChangePosition",Random.Range(1f,20f));
			changePos = true;
		}

		DetectInteractionRange ();
		
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
		if (interactionrange.catsmet) {
			print ("meow");
		}

		if (interactionrange.customermet) {
			print ("hellohuman");
		}
	}


}
