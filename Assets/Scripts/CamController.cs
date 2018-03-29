using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour {
	public Text txt;
	public Camera cam;
	public float RotateSpeed;
	public float ZoomSpeed;
	public float orthoZoomSpeed;
	public float perspectiveZoomSpeed;

	public float smoothSpeed = 0.005f;

	public float Delta;
	public Transform FocusPoint;
	public bool isIdle = true;

	public Object orig;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}


	Vector3 Focus(Vector3 Position){
		Vector3 res;
		res.x = transform.position.x - Position.x;
		res.y = transform.position.y - Position.y;
		res.z = transform.position.z - Position.z;
		Debug.DrawRay (transform.position,-res,Color.blue);
		return -res;

	}

	void Update()
	{

		if (Input.GetButton("Fire1")) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

		

			if (Physics.Raycast (ray, out hit) && hit.collider.tag == "Ground") {
				
				Vector3 move = new Vector3(hit.point.x - transform.position.x, 0, hit.point.z - transform.position.z);

				Vector3 desiredPosition = transform.position + move;

				transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

			
			}

		}


/*		if ((FocusPoint != null) && (isIdle)) {
			transform.LookAt (FocusPoint);
		}
*/
		if (Input.touchCount == 1 && Input.GetTouch (0).phase == TouchPhase.Moved) {
/*
			// Get movement of the finger since last frame
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
			if (Mathf.Abs(transform.rotation.x) >= 85.0f) {
				touchDeltaPosition.y = 0.0f;
			}
			Vector3 rot = new Vector3 (touchDeltaPosition.y, touchDeltaPosition.x, 0.0f);
			transform.Rotate (rot * RotateSpeed);
			txt.text += touchDeltaPosition.y.ToString () + (-touchDeltaPosition.x).ToString ();
			isIdle = false;
*/
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit) && hit.collider.tag == "Ground") {

				Vector3 move = new Vector3 (hit.point.x - transform.position.x, 0, hit.point.z - transform.position.z);

				Vector3 desiredPosition = transform.position + move;

				transform.position = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
			}
		}
		if (Input.touchCount == 0) {
			transform.Rotate (Vector3.zero);
		}
		// If there are two touches on the device...
		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			Vector3 pos = transform.position;
			pos.y += deltaMagnitudeDiff * ZoomSpeed;
			pos.z += deltaMagnitudeDiff * ZoomSpeed;

			transform.position = pos;
		}

	}
}
