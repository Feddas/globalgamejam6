using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Completer : MonoBehaviour
{
	private HouseItem houseItem;

	public static IList<CompletionStep> CompletionSteps = new List<CompletionStep>()
	{
		new CompletionStep(Completion.Start, HouseItemType.LivingroomUrn, 1),
		new CompletionStep(Completion.PlacedUrn, HouseItemType.LivingroomFirePoker, -1),
		new CompletionStep(Completion.HaveFirePoker, HouseItemType.LivingroomPaintingSisters, 0),
		new CompletionStep(Completion.FixedPainting, HouseItemType.Cryptex1Livingroom, -1),
		new CompletionStep(Completion.HallwayFloorBoard, HouseItemType.Cryptex2Hallway, -1),
		new CompletionStep(Completion.CryptexPieces2, HouseItemType.MasterbedPillow1, 0),
	};

	void Awake()
	{
		this.houseItem = this.GetComponent<HouseItem>();

		setInteractionEffects();
	}
	
	void Start() { }
	void Update() { }

	/// <summary>
	/// Sets the ActionOnCompletion delegate which is used to show an effect such as a fade or rotation when a houseItem is interacted with
	/// </summary>
	private void setInteractionEffects()
	{
		//set the action for the steps referencing this HouseItem
		foreach (var myStep in CompletionSteps.Where(step => step.HouseItemRequired == this.houseItem.HouseItemOf))
		{
			switch (this.houseItem.HouseItemOf)
			{
			case HouseItemType.LivingroomUrn:
			case HouseItemType.LivingroomFirePoker:
			case HouseItemType.Cryptex1Livingroom:
			case HouseItemType.Cryptex2Hallway:
				myStep.ActionOnCompletion = this.houseItem.Fade;
				break;
			case HouseItemType.LivingroomPaintingSisters:
				myStep.ActionOnCompletion = paintingSisters;
				break;
			case HouseItemType.MasterbedPillow1:
				myStep.ActionOnCompletion = pillowPickup;
				break;
			default:
				throw new UnityException("Completer.cs/setInteractionEffects() does not have a case for " + this.houseItem.HouseItemOf);
			}
		}
	}

	public void OnMouseDown()
	{
		//Check if this HouseItemType fulfills the next completion step
		foreach (var completionStep in CompletionSteps)
		{
			if (State.Instance.Completed == completionStep.StepToStart
			    && houseItem.HouseItemOf == completionStep.HouseItemRequired)
			{
				State.Instance.Completed++; //step fulfilled, increment to next step
				completionStep.ActionOnCompletion(completionStep.ActionArgument);
				CompletionSteps.Remove(completionStep);
				return;
			}
		}
	}

	#region completer effects
	private void pillowPickup(int arg)
	{
		this.GetComponent<Animator>().SetTrigger("PillowPickup");

		//save end transform after animation for room re-entry. http://answers.unity3d.com/questions/454523/component-without-gameobject.html
		var objectForTransform = new GameObject() { name = "Position" + this.houseItem.HouseItemOf };
		DontDestroyOnLoad(objectForTransform);
		Transform newTransform = objectForTransform.transform;
		newTransform.position = new Vector3(8f, -1f, 0f);
		newTransform.localScale = new Vector3(-1f, 1f, 1f);

		if (State.Instance.NewItemTransform.ContainsKey(this.houseItem.HouseItemOf)) //overwrite it
			State.Instance.NewItemTransform[this.houseItem.HouseItemOf] = newTransform;
		else
			State.Instance.NewItemTransform.Add(this.houseItem.HouseItemOf, newTransform);
	}

	private void paintingSisters(int arg)
	{
		this.houseItem.Fade(0); //hide the rotated painting

		//Show the children, CryptexPiece1 and the straightened painting
		FadeInChildren(this.transform);
	}

	public static void FadeInChildren(Transform parentTransform)
	{
		foreach (Transform child in parentTransform)
		{
			var childHouseItem = child.GetComponent<HouseItem>();
			childHouseItem.Fade(1);
		}
	}
	#endregion completer effects
}