using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothyCamera : MonoBehaviour {

	// Which camera looks at
	public Transform lookAt;
	private float smoothSpeed = 0.125f;
	private Vector3 offset = new Vector3 (0, 30, 0);

	void Update () {
		Vector3 desiredPosition = lookAt.transform.position + offset;
		transform.position = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);
	}
}
