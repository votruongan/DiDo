using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnim : MonoBehaviour {
	public GameObject Left;
	public GameObject Middle;
	public GameObject Right;
	public GameObject Dido;
	public GameObject Slogan;
	public GameObject ButtA;
	public GameObject ButtB;

	IEnumerator Wait(float secs, GameObject toActive){
		yield return new WaitForSeconds(secs);
		toActive.SetActive (true);
	}

	// Use this for initialization
	void Start () {
		Middle.SetActive (true);	
		StartCoroutine(Wait (0.1f,Left));
		StartCoroutine(Wait (0.3f,Right));
		StartCoroutine(Wait (0.6f,Dido));
		StartCoroutine(Wait (0.9f,Slogan));
		StartCoroutine(Wait (0.8f,ButtA));
		StartCoroutine(Wait (0.9f,ButtB));
	}
}
