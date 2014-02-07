using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HouseItem : MonoBehaviour
{
	public HouseItemType HouseItemOf = HouseItemType.None;

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
		var interactLevel = CanInteract.Where(item => item.Item1 == HouseItemOf).FirstOrDefault();
		if (interactLevel != null && interactLevel.Item2 <= State.Instance.Completed)
		{
			State.Instance.ItemToInteract = HouseItemOf;
		}
		else
		{
			TextLibrary.Instance.UpdateDialog(HouseItemOf);
		}
	}
}