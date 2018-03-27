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

	public int zoom = 14;
	public string mapWidth = "640";
	public string mapHeight = "640";
	public string scalevalue = "2";

	public enum mapType {roadmap,satellite,hybrid,terrain}
	public mapType mapSelected;
	public int scale;

	public string GM_KEY = "AIzaSyBPn5hf-yVrOuZmYqwJBkMfrVzwkRduLaI";
	IEnumerator Map()
	{
		url = "https://maps.googleapis.com/maps/api/staticmap?center=10.883050,106.782950&zoom=16&" +
		"size=" + mapWidth + "x" + mapHeight +
		"&scale=" + scalevalue +"&"+
		"key=" + GM_KEY;
		
		Debug.Log (url);
		WWW www = new WWW (url);
		Debug.Log ("GOT");
		yield return www;
		a.mainTexture = www.texture;
		Debug.Log ("GOT TEXT");
	}
	// Use this for initialization
	void Start () {
		a = GetComponent<MeshRenderer> ().material;
		StartCoroutine (Map());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}