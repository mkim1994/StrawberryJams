﻿using System.Collections;
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


	public int money;

	//public List<Toy> toys;
	public Toy[] toys;
	public List<Food> foods;
	public int[] foodsInInventory;

	float moneyperiod = 0f;
	float timeinterval = 10f;

	int catIDcount;
	UIManager uimanager;

	public GameObject[] existingCats;

	public int activeCats;

	AudioManager audiomanager;

	// Use this for initialization
	void Awake () {
		audiomanager = GameObject.FindWithTag ("AudioManager").GetComponent<AudioManager> ();
		uimanager = GameObject.FindWithTag ("UIManager").GetComponent<UIManager> ();
		fillDestinations ();

		toys = new Toy[uimanager.numOfShopToys];
		foods = new List<Food> ();
		foodsInInventory = new int[uimanager.numOfInventoryFood];


		catIDcount = 1;
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Cat")) {
			go.GetComponent<Cat> ().uniqueID = catIDcount;
			cats.Add (go);
			catIDcount++;
		}
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Customer")) {
			customers.Add (go);
		}

		activeCats = 3;
	}

	void Start(){
		InvokeRepeating ("SpawnCustomer", 30f, Random.Range (minCustomerSpawnRate, maxCustomerSpawnRate));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			Application.LoadLevel ("main");
		}

		UpdateCatStatus ();

		CountMoney ();
		//CheckSexyTimes ();
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

	public void purchaseToy(int toy){
		Toy newToy = new Toy ();
		newToy.toyIndex = toy;
		if (toy == 1) {
			newToy.hornygrade = 1;
		} else if (toy == 2) {
			newToy.hornygrade = 1;
		} else if (toy == 3) {
			newToy.hornygrade = 2;
		} else if (toy == 4) {
			newToy.hornygrade = 2;
		} else if (toy == 5) {
			newToy.hornygrade = 3;
		} else if (toy == 6) {
			newToy.hornygrade = 3;
		} else if (toy == 7) {
			newToy.hornygrade = 4;
		} else if (toy == 8) {
			newToy.hornygrade = 4;
		} else{
			newToy.hornygrade = 5;
		}
		toys[toy-1] = newToy;
	}

	public void purchaseFood(int food){
		Food newFood = new Food ();
		newFood.foodIndex = food;
		if (food == 1) {
			newFood.fullgrade = 1;
		} else if (food == 2) {
			newFood.fullgrade = 1;
		} else if (food == 3) {
			newFood.fullgrade = 2;
		} else if (food == 4) {
			newFood.fullgrade = 2;
		} else if (food == 5) {
			newFood.fullgrade = 3;
		} else if (food == 6) {
			newFood.fullgrade = 3;
		} else if (food == 7) {
			newFood.fullgrade = 4;
		} else if (food == 8) {
			newFood.fullgrade = 4;
		} else{
			newFood.fullgrade = 5;
		}
		foods.Add(newFood);
	}
		
	public void UpdateCatToyStatus(int catIndex, int toy){
		Toy currentToy = toys [toy-1];
		currentToy.inUse = true;
		cats [catIndex].GetComponent<Cat> ().horniness = currentToy.hornygrade;
		cats [catIndex].GetComponent<Cat> ().usingToy = currentToy.toyIndex;

	}
	public void UpdateCatFoodStatus(int catIndex, int food){

		if (cats [catIndex].GetComponent<Cat> ().ateFood == 0) {
			Food currentFood = new Food ();
			for (int i = 0; i < foods.Count; i++) {
				if (foods [i].foodIndex == food) {
					currentFood = foods [i];
					foods.RemoveAt (i);
					foodsInInventory [food - 1] -= 1;
					break;
				}
			}

			cats [catIndex].GetComponent<Cat> ().happiness = currentFood.fullgrade;
			cats [catIndex].GetComponent<Cat> ().ateFood = currentFood.foodIndex;
			audiomanager.cateat.PlayOneShot (audiomanager.cateat.clip);
		} else {
			StartCoroutine (uimanager.MessageDisplayTooFull ());
		}

	}
		
	public void UpdateCatStatus(){
		foreach (GameObject go in GameObject.FindGameObjectsWithTag ("Cat")) {
			if (!cats.Contains (go)) {
				cats.Add (go);
				activeCats++;
			}
		}

	}
}
