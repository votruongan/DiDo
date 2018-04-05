using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Xml;


public class SearchController : MonoBehaviour {
	public Text SearchText;
	public RawImage buildingImage;
	public Text buildingName;
	public Text Gender;
	public Text Desc;
	public string host = "https://nhquang.me";
	// Use this for initialization
	void OnEnabled () {
		
	}

	public void Start_FindBuilding(string BName){
		StartCoroutine (FindBuilding (BName));
	}

	IEnumerator FindBuilding(string BName){
	 	string srch = SearchText.text;
		WWW conn = new WWW(host + "/?building=detail" );
				yield return conn;
		Debug.Log (conn.text);
		XmlDocument doc = new XmlDocument ();
		doc.LoadXml (conn.text);
		XmlNodeList nodes = doc.SelectNodes ("Info/Building");
		foreach (XmlNode nd in nodes) {
			if (nd.SelectSingleNode("Name").InnerText == BName) {
				buildingName.text = BName;
				Debug.Log(nd.SelectSingleNode("Gender").InnerText);
				WWW img = new WWW(nd.SelectSingleNode("Image").InnerText);
				yield return img;
				buildingImage.texture = img.texture;
				Debug.Log(nd.SelectSingleNode("Image").InnerText);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
