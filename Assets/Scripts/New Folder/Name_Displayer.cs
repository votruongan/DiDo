using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Name_Displayer : MonoBehaviour {
	public Vector3 Rotation;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<TextMesh> ().text = gameObject.transform.parent.gameObject.name;;
		transform.localPosition = new Vector3 (0.15f,9f,0f);
	}
}
