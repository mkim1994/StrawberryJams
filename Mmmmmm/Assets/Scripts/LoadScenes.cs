using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toMainGame() {
		SceneManager.LoadScene ("main");
	}

	public void toMainMenu() {
		SceneManager.LoadScene ("Start");
	}

	public void toIntro() {
		SceneManager.LoadScene ("IntroScene");
	}
}
