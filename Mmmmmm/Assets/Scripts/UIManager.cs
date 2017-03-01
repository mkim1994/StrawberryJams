using System.Collections;
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

	GameManager gm;

	public int numOfShopToys;
	public int numOfInventoryToys;
	public int numOfShopFood;
	public int numOfInventoryFood;

	public int numOfCatPages;


	GameObject ShopFoodBookPage;
	GameObject ShopToyBookPage;

	GameObject InventoryFoodBookPage;
	GameObject InventoryToyBookPage;

	List<GameObject> InventoryToyPages;
	List<GameObject> InventoryFoodPages;
	List<GameObject> ShopToyPages;
	List<GameObject> ShopFoodPages;
	//Transform ToysShop = ShopUIElements [1].transform;

	// Use this for initialization
	void Start () {
		CatBookCanvas.SetActive (false);
		InventoryCanvas.SetActive (false);
		ShopCanvas.SetActive (false);

		CatUIElements = new List<GameObject> ();
		InventoryUIElements = new List<GameObject> ();
		ShopUIElements = new List<GameObject> ();

		InventoryToyPages = new List<GameObject> ();
		InventoryFoodPages = new List<GameObject> ();
		ShopToyPages = new List<GameObject> ();
		ShopFoodPages = new List<GameObject> ();

		foreach (Transform child in CatBookCanvas.transform) {
			CatUIElements.Add (child.gameObject);
		}

		foreach (Transform child in InventoryCanvas.transform) {
			InventoryUIElements.Add (child.gameObject);
		}

		foreach (Transform child in ShopCanvas.transform) {
			ShopUIElements.Add (child.gameObject);
		}

		foreach (Transform child in ShopUIElements[1].transform) {
			ShopToyPages.Add (child.gameObject);
		}
		foreach (Transform child in ShopUIElements[2].transform) {
			ShopFoodPages.Add (child.gameObject);
		}
		foreach (Transform child in InventoryUIElements[1].transform) {
			InventoryToyPages.Add (child.gameObject);
		}
		foreach (Transform child in InventoryUIElements[2].transform) {
			InventoryFoodPages.Add (child.gameObject);
		}

		ShopToyBookPage = ShopUIElements [1].transform.GetChild (0).GetChild (1).gameObject;
		ShopFoodBookPage = ShopUIElements [2].transform.GetChild (0).GetChild (1).gameObject;
		InventoryToyBookPage = InventoryUIElements [1].transform.GetChild (0).GetChild (1).gameObject;
		InventoryFoodBookPage = InventoryUIElements [2].transform.GetChild (0).GetChild (1).gameObject;


		gm = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		moneyText.text = "$"+gm.money;

		UpdateFoodInventory ();
		UpdateCatCurrentPage ();
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
		for (int i = 1; i < numOfCatPages + 1; i++) {
			CatUIElements [i].SetActive (false);
		}

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

		for (int i = 1; i < numOfInventoryToys + 1; i++) {
			InventoryUIElements [1].transform.GetChild(i).gameObject.SetActive (false);
		}
	}

	public void clickInventoryFood(){
		InventoryUIElements [0].SetActive (false);
		InventoryUIElements [1].SetActive (false);
		InventoryUIElements [2].SetActive (true);

		for (int i = 1; i < numOfInventoryFood + 1; i++) {
			InventoryUIElements [2].transform.GetChild(i).gameObject.SetActive (false);
		}
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

		for (int i = 1; i < numOfShopToys + 1; i++) {
			ShopUIElements [1].transform.GetChild(i).gameObject.SetActive (false);
		}
	}

	public void clickShopFood(){
		ShopUIElements [0].SetActive (false);
		ShopUIElements [1].SetActive (false);
		ShopUIElements [2].SetActive (true);

		for (int i = 1; i < numOfShopFood + 1; i++) {
			ShopUIElements [2].transform.GetChild(i).gameObject.SetActive (false);
		}
	}

	public void clickCatFromBook(int page){
		CatUIElements [0].SetActive (false);


		for (int i = 1; i < numOfCatPages + 1; i++) {
			ShopUIElements [2].transform.GetChild(i).gameObject.SetActive (false);
		}
		CatUIElements [1].SetActive (true);


	}

	public void clickExitToCatBook(){
		CatUIElements [0].SetActive (true);
		CatUIElements [1].SetActive (false);
	}

	//[1] is toy, [2] is food
	public void flipThroughPagesShopToy(int page){
		Transform ToysShop = ShopUIElements [1].transform;
		//skip (0) because it's the ToyPanel

		for (int i = 1; i < numOfShopToys + 1; i++) {
			if (i == page) {
				ToysShop.GetChild (page).gameObject.SetActive (true);
			} else{
				ToysShop.GetChild (i).gameObject.SetActive (false);
			}
		}
	}

	public void flipThroughPagesShopFood(int page){
		Transform FoodShop = ShopUIElements [2].transform;
		//skip (0) because it's the ToyPanel

		for (int i = 1; i < numOfShopFood + 1; i++) {
			if (i == page) {
				FoodShop.GetChild (page).gameObject.SetActive (true);
			} else{
				FoodShop.GetChild (i).gameObject.SetActive (false);
			}
		}
	}

	public void flipThroughPagesInventoryToy(int page){
		Transform ToyInventory = InventoryUIElements [1].transform;
		//skip (0) because it's the ToyPanel

		for (int i = 1; i < numOfInventoryToys + 1; i++) {
			if (i == page) {
				ToyInventory.GetChild (page).gameObject.SetActive (true);
			} else{
				ToyInventory.GetChild (i).gameObject.SetActive (false);
			}
		}
	}

	public void flipThroughPagesInventoryFood(int page){
		Transform FoodInventory = InventoryUIElements [2].transform;
		//skip (0) because it's the ToyPanel

		for (int i = 1; i < numOfInventoryFood + 1; i++) {
			if (i == page) {
				FoodInventory.GetChild (page).gameObject.SetActive (true);
			} else{
				FoodInventory.GetChild (i).gameObject.SetActive (false);
			}
		}
	}

	public void flipThroughPagesCat(int page){
		//Transform CatBook = CatUIElements [1].transform;

		CatUIElements [0].gameObject.SetActive (false);

		for (int i = 1; i < numOfCatPages + 1; i++) {
			if (i == page) {
				CatUIElements[i].gameObject.SetActive (true);
			} else {
				CatUIElements[i].gameObject.SetActive (false);
			}
		}
	}

	public void PurchaseToy(int toy){
		int cost = 0;
		if (toy == 1) {
			cost = 10;
		} else if (toy == 2) {
			cost = 10;
		} else if (toy == 3) {
			cost = 20;
		} else if (toy == 4) {
			cost = 20;
		} else if (toy == 5) {
			cost = 25;
		} else if (toy == 6) {
			cost = 30;
		} else if (toy == 7) {
			cost = 40;
		} else if (toy == 8) {
			cost = 50;
		} else{
			cost = 100;
		}
		if (gm.money - cost >= 0) {
			gm.money -= cost;
			gm.purchaseToy (toy);

			InventoryToyBookPage.transform.GetChild (toy - 1).gameObject.GetComponent<Button> ().interactable = true;
			ShopToyBookPage.transform.GetChild (toy - 1).gameObject.GetComponent<Button> ().interactable = false;
			clickShopToy ();
		} else {
			Debug.Log ("insufficient funds");
		}

	}

	public void PurchaseFood(int food){
		int cost = 0;
		if (food == 1) {
			cost = 5;
		} else if (food == 2) {
			cost = 5;
		} else if (food == 3) {
			cost = 10;
		} else if (food == 4) {
			cost = 10;
		} else if (food == 5) {
			cost = 15;
		} else if (food == 6) {
			cost = 15;
		} else if (food == 7) {
			cost = 20;
		} else if (food == 8) {
			cost = 25;
		} else{
			cost = 50;
		}
		if (gm.money - cost >= 0) {
			gm.money -= cost;
			gm.purchaseFood(food);
			InventoryFoodBookPage.transform.GetChild (food - 1).gameObject.GetComponent<Button> ().interactable = true;
			gm.foodsInInventory [food - 1] += 1;
		} else {
			Debug.Log ("insufficient funds");
		}

	}

	void UpdateFoodInventory(){
		for (int i = 1; i < InventoryFoodPages.Count; i++) {
			InventoryFoodPages [i].transform.GetChild (4).GetChild (0).gameObject.GetComponent<Text> ().text = "IN STOCK: " + gm.foodsInInventory [i - 1];
		}

		for (int j = 1; j < ShopFoodPages.Count; j++) {
			ShopFoodPages [j].transform.GetChild (4).GetChild (0).gameObject.GetComponent<Text> ().text = "IN STOCK: " + gm.foodsInInventory [j - 1];
		}
	}

	void UpdateCatCurrentPage(){

	}
}
