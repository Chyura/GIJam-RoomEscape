﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PortalScript : MonoBehaviour {

	float pRotate = 0.0f;
	Vector3 RotationVector = Vector3.zero;

	bool ZoomIn = false;
	Camera MainCamera;
	public float ZoomSpeed;


	public GameObject FadeBackground;
	float FadeIn = 0.0f;
	float FadeColour = 0.0f;
	bool isFadingIn = false;
	bool isFadingOut = false;

	public GameObject Timer; 
	public GameObject LevelText;

	void Awake(){

		gameObject.SetActive (false);
	}


	// Use this for initialization
	void Start () {
		RotationVector  = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y,transform.eulerAngles.z);
		MainCamera = GameObject.FindWithTag ("MainCamera").GetComponent<Camera>();
		FadeBackground = GameObject.FindWithTag ("Fade");

	}
	
	// Update is called once per frame
	void Update () {
	

		pRotate += 2.0f;
		RotationVector.Set (transform.eulerAngles.x, transform.eulerAngles.y,pRotate);
		transform.eulerAngles = RotationVector;

		if (pRotate >= 360) {
			pRotate = 0;
		}

		if (ZoomIn == true) {
			if (MainCamera.orthographicSize >= 1.0f) {
				MainCamera.orthographicSize -= ZoomSpeed;
				if (isFadingIn == false || FadeColour == 1.0f) {
					FadeColour = 0.0f;
					FadeIn = 0.0f;
					isFadingIn = true;
					FadeBackground.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
				}
			}
		}

		if (isFadingIn) {
			if (FadeColour == 0.0f) {
				Timer.SetActive (false);
				LevelText.SetActive (false);

			}

			if (FadeIn + 0.015f < 1.0f){
				FadeIn += 0.02f;

				if (FadeColour == 1.0f){
					FadeIn += 0.02f;
				}


				FadeBackground.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
			}
			else{
				isFadingIn = false;

				if (FadeColour == 0.0f && PlayerScript.Dead == false) {
					GameManager.Level++;
					GameManager.FinishedRound = false;
					PlayerScript.Dead = false;
					GameManager.CashIncrease += GameManager.CashIncreaseIncrease;

					ZoomIn = false;

					GameManager.TotalCash += GameManager.CashIncrease;
					GameManager.Cash = GameManager.TotalCash;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
				} else if (FadeColour == 0.0f && PlayerScript.Dead == true && GameManager.FinishedRound == false) {
					GameManager.Cash = GameManager.TotalCash;
					GameManager.FinishedRound = false;
					ZoomIn = false;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					PlayerScript.Dead = false;
				} else if (FadeColour == 0.0f) {
					GameManager.Cash = GameManager.TotalCash;
					GameManager.FinishedRound = false;
					ZoomIn = false;
					SceneManager.LoadScene(SceneManager.GetActiveScene().name);
					PlayerScript.Dead = false;
				}


				if (FadeColour == 1.0f){
					CutsceneFadeOut ("White");	
				} 

			}

		}

		if (isFadingOut) {

			if (FadeIn - 0.015f > 0){
				FadeIn -= 0.02f;
				FadeBackground.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
			}
			else{
				isFadingOut = false;
				FadeBackground.GetComponent<GUITexture>().enabled = false;

			}
		}


	}



	public void CutsceneFadeIn (string Colour ){
		if (FadeBackground == null) {
			FadeBackground = GameObject.FindWithTag ("Fade");
		}

			FadeBackground.GetComponent<GUITexture> ().enabled = true;
			isFadingIn = true;
			isFadingOut = false;
			FadeIn = 0.0f;
			if (Colour == "Black") {
				FadeColour = 0.0f;

			} else if (Colour == "White") {
				FadeColour = 1.0f;
			}
	 
		FadeBackground.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
	}
	public void CutsceneFadeOut (string Colour ){
		FadeBackground.GetComponent<GUITexture>().enabled = true;
		isFadingIn = false;
		isFadingOut = true;
		FadeIn = 1.0f;
		if (Colour == "Black") {
			FadeColour = 0.0f;

		} else if (Colour == "White") {
			FadeColour = 1.0f;
		}
		FadeBackground.GetComponent<GUITexture>().color = new Color (FadeColour, FadeColour, FadeColour,FadeIn);
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.tag == "Player") {
			if (ZoomIn == false) {

				ZoomIn = true;
				CutsceneFadeIn ("Black");
			}


		}

	}
	public bool isFading (){
		return isFadingIn;
	}



}
