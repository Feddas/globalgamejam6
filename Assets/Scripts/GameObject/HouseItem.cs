using UnityEngine;
using System.Collections;

public class HouseItem : MonoBehaviour {

	public HouseItemType type = HouseItemType.None;

	void Awake() { }
	void Start() {	}
	void Update() { }

	public void OnMouseDown() {
		string dialogText = TextLibrary.Instance.GetTextFor(type);
		if (string.IsNullOrEmpty(dialogText))
		{
			Debug.Log(this.gameObject.name + " was clicked.");
			return;
		}
		else if (dialogText != TextLibrary.CompletedDialog)
		{
			State.Instance.CurrentDialog = dialogText;

			if (State.Instance.GameDialog != null) //if it is null, it hasn't been closed yet. therefore, it's already visible.
				State.Instance.GameDialog.IsVisible = true;
		}
	}
}
