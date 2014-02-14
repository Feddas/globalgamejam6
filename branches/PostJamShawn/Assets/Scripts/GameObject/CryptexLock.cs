using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CryptexLock : MonoBehaviour 
{
	public dfPanel Photo;

	private int[] selectedIndices = new int[5];
	
	void Update()
	{
	}

	public void OnSelectedIndexChanged( dfControl control, int value )
	{
		int controlIndex;
		if (int.TryParse(control.name.Substring(7), out controlIndex) == false)
		    return;

		selectedIndices[controlIndex] = value;

		if (asString(selectedIndices) == "LOVED")
		{
			//prevent if block from being called multiple times
			selectedIndices = new int[5];

			//show photo
			CyrptexBox.PanelToFade = Photo.GetComponent<dfPanel>();
			Photo.IsVisible = true;
			CyrptexBox.Fade(1);
		}
	}

	private string asString(int[] array)
	{
		string result = "";
		foreach (var integer in array)
		{
			result += (char)(integer + 65);
		}
		return result;
	}
}