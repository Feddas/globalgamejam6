﻿using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Player.GetComponent<Player>().SetPlayerCurrentState(PlayerState.InCinematic);

		//if(Player.Instance == false) {
		//}
		Player.Instance.currentState = PlayerState.InCinematic;
		GameObject cinematicToPerformGameObject = new GameObject("Cinematic");
		cinematicToPerformGameObject.AddComponent<StartGameCinematic>();
		Player.Instance.cinematicToPerform = cinematicToPerformGameObject;
		//Keeps it alive so the player's cinematic isn't destroyed
		DontDestroyOnLoad(cinematicToPerformGameObject);

		//Debug.Log(Player.Instance.currentState);
		Application.LoadLevel("FrontHouse");
	}
	
	// Update is called once per frame
	void Update () {
	}
}