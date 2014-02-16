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
		new CompletionStep(Completion.HaveFirePoker, HouseItemType.LivingroomPaintingSisters, -1),
		new CompletionStep(Completion.FixedPainting, HouseItemType.Cryptex1Livingroom, -1),
		new CompletionStep(Completion.HallwayFloorBoard, HouseItemType.Cryptex2Hallway, -1),
		new CompletionStep(Completion.Cryptex2Hallway, HouseItemType.MasterbedPillow1, 0),
		new CompletionStep(Completion.PillowsPlaced, HouseItemType.Cryptex3Masterbed, -1),
		new CompletionStep(Completion.Cryptex3Masterbed, HouseItemType.MasterbedMirrorShard, -1),
		new CompletionStep(Completion.HaveMirrorShard, HouseItemType.FoyerMirror, 1),
		new CompletionStep(Completion.MirrorShardUsed, HouseItemType.Cryptex4MirrorShard, -1),
		new CompletionStep(Completion.Cryptex4MirrorShard, HouseItemType.AtticRockingHorse, 1),
		new CompletionStep(Completion.BunnyFell, HouseItemType.Cryptex5Attic, -1),
		new CompletionStep(Completion.Cryptex5Attic, HouseItemType.LivingroomCodexBox, 7),
	};

	void Awake()
	{
		//toggle-able debug code below
//		if (State.Instance.Completed < Completion.CryptexPieces5)
//			State.Instance.Completed = Completion.CryptexPieces5;

		this.houseItem = this.GetComponent<HouseItem>();

		setInteractionEffects();
		
		//complete on scene change
		if (this.houseItem.HouseItemOf == HouseItemType.Cryptex2Hallway
			&& State.Instance.Completed == Completion.HallwayFloorBoard)
		{
			this.houseItem.Fade(1);
		}
		else if (this.houseItem.HouseItemOf == HouseItemType.AtticBunny
			&& State.Instance.Completed == Completion.StoppedRockingHorse)
		{
			this.GetComponent<Animator>().SetTrigger("MakeBunnyFall");
		}
	}
	
	void Start() { }
	void Update() { }

	/// <summary>
	/// Sets the ActionOnCompletion delegate which is used to show an effect such as a fade or rotation when a houseItem is interacted with
	/// </summary>
	private void setInteractionEffects()
	{
		//set the type of effect (also ensures referencing of the correct instance of HouseItem)
		foreach (var myStep in CompletionSteps.Where(step => step.HouseItemRequired == this.houseItem.HouseItemOf))
		{
			switch (this.houseItem.HouseItemOf)
			{
			case HouseItemType.LivingroomUrn:
			case HouseItemType.LivingroomFirePoker:
			case HouseItemType.Cryptex1Livingroom:
			case HouseItemType.Cryptex2Hallway:
			case HouseItemType.Cryptex3Masterbed:
			case HouseItemType.MasterbedMirrorShard:
			case HouseItemType.Cryptex4MirrorShard:
			case HouseItemType.Cryptex5Attic:
				myStep.ActionOnCompletion = this.houseItem.Fade;
				break;
			case HouseItemType.LivingroomPaintingSisters:
			case HouseItemType.FoyerMirror:
				myStep.ActionOnCompletion = fadeInChildren;
				break;
			case HouseItemType.MasterbedPillow1:
				myStep.ActionOnCompletion = pillowPickup;
				break;
			case HouseItemType.AtticRockingHorse:
				myStep.ActionOnCompletion = killAnimator;
				break;
			case HouseItemType.LivingroomCodexBox:
				myStep.ActionOnCompletion = loadScene;
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
				completeStep(completionStep);
				CompletionSteps.Remove(completionStep);
				return;
			}
		}
	}

	private void completeStep(CompletionStep completionStep)
	{
		completeStep(completionStep.ActionOnCompletion, completionStep.ActionArgument);
	}

	private void completeStep(CompletionStep.Action action, int actionArg)
	{
		State.Instance.Completed++; //step fulfilled, increment to next step
		action(actionArg);
	}

	#region completer effects
	private void pillowPickup(int arg)
	{
		this.GetComponent<Animator>().SetTrigger("PillowPickup");

		//The pillow hasn't reached it's final position yet, it's manually put in here. This could be refactored to be called from the dopesheet after the animation is done. just need to find out if it still happens if they leave the room before the animation is complete.
		Transform newTransform = new GameObject().transform;
		newTransform.position = new Vector3(8f, -1f, 0f);
		newTransform.localScale = new Vector3(-1f, 1f, 1f);
		newTransform = newPersistantTransform(newTransform, this.houseItem.HouseItemOf);

		if (State.Instance.NewItemTransform.ContainsKey(this.houseItem.HouseItemOf)) //overwrite it
			State.Instance.NewItemTransform[this.houseItem.HouseItemOf] = newTransform;
		else
			State.Instance.NewItemTransform.Add(this.houseItem.HouseItemOf, newTransform);
	}

	/// <summary> This is called from the Bunny Animation DopeSheet in the Unity Editor </summary>
	private void BunnyFall()
	{
		completeStep(killAnimator, 1);
	}

	/// <summary>
	/// Sets the animator component on this houseItem to be destroyed.
	/// </summary>
	/// <param name="arg">if set to 1, Animator is destroyed immediately, rather than on scene reload.</param>
	private void killAnimator(int arg)
	{
		if (arg == 1)
		{
			//Animator locks the postion even when "Apply Root Motion" is unchecked
			Destroy(this.GetComponent<Animator>());
		}

		//Save final Position
		Transform finalPosition = newPersistantTransform(this.transform, this.houseItem.HouseItemOf);
		if (State.Instance.NewItemTransform.ContainsKey(this.houseItem.HouseItemOf)) //overwrite it
			State.Instance.NewItemTransform[this.houseItem.HouseItemOf] = finalPosition;
		else
			State.Instance.NewItemTransform.Add(this.houseItem.HouseItemOf, finalPosition);
	}

	private void loadScene(int buildIndex)
	{
		Application.LoadLevel(buildIndex);
	}

	/// <summary>
	/// save transform after animation for room re-entry. http://answers.unity3d.com/questions/454523/component-without-gameobject.html
	/// </summary>
	/// <returns>The persistant transform.</returns>
	/// <param name="basedOffOf">Based off of.</param>
	/// <param name="itemType">Item type.</param>
	private Transform newPersistantTransform(Transform basedOffOf, HouseItemType itemType)
	{
		var objectForTransform = new GameObject() { name = "Position" + itemType };
		DontDestroyOnLoad(objectForTransform); //make transform persist accross scenes
		objectForTransform.transform.position = basedOffOf.position;
		objectForTransform.transform.rotation = basedOffOf.rotation;
		objectForTransform.transform.localScale = basedOffOf.localScale;
		return objectForTransform.transform;
	}

	private void fadeInChildren(int fadeDirection)
	{
		this.houseItem.Fade(fadeDirection); //hide the rotated painting

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