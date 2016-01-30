﻿using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	int displace = 50;
	int boxW = 100;
	int boxH = 30;

	void OnGUI () {
		// Make a background box
		//GUI.skin.label.font = GUI.skin.button.font = GUI.skin.box.font = Font;
		GUI.skin.box.fontSize = 20;
		GUI.Box(new Rect(displace, displace - displace / 5, Screen.width - displace * 2, 
			Screen.height - (displace * 2 - (displace /5))), "Main Menu");

		if(GUI.Button(new Rect(Screen.width / 2 - (boxW / 2), Screen.height - displace * (1 + 3), boxW, boxH), "Start Game")) {
			Application.LoadLevel("Level 1");
		}
			
		if(GUI.Button(new Rect(Screen.width / 2 - (boxW / 2), Screen.height - displace * (1 + 2), boxW, boxH), "Instructions")) {
			Application.LoadLevel("instructions");
		}

		if(GUI.Button(new Rect(Screen.width / 2 - (boxW / 2), Screen.height - displace * (1 + 1), boxW, boxH), "Quit Game")) {
			Application.Quit ();
		}
	}
}