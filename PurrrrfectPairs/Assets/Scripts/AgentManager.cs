using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour {

	private TargetDestination[] targetDestinations;

	private List<GameObject> cats;
	private List<GameObject> customers;
	public List<int> activeCatIndices;
	public List<int> activeHappyIndices;

	private static AgentManager agentManager;

	//public static AgentManager stance;
	public Transform kittySpawnPoint;

	public List<int> CatsHavingSex;

	public static AgentManager instance{
		get{
			if (!agentManager) {
				agentManager = FindObjectOfType (typeof(AgentManager)) as AgentManager;
				if (!agentManager) {
					Debug.LogError ("no active agentmanager");
				} else {
					agentManager.Init ();
					return agentManager;
				}
			}
			return agentManager;
		}

	}

	void Init(){
		
		cats = new List<GameObject> ();
		customers = new List<GameObject> ();
		GameObject[] catObjs = GameObject.FindGameObjectsWithTag ("Cat");
		GameObject[] customerObjs = GameObject.FindGameObjectsWithTag ("Customer");

		int count = 0;
		foreach (GameObject c in catObjs) {
			c.GetComponent<Cat> ().uniqueID = count;
			cats.Add (c);
			count++;
		}
		count = 0;
		foreach (GameObject c2 in customerObjs) {
			c2.GetComponent<Customer> ().index = count;
			customers.Add (c2);
			count++;
		}

		Transform targetDest = GameObject.FindWithTag ("TargetDestinations").transform;
		targetDestinations = new TargetDestination[targetDest.childCount];
		int childcount = 0;
		foreach (Transform child in targetDest) {
			targetDestinations [childcount] = new TargetDestination ();
			targetDestinations[childcount].position = child.position;
			targetDestinations[childcount].taken = false;
			childcount++;
		}

		activeCatIndices = new List<int> ();
		activeHappyIndices = new List<int> ();


	}

	public TargetDestination[] GetTargetDestinations(){
		return targetDestinations;
	}

	public List<GameObject> GetCats(){
		return cats;
	}

	public List<GameObject> GetCustomers(){
		return customers;
	}


	// Use this for initialization
	void Start () {
		EventManager.StartListening ("PopOutKitty", PopOutKitty);

		//assign destinations to the pre-existing cats
	/*	int destCount = 0;
		foreach (GameObject cat in instance.cats) {
			cat.GetComponent<Cat> ().SetDestination (targetDestinations [destCount].position);
			destCount++;
		}
		foreach (GameObject customer in instance.customers) {
			customer.GetComponent<Customer> ().SetDestination (targetDestinations [destCount].position);
			destCount++;
		}*/
	}


	
	// Update is called once per frame
	void FixedUpdate () {
		//CatInteractWithCat ();
	//	CatInteractWithCustomer ();
	}


	//event
	public void PopOutKitty(){
		if (CatsHavingSex.Count > 1) {
			CatsHavingSex.RemoveAt (0);
			CatsHavingSex.RemoveAt (0);
			GameObject instance = Instantiate(Resources.Load("Prefab/Cat")) as GameObject;
			instance.transform.position = kittySpawnPoint.position;
		}

	}

	/*
	public void CatInteractWithCat(){

		if (activeCatIndices.Count == 2) {
			if (cats [activeCatIndices[0]].GetComponent<Cat> ().canDoThings && cats [activeCatIndices[1]].GetComponent<Cat> ().canDoThings) {
				StartCoroutine (CatSex (activeCatIndices [0], activeCatIndices [1]));
				activeCatIndices.Clear ();
			}

		}

	}


	IEnumerator CatSex(int cat1, int cat2){
		Vector3 explicitPos = Vector3.Lerp (cats [cat1].GetComponent<Cat> ().pathfinding.transform.position, 
			                      cats [cat2].GetComponent<Cat> ().pathfinding.transform.position, 0.5f);
		GameObject instance = Instantiate(Resources.Load("Prefab/ExplicitSign")) as GameObject;
		instance.transform.position = explicitPos;
		yield return new WaitForSeconds (2f);
		Destroy (instance);

		cats [cat1].GetComponent<Cat> ().canDoThings = true; 
		cats [cat2].GetComponent<Cat> ().canDoThings = true;

		yield return new WaitForSeconds (2f);
	}


	public void CatInteractWithCustomer(){
		if (activeHappyIndices.Count == 2) {
			StartCoroutine (CatHappy (activeHappyIndices [0], activeHappyIndices [1]));
			activeHappyIndices.Clear ();
		}

	}
	IEnumerator CatHappy(int cat, int customer){
		cats [cat].GetComponent<Cat> ().DisableAgent ();
		customers [customer].GetComponent<Customer> ().EnableObstacle ();
		yield return new WaitForSeconds (2f);
		cats [cat].GetComponent<Cat> ().EnableAgent ();
		customers [customer].GetComponent<Customer> ().DisableObstacle ();

		yield return new WaitForSeconds (4f);

		cats [cat].GetComponent<Cat> ().pathfinding.GetComponent<BoxCollider> ().enabled = true;
		customers [customer].GetComponent<Customer> ().pathfinding.GetComponent<BoxCollider> ().enabled = true;

	}
	*/

}
