using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HouseItem : MonoBehaviour
{
	public HouseItemType HouseItemOf = HouseItemType.None;

	void Awake() { }
	void Start() { }
	void Update() { }

	public void OnMouseDown()
	{
		var interactLevel = Completer.CompletionSteps.Where(item => item.HouseItemRequired == HouseItemOf).FirstOrDefault();

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
}