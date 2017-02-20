using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	float minZoom = 2f;
	float maxZoom = 11f;
	/*float minX = 1.2
	float maxMove;*/

	float minX = -4.55f;
	float minY = 8.14f;
	float minZ= 7.55f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse ScrollWheel") > 0){
			ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), 1f);
		}

		if (Input.GetAxis ("Mouse ScrollWheel") < 0f) {
			ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), -1f);
		}

		if (Camera.main.orthographicSize == maxZoom) {
			Vector3 tempPos = new Vector3 (-4f,8f,8f);
			iTween.MoveTo (this.gameObject, iTween.Hash (
				"position", tempPos,
				"time",1f,
				"easetype","easeOutBack"
			));
		}
	}

	void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
	{

		if (minZoom < Camera.main.orthographicSize && Camera.main.orthographicSize < maxZoom) {
			// Move camera
			// Calculate how much we will have to move towards the zoomTowards position
			float multiplier = (1.0f / Camera.main.orthographicSize * amount);
			Camera.main.transform.position += (zoomTowards - transform.position) * multiplier; 
		}

		// Zoom camera
		Camera.main.orthographicSize -= amount;

		// Limit zoom
		Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, minZoom, maxZoom);

		//Limit Move
		//Camera.main.transform.position = new Vector3 (


	//	);
	}
}
