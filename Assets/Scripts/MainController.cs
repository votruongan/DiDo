using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour {
	public GameObject StartPanel;

	void Select_Sector(int SectorID){
		switch (SectorID) {
			//Sector A
		case 0:
			StartPanel.SetActive (false);		
			break;

			//Sector B
		case 1:
			StartPanel.SetActive (false);

			break;
		}
	
	}

}
