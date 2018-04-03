using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CamController : MonoBehaviour {
	public Transform Center;
	public Camera cam;
	public float RotateSpeed = 2;
	public float ZoomSpeed = 0.125f;
	public float smoothspeed = 0.125f;
	public Transform FocusPoint;

	Vector3 prevTouchPos;
	Vector2 TouchPosDiff = Vector2.zero;
	Vector2 PointDiff = Vector2.zero;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	Vector3 Focus(Vector3 Position){
		Vector3 res;
		res.x = transform.position.x - Position.x;
		res.y = transform.position.y - Position.y;
		res.z = transform.position.z - Position.z;
		//Debug.DrawRay (transform.position,-res,Color.blue);
		return -res;
	}

	// After Update
	void FixedUpdate(){
		Vector3 CurTouchPos = Input.GetTouch(0).position;
		TouchPosDiff = new Vector2 (CurTouchPos.x - prevTouchPos.x, CurTouchPos.y - prevTouchPos.y);

		Ray prevRay = cam.ScreenPointToRay (prevTouchPos);
		RaycastHit prevHit;
		Physics.Raycast(prevRay, out prevHit);

		Ray curRay = cam.ScreenPointToRay (CurTouchPos);
		RaycastHit curHit;
		Physics.Raycast(curRay, out curHit);

		PointDiff = new Vector2 (curHit.point.x - prevHit.point.x, curHit.point.z - prevHit.point.z);
		prevTouchPos = CurTouchPos;
	}

	void Update()
	{

		if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Moved)
		{
			// Get movement of the finger since last frame
			Vector3 desiredPos = new Vector3 (transform.position.x - PointDiff.x/smoothspeed, transform.position.y, transform.position.z - PointDiff.y/smoothspeed);
			transform.position = Vector3.Lerp (transform.position, desiredPos, smoothspeed);
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
