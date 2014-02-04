using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HouseItem : MonoBehaviour
{
	public HouseItemType type = HouseItemType.None;

	/// <summary> When each item can be interacted with </summary>
	IList<Tuple<HouseItemType,Completion>> CanInteract = new List<Tuple<HouseItemType,Completion>>()
	{
		Tuple.Create(HouseItemType.MasterbedMirrorShard, Completion.CyrptexPieces3),
		Tuple.Create(HouseItemType.MasterbedPillow1, Completion.CyrptexPieces3),
		Tuple.Create(HouseItemType.MasterbedPillow2, Completion.CyrptexPieces3),
		Tuple.Create(HouseItemType.LivingroomCodex, Completion.CyrptexPieces3),
		Tuple.Create(HouseItemType.LivingroomFirePoker, Completion.CyrptexPieces3),
		Tuple.Create(HouseItemType.LivingroomFireplaceMantle, Completion.CyrptexPieces3),
		Tuple.Create(HouseItemType.LivingroomPaintingSisters, Completion.PlacedUrn),
		Tuple.Create(HouseItemType.FoyerMirror, Completion.None),
	};

	void Awake() { }
	void Start() { }
	void Update() { }

	public void OnMouseDown()
	{
		var interactLevel = CanInteract.Where(item => item.Item1 == type).FirstOrDefault();
		if (interactLevel != null && interactLevel.Item2 <= State.Instance.Completed)
		{
			State.Instance.ItemToInteract = type;
		}
		else
		{
			updateDialog();
		}
	}

	private void updateDialog()
	{
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