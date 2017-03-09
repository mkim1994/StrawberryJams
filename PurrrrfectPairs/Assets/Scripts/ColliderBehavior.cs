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

	void OnTriggerEnter(Collider other){ //for cats
		if (other.gameObject.transform.parent.tag == "Cat") {
			if (AgentManager.instance.activeCatIndices.Count < 2) {
			
				AgentManager.instance.activeCatIndices.Add (GetComponentInParent<Cat> ().index);
				GetComponentInParent<Cat> ().canDoThings = false;
				GetComponentInParent<Cat> ().metCat = true;
				other.gameObject.GetComponentInParent<Cat> ().canDoThings = false;
				GetComponent<BoxCollider> ().enabled = false;

				GetComponentInParent<Cat> ().interactionTarget = other.gameObject.GetComponentInParent<Cat> ().pathfinding.transform;


			
			}
			 /*else if (transform.parent.tag == "Customer") {
				if (!other.gameObject.GetComponentInParent<Cat> ().canDoThings) {
					GetComponentInParent<Customer> ().ChangeDestination ();
				}
			}*/

		} else if (other.gameObject.transform.parent.tag == "Customer") {
			if (AgentManager.instance.activeHappyIndices.Count < 2 && GetComponentInParent<Cat> ().canDoThings && other.gameObject.GetComponentInParent<Customer> ().canDoThings) {
				AgentManager.instance.activeHappyIndices.Add (GetComponentInParent<Cat> ().index);
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
