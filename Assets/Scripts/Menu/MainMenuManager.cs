using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI() {
		GUIStyle gameTitleStyle = new GUIStyle();
		gameTitleStyle.fontSize = 64;
		gameTitleStyle.normal.textColor = Color.white;
		GUI.Label(new Rect(200, 150, 200, 200), "Broken Home", gameTitleStyle);

		if(GUI.Button(new Rect(325, 300, 150, 75), "Start")) {
			Player.Instance.currentState = PlayerState.InCinematic;
			GameObject cinematicToPerformGameObject = new GameObject("Cinematic");
			cinematicToPerformGameObject.AddComponent<StartGameCinematic>();
			Player.Instance.cinematicToPerform = cinematicToPerformGameObject;
			Player.Instance.transform.position = new Vector3(3.05f, -3, Player.Instance.transform.position.z);
			Player.Instance.targetPosition = new Vector2(3.05f, -3);
			//Keeps it alive so the player's cinematic isn't destroyed
			DontDestroyOnLoad(cinematicToPerformGameObject);
			
			//Debug.Log(Player.Instance.currentState);
			Application.LoadLevel("FrontHouse");
		}
		if(GUI.Button(new Rect(325, 375, 150, 75), "Credits")) {
			//Application.LoadLevel("FrontHouse");
		}
	}
}
