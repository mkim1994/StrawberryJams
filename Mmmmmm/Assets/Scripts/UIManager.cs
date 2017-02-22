﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject MainMenuCanvas;
	public GameObject CatBookCanvas;
	public GameObject InventoryCanvas;
	public GameObject ShopCanvas;

	public Text moneyText;

	List<GameObject> CatUIElements;
	List<GameObject> InventoryUIElements;
	List<GameObject> ShopUIElements;

	Player player;

	// Use this for initialization
	void Start () {
		CatUIElements = new List<GameObject> ();
		InventoryUIElements = new List<GameObject> ();
		ShopUIElements = new List<GameObject> ();

		foreach (Transform child in CatBookCanvas.transform) {
			CatUIElements.Add (child.gameObject);
		}

		foreach (Transform child in InventoryCanvas.transform) {
			InventoryUIElements.Add (child.gameObject);
		}

		foreach (Transform child in ShopCanvas.transform) {
			ShopUIElements.Add (child.gameObject);
		}

		player = GameObject.FindWithTag ("Player").GetComponent<Player> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = "$"+player.money;
	}

	public void clickExitToCafe(){
		CatBookCanvas.SetActive (false);
		InventoryCanvas.SetActive (false);
		ShopCanvas.SetActive (false);
	}

	public void clickCatBook(){
		CatBookCanvas.SetActive (true);
		InventoryCanvas.SetActive (false);
		ShopCanvas.SetActive (false);

		CatUIElements [0].SetActive (true);
		CatUIElements [1].SetActive (false);

	}
	public void clickInventory(){
		CatBookCanvas.SetActive (false);
		InventoryCanvas.SetActive (true);
		ShopCanvas.SetActive (false);

		InventoryUIElements [0].SetActive (true);
		InventoryUIElements [1].SetActive (false);
		InventoryUIElements [2].SetActive (false);
	}
	public void clickShop(){
		CatBookCanvas.SetActive (false);
		InventoryCanvas.SetActive (false);
		ShopCanvas.SetActive (true);

		ShopUIElements [0].SetActive (true);
		ShopUIElements [1].SetActive (false);
		ShopUIElements [2].SetActive (false);

	}

	public void clickInventoryToy(){
		InventoryUIElements [0].SetActive (false);
		InventoryUIElements [1].SetActive (true);
		InventoryUIElements [2].SetActive (false);

		InventoryUIElements [1].transform.GetChild (0).gameObject.SetActive (true);
		InventoryUIElements [1].transform.GetChild (1).gameObject.SetActive (false);
	}

	public void clickInventoryFood(){
		InventoryUIElements [0].SetActive (false);
		InventoryUIElements [1].SetActive (false);
		InventoryUIElements [2].SetActive (true);

		InventoryUIElements [2].transform.GetChild (0).gameObject.SetActive (true);
		InventoryUIElements [2].transform.GetChild (1).gameObject.SetActive (false);
		//InventoryUIElements [0].SetActive (false);
	}

	public void clickInventoryToyPage(){
		InventoryUIElements [1].transform.GetChild (0).gameObject.SetActive (false);
		InventoryUIElements [1].transform.GetChild (1).gameObject.SetActive (true);
	}

	public void clickInventoryFoodPage(){
		InventoryUIElements [2].transform.GetChild (0).gameObject.SetActive (false);
		InventoryUIElements [2].transform.GetChild (1).gameObject.SetActive (true);
	}

	public void clickShopToyPage(){
		ShopUIElements [1].transform.GetChild (0).gameObject.SetActive (false);
		ShopUIElements [1].transform.GetChild (1).gameObject.SetActive (true);
	}

	public void clickShopFoodPage(){
		ShopUIElements [2].transform.GetChild (0).gameObject.SetActive (false);
		ShopUIElements [2].transform.GetChild (1).gameObject.SetActive (true);
	}

	public void clickShopToy(){
		ShopUIElements [0].SetActive (false);
		ShopUIElements [1].SetActive (true);
		ShopUIElements [2].SetActive (false);

		ShopUIElements [1].transform.GetChild (0).gameObject.SetActive (true);
		ShopUIElements [1].transform.GetChild (1).gameObject.SetActive (false);
	}

	public void clickShopFood(){
		ShopUIElements [0].SetActive (false);
		ShopUIElements [1].SetActive (false);
		ShopUIElements [2].SetActive (true);

		ShopUIElements [2].transform.GetChild (0).gameObject.SetActive (true);
		ShopUIElements [2].transform.GetChild (1).gameObject.SetActive (false);
	}

	public void clickCatFromBook(){
		//in a separate method, change out all the elements of the page according to the cat type
		//(cat type is determined by the button of the cat you click)
		CatUIElements [0].SetActive (false);
		CatUIElements [1].SetActive (true);
	}

	public void clickExitToCatBook(){
		CatUIElements [0].SetActive (true);
		CatUIElements [1].SetActive (false);
	}
}
