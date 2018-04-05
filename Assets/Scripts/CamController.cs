using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour {
	public Transform Center;
	public Camera cam;
	public float RotateSpeed = 2;
	public float ZoomSpeed = 0.001f;
	public float smoothspeed = 0.125f;
	public float maxCameraHigh = 40.0f;
	public float minCameraHigh = 2.0f;
	public float maxRotateAngles = 80.0f;
	public float minRotateAngles = 15.0f; 
	public Transform FocusPoint;
	public int tapCount = 0;
	public float doubleTapTimer = 0f;
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	bool isZoom(Touch touchZero, Touch touchOne) {
		
		Vector2 deltaTouchZero = touchZero.deltaPosition / touchZero.deltaPosition.magnitude;
		Vector2 deltaTouchOne = touchOne.deltaPosition / touchOne.deltaPosition.magnitude;

		Vector2 sumVector = deltaTouchOne + deltaTouchZero;
		if (sumVector.magnitude < 1)
			return true;
		return false;
	}

	bool isRotateUpDown(Touch touchZero, Touch touchOne) {
		if (isZoom(touchZero, touchOne)) return false;
		Vector2 deltaTouchZero = touchZero.deltaPosition;
		return Mathf.Abs (deltaTouchZero.y) > Mathf.Abs (deltaTouchZero.x);
	}

	bool isRotateLeftRight(Touch touchZero, Touch touchOne) {
		if (isZoom(touchZero, touchOne)) return false;
		Vector2 deltaTouchZero = touchZero.deltaPosition;
		return Mathf.Abs (deltaTouchZero.x) > Mathf.Abs (deltaTouchZero.y);
	}

	void Update()
	{

		if (Input.touchCount == 0) {
			transform.Rotate (Vector3.zero);
		}

		// Move camera
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			Touch curTouch = Input.GetTouch (0);
			Vector2 curTouchPos = curTouch.position;
			Vector2 prevTouchPos = curTouch.position - curTouch.deltaPosition;

			Ray prevRay = cam.ScreenPointToRay (prevTouchPos);
			RaycastHit prevHit;
			Physics.Raycast(prevRay, out prevHit);


			Ray curRay = cam.ScreenPointToRay (curTouchPos);
			RaycastHit curHit;
			if (!Physics.Raycast(curRay, out curHit)) curHit = prevHit;
			Debug.Log (prevHit.point + ", " + curHit.point);

			Vector3 PointDiff = curHit.point - prevHit.point;

			Vector3 desiredPos = transform.position - PointDiff/smoothspeed;
			transform.position = Vector3.Lerp (transform.position, desiredPos, smoothspeed);
		}

		//If there are two touches on the device...
		if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved) {

			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
		
			// Zoom
			if (isZoom(touchZero, touchOne)) {
				// Focus on the middle point between two touch
				Vector2 screenFocusPoint = (touchOne.position + touchZero.position) / 2;
				Ray ray = cam.ScreenPointToRay (screenFocusPoint);
				RaycastHit hit;
				Physics.Raycast (ray, out hit);

				// Delta magnitude
				Vector2 deltaPreVecto = touchZeroPrevPos - touchOnePrevPos;
				Vector2 deltaCurVecto = touchZero.position - touchOne.position;
				float deltaMag = deltaCurVecto.magnitude - deltaPreVecto.magnitude;

				// Zoom
				if (deltaMag > 0) {
					if (transform.position.y > minCameraHigh)
						transform.position = Vector3.Lerp (transform.position, hit.point, ZoomSpeed);
				} else { 
					if (transform.position.y < maxCameraHigh)
						transform.position = Vector3.Lerp (transform.position, 2 * transform.position - hit.point, ZoomSpeed);
				}
			}

			if (isRotateUpDown (touchZero, touchOne)) {
				Vector3 rot = new Vector3 (touchZero.deltaPosition.y, 0, 0);
				transform.Rotate (rot * RotateSpeed);
			}

			if (isRotateLeftRight(touchZero, touchOne)) {
				// Focus on the middle point between two touch
				Vector2 screenFocusPoint = new Vector2 (Screen.width /2, Screen.height /2);
				Ray ray = cam.ScreenPointToRay (screenFocusPoint);
				RaycastHit hit;
				Physics.Raycast (ray, out hit);

				Vector3 axis = new Vector3 (0, touchZero.deltaPosition.x, 0);
				transform.RotateAround (hit.point, axis, RotateSpeed * 10);
			}


		}

		// Double tap to choose a building
		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
		{
			tapCount++;
		}
		if (tapCount > 0)
		{
			doubleTapTimer += Time.deltaTime;
		}
		if (tapCount >= 2)
		{
			Ray ray = cam.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit;
			Physics.Raycast (ray, out hit);
			/*
			Vector3 desiredPos = hit.collider.gameObject.transform.position - 10 * (transform.position - hit.collider.gameObject.transform.position)
								/ (hit.collider.gameObject.transform.position - transform.position).y;
			transform.position = Vector3.Lerp (transform.position, desiredPos, 2 * smoothspeed);
		*/
			doubleTapTimer = 0.0f;
			tapCount = 0;
		}
		if (doubleTapTimer > 0.5f)
		{
			doubleTapTimer = 0f;
			tapCount = 0;
		}
	}
}
