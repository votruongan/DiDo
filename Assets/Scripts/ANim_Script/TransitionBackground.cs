using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionBackground : MonoBehaviour {
	public bool isComing = false;
	public bool isFading = false;
	public float TransitionSpeed;
	public RawImage img;
	private Color tcol;
	// Use this for initialization
	void Start () {
		img = GetComponent<RawImage> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if ((img.color.a - TransitionSpeed >= 0) && (isFading)){
			tcol = img.color;
			tcol.a -= TransitionSpeed;
			img.color = tcol;
		}
		if ((img.color.a + TransitionSpeed < 1f) && (isComing)){
			tcol = img.color;
			tcol.a += TransitionSpeed;
			img.color = tcol;
		}
		if (img.color.a == 0) {
			isFading = false;
			gameObject.SetActive (false);
		}
		if (img.color.a == 1f) {
			isComing = false;
		}
	}
}
