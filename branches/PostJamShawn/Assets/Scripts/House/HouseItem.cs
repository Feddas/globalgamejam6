using UnityEngine;
using System.Collections;

public class HouseItem : HouseBaseObject {

	public HouseItemType type = HouseItemType.None;

	public override void Awake() {
	}

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public void OnMouseDown() {
		string debugText = TextLibrary.Instance.GetTextFor(type);
		if (string.IsNullOrEmpty(debugText))
		{
			debugText = this.gameObject.name + " was clicked.";
		}
		if (debugText != TextLibrary.CompletedDialog)
		{
			Debug.Log(debugText);
			showText(debugText);
		}

		Vector3 mousePosition = Input.mousePosition;
		Vector3 worldPosition = Camera.main.camera.ScreenToWorldPoint(mousePosition);

		//Debug.Log("calling player set target from house item");
		if(playerReference.currentState != PlayerState.InCinematic) {
			playerReference.SetTargetObjectAndTargetPosition(this.gameObject, worldPosition);
		}
	}

	bool newText;
	string textBoxText = "this is a test of the broad";
	private void showText(string text)
	{
		newText = true;
		this.textBoxText = text;
	}

	public void OnGUI() {
		if(newText) {
			var screenHeight = StartGameCinematic.textboxWidth;
			var textboxHeight = StartGameCinematic.textboxHeight;
			var textboxWidth = StartGameCinematic.textboxWidth;
			if(GUI.Button(new Rect(735, (screenHeight - textboxHeight) - 64, 64, 64), "Next")) {
				newText = false;
			}
			GUI.TextArea(new Rect(0, screenHeight - textboxHeight, textboxWidth, textboxHeight), textBoxText);
		}
	}
}
