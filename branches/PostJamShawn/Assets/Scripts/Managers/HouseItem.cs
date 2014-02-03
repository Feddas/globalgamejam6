using UnityEngine;
using System.Collections;

public class HouseItem : MonoBehaviour {

	public HouseItemType type = HouseItemType.None;

	void Awake() { }
	void Start() {	}
	void Update() { }

	public void OnMouseDown() {
		string debugText = TextLibrary.Instance.GetTextFor(type);
		if (string.IsNullOrEmpty(debugText))
		{
			debugText = this.gameObject.name + " was clicked.";
		}
		if (debugText != TextLibrary.CompletedDialog)
		{
			State.Instance.CurrentDialog = debugText;
			Debug.Log(debugText);

			if (State.Instance.GameDialog != null) //if it is null, it hasn't been closed yet. therefore, it's already visible.
				State.Instance.GameDialog.IsVisible = true;
		}
	}
}
