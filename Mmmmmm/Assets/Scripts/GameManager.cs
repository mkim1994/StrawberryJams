using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour { //+hacky EventManager

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

	// Use this for initialization
	void Awake () {
		fillDestinations ();

		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Cat")) {
			cats.Add (go);
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Customer")) {
			customers.Add (go);
		}
	}

	void Start(){
		InvokeRepeating ("SpawnCustomer", 10f, Random.Range (minCustomerSpawnRate, maxCustomerSpawnRate));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
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
