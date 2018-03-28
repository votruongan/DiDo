using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothyCamera : MonoBehaviour {

	// Which camera looks at
	public Transform lookAt;
	private float smoothSpeed = 0.125f;

	void Update () {
		Vector3 desiredPosition = lookAt.transform.position;
		transform.position = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
	}
}
