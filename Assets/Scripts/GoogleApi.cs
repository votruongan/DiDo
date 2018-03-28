using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleApi : MonoBehaviour {
	public Material a;

	string url;

	public float lat;
	public float lon;

	LocationInfo li;

	public float zoomLevel = 14;
	public string Center;
	public string mapWidth = "640";
	public string mapHeight = "640";
	public string scalevalue = "2";

	public string mapType;
	public int scale;

	public string GM_KEY = "AIzaSyBPn5hf-yVrOuZmYqwJBkMfrVzwkRduLaI";
	IEnumerator Map()
	{
		url = "https://maps.googleapis.com/maps/api/staticmap?center=" + Center +
			"&zoom=" + zoomLevel.ToString() +
		"&size=" + mapWidth + "x" + mapHeight +
		"&maptype=" + mapType +
		"&scale=" + scalevalue +
		"&key=" + GM_KEY;
		
		Debug.Log (url);
		WWW www = new WWW (url);
		Debug.Log ("GOT");
		yield return www;
		a.mainTexture = www.texture;
		Debug.Log ("GOT TEXT");
	}
	// Use this for initialization
	public void UpdateMap () {
		a = GetComponent<MeshRenderer> ().material;
		StartCoroutine (Map());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}