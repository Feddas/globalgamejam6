using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class HouseItem : MonoBehaviour
{
	public HouseItemType HouseItemOf = HouseItemType.None;

	void Awake()
	{
		useUpdatedVisiblity();
	}

	void Start() { }

	void Update()
	{
		if (action != null)
			action();
	}

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

	#region completer effects
	private SpriteRenderer sprite;
	private Color color;
	private float weight, lerpFrom, lerpTo, lerpSpeed = 0.5f;
	private delegate void Action();
	private Action action;

	/// <summary> fade the alpha of this house item to completely opaque or transparent </summary>
	/// <param name="fadeDirection">-1 fades to completely transparent, 1 fades to completely opaque</param>
	public void Fade(int fadeDirection)
	{
		this.sprite = this.GetComponent<SpriteRenderer>();
		color = this.sprite.color;
		weight = 0;
		lerpFrom = color.a;
		lerpTo = (fadeDirection > 0 ? 1f : 0f); //set desired alpha value
		action = fade;

		if (this.HouseItemOf != HouseItemType.None)
		{
			Color newColor = this.color;
			newColor.a = lerpTo;
			if (State.Instance.NewItemColor.ContainsKey(this.HouseItemOf)) //overwrite it
				State.Instance.NewItemColor[this.HouseItemOf] = newColor;
			else
				State.Instance.NewItemColor.Add(this.HouseItemOf, newColor);
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