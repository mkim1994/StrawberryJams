using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour { //+hacky EventManager

	public GameObject censored;

	[HideInInspector]
	public List<Transform> catdestinations;
	public Transform catdestinationgroup;

	[HideInInspector]
	public List<Transform> customerdestinations;
	public Transform customerdestinationgroup;

	[HideInInspector]
	public List<GameObject> customers;
	[HideInInspector]
	public List<GameObject> cats;

	public List<GameObject> customerPrefabs;

	public float minCustomerSpawnRate, maxCustomerSpawnRate;


	public int money;

	List<Toy> toys;
	List<Food> food;

	float moneyperiod = 0f;
	float timeinterval = 10f;

	int catIDcount;

	// Use this for initialization
	void Awake () {
		fillDestinations ();


		catIDcount = 1;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Cat")) {
			go.GetComponent<Cat> ().uniqueID = catIDcount;
			cats.Add (go);
			catIDcount++;
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Customer")) {
			customers.Add (go);
		}
	}

	void Start(){
		//InvokeRepeating ("SpawnCustomer", 30f, Random.Range (minCustomerSpawnRate, maxCustomerSpawnRate));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}


		CountMoney ();
		//CheckSexyTimes ();
	}

	void CheckSexyTimes(){
		//WHAT
		//use theOtherCatID and uniqueID to get rid of duplicates
		Transform censoreds = GameObject.FindWithTag("Censoreds").transform;
		foreach (GameObject cat in cats) {
			if (cat.GetComponent<Cat> ().sexytimes) {
				for (int i = 0; i < censoreds.childCount; i++) {
					if (!censoreds.GetChild(i).gameObject.activeSelf) {
						return;
					}
				}
			}
		}
	}

	void CountMoney(){
		if (moneyperiod > timeinterval/(customers.Count*0.9f)) {
			money += 1;
			moneyperiod = 0f;
		}
		moneyperiod += Time.deltaTime;
	}

	void fillDestinations(){
		foreach (Transform child in catdestinationgroup) {
			catdestinations.Add (child);
		}
		foreach (Transform child in customerdestinationgroup) {
			customerdestinations.Add (child); //0 index is always entrance/exit
		}
	}

	void SpawnCustomer(){
		GameObject customer = Instantiate (customerPrefabs [Random.Range (0, customerPrefabs.Count)], customerdestinations [0].position, Quaternion.identity);
		customer.transform.eulerAngles = new Vector3 (0f, -90f, 0f);
		customers.Add (customer);
	}

}
