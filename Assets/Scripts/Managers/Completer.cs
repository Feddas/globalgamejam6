using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Completer : MonoBehaviour
{
	private HouseItemType houseItem;

	private SpriteRenderer sprite;
	private Color color;
	private float weight, lerpFrom, lerpTo, lerpSpeed = 0.3f;
	private delegate void Action();
	private Action action;
	public static IList<CompletionStep> CompletionSteps = new List<CompletionStep>()
	{
		new CompletionStep(Completion.Start, HouseItemType.LivingroomUrn, 1),
		new CompletionStep(Completion.PlacedUrn, HouseItemType.LivingroomFirePoker, -1),
		new CompletionStep(Completion.HaveFirePoker, HouseItemType.LivingroomPaintingSisters, -1),
	};

	void Awake()
	{
		this.houseItem = this.GetComponent<HouseItem>().HouseItemOf;
		this.sprite = this.GetComponent<SpriteRenderer>();

		setInteractionEffects();
	}

	private void setInteractionEffects()
	{
		//set the action for the steps referencing this HouseItem
		foreach (var myStep in CompletionSteps.Where(step => step.HouseItemRequired == this.houseItem))
		{
			switch (this.houseItem)
			{
			case HouseItemType.LivingroomUrn:
			case HouseItemType.LivingroomFirePoker:
			default:
				myStep.ActionOnCompletion = fadeInit;
				break;
			case HouseItemType.LivingroomPaintingSisters:
				myStep.ActionOnCompletion = paintingSisters;
				break;
			}
		}
	}

	void Start() { }
	void Update()
	{
		if (action != null)
			action();
	}

	public void OnMouseDown()
	{
		//Check if this HouseItemType fulfills the next completion step
		foreach (var completionStep in CompletionSteps)
		{
			if (State.Instance.Completed == completionStep.StepToStart
			    && houseItem == completionStep.HouseItemRequired)
			{
				State.Instance.Completed++; //step fulfilled, increment to next step
				completionStep.ActionOnCompletion(completionStep.ActionArgument);
				CompletionSteps.Remove(completionStep);
				return;
			}
		}
	}

	#region completer effects
	private void paintingSisters(int arg)
	{
		foreach (Transform child in this.transform)
		{
			this.sprite = child.GetComponent<SpriteRenderer>(); //this is CryptexPiece1
			fadeInit(1);
		}
	}

	private void fadeInit(int fadeDirection)
	{
		color = this.sprite.color;
		weight = 0;
		lerpFrom = color.a;
		lerpTo = (fadeDirection > 0 ? 1f : 0f); //set desired alpha value
		action = fade;

		var fadedItem = this.sprite.GetComponent<HouseItem>().HouseItemOf;
		if (fadedItem != HouseItemType.None)
		{
			Color newColor = this.color;
			newColor.a = lerpTo;
			//Debug.Log(fadedItem + " added to NewItemColor " + newColor.a);
			State.Instance.NewItemColor.Add(fadedItem, newColor);
		}
	}

	private void fade()
	{
		//increment the fade
		this.weight += Time.deltaTime * lerpSpeed;
		this.color.a = Mathf.Lerp(lerpFrom, lerpTo, weight);
		this.sprite.color = this.color;

		//end the fade
		if (weight > 0.98f)
		{
			action = null;
		}
	}
	#endregion completer effects
}

//public class CompleterEffects()
//{
//	public Color ColorRenderer { get; set; }
//}