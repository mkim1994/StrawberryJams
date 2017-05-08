using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cat : MonoBehaviour {

	private FSM<Cat> _fsm; //apply states to Cat

	private NavMeshAgent agent;
	private NavMeshObstacle obstacle;

	public GameObject pathfinding;
	public GameObject model;


	public int uniqueID;

	public bool metCat;
	public bool metCustomer;
	public bool canDoThings;
	public bool restingFromInteraction;

	public int targetDest;

	private Animator anim;

	public Transform interactionTarget;
	private Quaternion _lookRotation;
	private Vector3 _direction;

	public bool idling;
	public bool walking;
	public bool interacting;

	public int horniness;
	public int happiness;
	public int usingToy;
	public int ateFood;



	void Awake(){
		pathfinding = transform.GetChild (0).gameObject;
		model = transform.GetChild (1).gameObject;


		agent = pathfinding.GetComponent<NavMeshAgent> ();
		obstacle = pathfinding.gameObject.GetComponent<NavMeshObstacle> ();
		obstacle.enabled = false;

		anim = model.GetComponent<Animator> ();
		canDoThings = true;

		_fsm = new FSM<Cat> (this);
		_fsm.TransitionTo<Idling> ();
		idling = true;

		restingFromInteraction = false;
	}
	void Start(){
		
	}

	void Update(){
		_fsm.Update ();
	}

	public Vector3 NewDestination(){
		int targetDest = Random.Range (0, AgentManager.instance.GetTargetDestinations ().Length);
		return AgentManager.instance.GetTargetDestinations () [targetDest].position;
	}

	public void MoveInitial(){
		StartCoroutine(MoveAtRandomIntervals());
	}

	public void MoveModel(){
		model.transform.position = Vector3.Lerp (model.transform.position, pathfinding.transform.position, Time.deltaTime * 2);
		model.transform.rotation = pathfinding.transform.rotation;
	}

	IEnumerator MoveAtRandomIntervals(){
		while(walking){ //if you reach the destination then change to idle a bit
			agent.SetDestination (NewDestination ());
			yield return new WaitForSeconds (Random.Range (3f, 10f));
		}
	}

	public void KittyBorn(){
		//StartCoroutine (SizeUp ());
		//size up
	}

		
	public void InitiateIdle(){
		EnableAgent ();
	}

	public void Wait(float seconds){
		StartCoroutine (WaitGivenSeconds(seconds));
	}

	IEnumerator WaitGivenSeconds(float seconds){
		yield return new WaitForSeconds (seconds);
		idling = false;
	}

	public void SetIdleAnimation(){
		anim.SetBool ("Idle", true);
		anim.SetBool ("Walk", false);
		anim.SetBool ("Interact", false);
		anim.SetBool ("Eating", false);
	}

	public void SetWalkAnimation(){
		anim.SetBool ("Idle", false);
		anim.SetBool ("Walk", true);
		anim.SetBool ("Interact", false);
		anim.SetBool ("Eating", false);
	}

	public void SetInteractAnimation(){
		anim.SetBool ("Idle", false);
		anim.SetBool ("Walk", false);
		anim.SetBool ("Interact", true);
		anim.SetBool ("Eating", false);
	}

	public void SetEatingAnimation(){
		anim.SetBool ("Idle", false);
		anim.SetBool ("Walk", false);
		anim.SetBool ("Interact", false);
		anim.SetBool ("Eating", true);
	}

	public void DisableAgent(){
		agent.isStopped = true;
		agent.enabled = false;
		//obstacle.enabled = true;
		pathfinding.GetComponent<BoxCollider> ().enabled = false;
	}

	public void EnableAgent(){
		//obstacle.enabled = false;
		agent.enabled = true;
		agent.isStopped = false;
		pathfinding.GetComponent<BoxCollider> ().enabled = true;
	}


	public void InitiateInteraction(){

		DisableAgent ();
		AgentManager.instance.CatsHavingSex.Add (uniqueID);
	}

	public void InteractWithCat(){
		FaceEachOther ();
	}

	public void SexyTimes(){
		StartCoroutine (Explicit ());
	}
	IEnumerator Explicit(){
		Vector3 explicitPos = Vector3.Lerp (pathfinding.transform.position, 
			interactionTarget.position, 0.5f);
		GameObject instance = Instantiate(Resources.Load("Prefab/ExplicitSign")) as GameObject;
		instance.transform.position = explicitPos;
		yield return new WaitForSeconds (3f);
		Destroy (instance);
		interacting = false;
		canDoThings = true;
		EventManager.TriggerEvent ("PopOutKitty");
	}

	void FaceEachOther(){
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

	void ResetInteractionWindow(){
		StartCoroutine (Resting ());
	}
	IEnumerator Resting(){
		yield return new WaitForSeconds (5f);
		restingFromInteraction = false;
	}

	private class CatState : FSM<Cat>.State{}

	private class Idling : CatState{ //totally uninterested

		private float seconds;
		public override void OnEnter(){
			Context.idling = true;
			seconds = Random.Range (0.5f, 5f);
			Context.InitiateIdle ();
			Context.Wait(seconds);
			Context.SetIdleAnimation ();
		}
		public override void Update(){
			if (!Context.idling) {
				TransitionTo<Walking> ();
				return;
			}
		}
	}

	private class Walking : CatState{

		public override void OnEnter(){
			Context.walking = true;
			Context.SetWalkAnimation ();
			Context.MoveInitial();
		}
		public override void Update(){

			if (Context.metCat) {
				Context.walking = false;
				TransitionTo<InteractingWithCat> ();
				return;
			} else {
				Context.MoveModel ();

			}
		}

	}

	private class InteractingWithCat : CatState{

		public override void OnEnter(){
			Context.metCat = false;
			Context.interacting = true;
			Context.SetInteractAnimation ();

			Context.InitiateInteraction ();

			Context.SexyTimes ();
		

		}

		public override void Update(){
			if (!Context.canDoThings) {
				Context.MoveModel ();
				Context.InteractWithCat ();
			} else {
				Context.ResetInteractionWindow ();
				TransitionTo<Idling> ();
				return;
			}

		}

	}
	private class InteractingWithCustomer : CatState{

	}

	private class Eating : CatState{ //uninterested
		public override void Update(){

		}
	}
		




}
