using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	float minZoom = 2f;
	float maxZoom = 11f;

	float minX = -4f;
	float minY = 4.7f;
	float minZ= 8f;

	float cameraSpeed = 0.1f;

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
			Vector3 tempPos = new Vector3 (minX,minY,minZ);
			Camera.main.transform.position = tempPos;
			/*iTween.MoveTo (this.gameObject, iTween.Hash (
				"position", tempPos,
				"time",1f,
				"easetype","easeOutBack"
			));*/
		}

		/*if (Input.GetMouseButton (0)) {*/
		Camera.main.transform.position += new Vector3 (-Input.GetAxis ("Mouse X") * cameraSpeed, Input.GetAxis ("Mouse Y") * cameraSpeed);
		//Camera.main.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition)*cameraSpeed;
		/*	Camera.main.transform.LookAt (Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x,
				Input.mousePosition.y, Camera.main.transform.position.z)), Vector3.up);*/
		//}
	}

	void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
	{

		/*if (minZoom < Camera.main.orthographicSize && Camera.main.orthographicSize < maxZoom) {*/
		// Move camera
		// Calculate how much we will have to move towards the zoomTowards position
		/*	float multiplier = (1.0f / Camera.main.orthographicSize * amount);
			Camera.main.transform.position += (zoomTowards - transform.position) * multiplier; */
		//}

		// Zoom camera
		Camera.main.orthographicSize -= amount;

		transform.GetChild (0).gameObject.GetComponent<Camera> ().orthographicSize -= amount;

		// Limit zoom
		Camera.main.orthographicSize = Mathf.Clamp (Camera.main.orthographicSize, minZoom, maxZoom);

		transform.GetChild (0).gameObject.GetComponent<Camera> ().orthographicSize = Mathf.Clamp (transform.GetChild (0).gameObject.GetComponent<Camera> ().orthographicSize, minZoom, maxZoom);

		//Limit Move
		//Camera.main.transform.position = new Vector3 (


		//	);
	}
}
