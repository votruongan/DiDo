using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
	[Header ("UI Settings")]
	public CamController camcon;
	public LanguageController LangCont;
	public GameObject Plane;
	public GameObject StartPanel;
	public GameObject SearchPanel;
	public Texture SearchIcon;
	public GameObject SearchButt;
	public GameObject InstructPanel;
	public GameObject Background;
	public GameObject Change_Lang;
	public Transform Center;
	public Transform Empty;
	[Header ("Map Settings")]
	public GoogleApi MapCon;
	public float ZoomLevelA;
	public float ZoomLevelB;
	public string[] Sectors_Pos;
	public GameObject SectorB;
	public GameObject SectorA;
	public Texture Offline_A;
	public Texture Offline_B;

	public void ToggleSearch(){
		SearchPanel.SetActive (!(SearchPanel.activeInHierarchy));
	}

	void Close_n_Focus(bool state){
		TransitionBackground bgtrans = Background.GetComponent<TransitionBackground>();
		SearchButt.SetActive (!state);
		Plane.SetActive (!state);
		camcon.FocusPoint = (state)?(Empty):(Center);
		StartPanel.SetActive (state);
		Change_Lang.SetActive (state);
		if (state) {
			Background.SetActive (true);
			bgtrans.isComing = true;
		} else {
			bgtrans.isFading = true;
		}
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
			Center.position = new Vector3 (0f,0f,0f);
			Close_n_Focus(false);
			MapCon.UpdateMap ();
			break;

			//Sector B
		case 1:
			SectorB.SetActive (true);
			MapCon.Center = Sectors_Pos [1];
			MapCon.zoomLevel = ZoomLevelB;
			Center.position = new Vector3 (0f,0f,0f);
			Close_n_Focus(false);
			MapCon.UpdateMap ();
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
