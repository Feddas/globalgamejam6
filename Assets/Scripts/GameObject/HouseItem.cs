using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HouseItem : MonoBehaviour
{
	public HouseItemType HouseItemOf = HouseItemType.None;

	void Awake() { useUpdatedVisiblity(); }
	void Start() { }
	void Update() { }

	public void OnMouseDown()
	{
		var interactLevel = Completer.CompletionSteps
			.Where(item => item.HouseItemRequired == HouseItemOf)
			.FirstOrDefault();

		//Walk to the item and grab it
		if (interactLevel != null && interactLevel.StepToStart == State.Instance.Completed)
		{
			State.Instance.ItemToInteract = HouseItemOf;
		}

		//Don't walk, just update the dialog text
		else
		{
			TextLibrary.Instance.UpdateDialog(HouseItemOf);
		}
	}
	
	/// <summary> returns a houseitem to the alpha it was set to after a player interaction set from State.Instance.NewItemColor.Add(). so items don't regress to a previous state after switching rooms </summary>
	private void useUpdatedVisiblity()
	{
		var newVisibility = State.Instance.NewItemColor
			.Where(item => item.Key == this.HouseItemOf)
			.FirstOrDefault().Value;
		if (newVisibility.HasValue)
		{
			this.GetComponent<SpriteRenderer>().color = newVisibility.Value;
		}
	}
}