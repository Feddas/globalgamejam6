using UnityEngine;
using System.Collections;

public class HouseItem : HouseBaseObject {

	public HouseItemType type = HouseItemType.None;

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
			Debug.Log(debugText);

		Vector3 mousePosition = Input.mousePosition;
		Vector3 worldPosition = Camera.main.camera.ScreenToWorldPoint(mousePosition);

		playerReference.SetTargetObjectAndTargetPosition(this.gameObject, worldPosition);
	}
}
