using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
	public GameObject StartPanel;
	public CamController camcon;
	public GoogleApi MapCon;
	public Transform Center;
	public Transform Empty;
	public string[] Sectors_Pos;
	void Close_n_Focus(bool state){
		StartPanel.SetActive (state);
		camcon.FocusPoint = (state)?(Empty):(Center);
	}

	public void Select_Sector(int SectorID){
		switch (SectorID) {
			//Sector A
		case 0:
			MapCon.Center = Sectors_Pos [0];
			MapCon.zoomLevel = 15;
			MapCon.UpdateMap ();
			Center.position = new Vector3 (0f,0f,0f);
			Close_n_Focus(false);
			break;

			//Sector B
		case 1:
			MapCon.Center = Sectors_Pos [1];
			MapCon.zoomLevel = 17;
			MapCon.UpdateMap ();
			Center.position = new Vector3 (0f,0f,0f);
			Close_n_Focus(false);
			break;
		}
	
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Close_n_Focus (true);
		}
	}
}
