using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
	public GameObject StartPanel;
	public GameObject SearchPanel;
	public CamController camcon;
	public GoogleApi MapCon;
	public Transform Center;
	public Transform Empty;
	public string[] Sectors_Pos;
	public float ZoomLevelA;
	public float ZoomLevelB;
	public GameObject SectorB;
	public GameObject SectorA;
	public LanguageController LangCont;

	void Close_n_Focus(bool state){
		StartPanel.SetActive (state);
		camcon.FocusPoint = (state)?(Empty):(Center);
	}

	public void Select_Search(){
		StartPanel.SetActive (false);
		SearchPanel.SetActive (true);		
	}

	public void Select_Sector(int SectorID){
		switch (SectorID) {
			//Sector A
		case 0:
			SectorA.SetActive (true);
			MapCon.Center = Sectors_Pos [0];
			MapCon.zoomLevel = ZoomLevelA;
			MapCon.UpdateMap ();
			Center.position = new Vector3 (0f,0f,0f);
			Close_n_Focus(false);
			break;

			//Sector B
		case 1:
			SectorB.SetActive (true);
			MapCon.Center = Sectors_Pos [1];
			MapCon.zoomLevel = ZoomLevelB;
			MapCon.UpdateMap ();
			Center.position = new Vector3 (0f,0f,0f);
			Close_n_Focus(false);
			break;
		}
	}
	IEnumerator Wait(float sec){
		yield return new WaitForSeconds(sec);
	}
	void Start(){
		StartCoroutine(Wait(0.02f));
		LangCont.UpdateLanguage ();
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SearchPanel.SetActive (false);
			SectorA.SetActive (false);
			SectorB.SetActive (false);
			Close_n_Focus (true);
		}
	}
}
