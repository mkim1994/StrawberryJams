using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpScreen_Ui : MonoBehaviour {

	public GameObject help0;
	public GameObject help1;
	public GameObject help2;

	// Use this for initialization
	public void ChangePanelNext()
	{
		if (help0.activeSelf) {
			help0.SetActive (false);
			help1.SetActive (true);
		} else if (help1.activeSelf) {
			help1.SetActive (false);
			help2.SetActive (true);
		} 

	}

	public void ChangePanelPrevious(){


		if (help1.activeSelf) {
			help1.SetActive (false);
			help0.SetActive (true);
		} else if (help2.activeSelf) {
			help2.SetActive (false);
			help1.SetActive (true);
		}
	}
}
