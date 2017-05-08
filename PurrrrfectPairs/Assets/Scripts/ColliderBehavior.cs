using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBehavior : MonoBehaviour {



	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//change this so it just uses state machine's state (currentstate)

	void OnTriggerEnter(Collider other){ //for cats
		if (other.gameObject.transform.parent.tag == "Cat") {
			
			Cat thiscat = GetComponentInParent<Cat>();
			Cat othercat = other.gameObject.GetComponentInParent<Cat> ();

			if(!thiscat.restingFromInteraction &&  !othercat.restingFromInteraction){
				thiscat.interactionTarget = othercat.pathfinding.transform;
				othercat.interactionTarget = thiscat.pathfinding.transform;

				thiscat.canDoThings = false;
				othercat.canDoThings = false;

				thiscat.restingFromInteraction = true;
				othercat.restingFromInteraction = true;

				thiscat.metCat = true;
				othercat.metCat = true;
			}


		} else if (other.gameObject.transform.parent.tag == "Customer") {
			if (AgentManager.instance.activeHappyIndices.Count < 2 && GetComponentInParent<Cat> ().canDoThings && other.gameObject.GetComponentInParent<Customer> ().canDoThings) {
				AgentManager.instance.activeHappyIndices.Add (GetComponentInParent<Cat> ().uniqueID);
				AgentManager.instance.activeHappyIndices.Add (other.gameObject.GetComponentInParent<Customer> ().index);
				GetComponent<BoxCollider> ().enabled = false;
				other.gameObject.GetComponent<BoxCollider> ().enabled = false;
				GetComponentInParent<Cat> ().canDoThings = false;
				GetComponentInParent<Cat> ().interactionTarget = other.gameObject.GetComponentInParent<Customer> ().pathfinding.transform;

				other.gameObject.GetComponentInParent<Customer> ().canDoThings = false;
				other.gameObject.GetComponentInParent<Customer> ().interactionTarget = GetComponentInParent<Cat> ().pathfinding.transform;
				GetComponentInParent<Cat> ().metCustomer = true;
			}
			 /*else if (transform.parent.tag == "Customer") {
				GetComponentInParent<Customer> ().ChangeDestination ();

			}*/

		}
	}
}
