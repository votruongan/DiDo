using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoogleApi : MonoBehaviour {
	public Material a;
	public bool Loading;
	string url;
	public GameObject Load_Panel;
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
		"&zoom=" + zoomLevel.ToString () +
		"&size=" + mapWidth + "x" + mapHeight +
		"&maptype=" + mapType +
		"&scale=" + scalevalue +
		"&style=feature:poi|visibility:off"+
		"&style=element:labels|visibility:off" +
		"&style=feature:landscape.man_made|geometry.fill|color:0x0080C8" +
		"&style=feature:landscape.natural|geometry.fill|color:0x77dd77" +
		"&key=" + GM_KEY;
		
		Debug.Log (url);
		WWW www = new WWW (url);
		yield return www;
		a.mainTexture = www.texture;
		Debug.Log ("GOT TEXTURE");
		Loading = false;
		Load_Panel.SetActive (false);
	}

	IEnumerator WaitandLoad(float secs){
		yield return new WaitForSeconds(secs);
		if (Loading) {
			Load_Panel.SetActive (true);			
		}
	}

	// Use this for initialization
	public void UpdateMap () {
		a = GetComponent<MeshRenderer> ().material;
		Loading = true;
		StartCoroutine (Map());
		WaitandLoad (0.2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}