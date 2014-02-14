using UnityEngine;
using System.Collections;

public class CyrptexBox : MonoBehaviour
{
	public dfPanel Next;

	void Start()
	{
		Next.IsVisible = false;
		CyrptexBox.PanelToFade = this.GetComponent<dfPanel>();
		Fade(1);
	}

	void Update()
	{
		if (CyrptexBox.FadeAction != null)
			CyrptexBox.FadeAction();

		//Fade in TumblerLarge which should be held in Next var
		else if (FadeAction == null && Next != null && Next.BackgroundColor.a < (byte)200)
		{
			CyrptexBox.PanelToFade = Next.GetComponent<dfPanel>();
			Next.IsVisible = true;
			Fade(1);
		}
	}

	#region completer effects (NOTE: this region is very similiar to the region in HouseItem.cs)
	public static dfPanel PanelToFade;
	public delegate void Action();
	public static Action FadeAction;

	private static Color32 color;
	private static float weight, lerpSpeed = 0.5f;
	private static byte lerpFrom, lerpTo;
	
	/// <summary> fade the alpha to completely opaque or transparent </summary>
	/// <param name="fadeDirection">-1 fades to completely transparent, 1 fades to completely opaque</param>
	public static void Fade(int fadeDirection)
	{
		color = CyrptexBox.PanelToFade.BackgroundColor;
		weight = 0;
		lerpFrom = color.a;
		lerpTo = (fadeDirection > 0 ? (byte)255 : (byte)0); //set desired alpha value
		FadeAction = fade;

		Color newColor = CyrptexBox.color;
		newColor.a = lerpTo;
	}
	
	private static void fade()
	{
		//increment the fade
		CyrptexBox.weight += Time.deltaTime * lerpSpeed;
		CyrptexBox.color.a = (byte)Mathf.Lerp(lerpFrom, lerpTo, weight);
		CyrptexBox.PanelToFade.BackgroundColor = CyrptexBox.color;
		
		//end the fade
		if (weight > 0.99f)
		{
			FadeAction = null;
		}
	}
	#endregion completer effects
}