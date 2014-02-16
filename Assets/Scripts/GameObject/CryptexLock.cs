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
			//prevent win being called multiple times
			selectedIndices = new int[5];

			win();
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

	private void win()
	{
		//show photo
		CyrptexBox.PanelToFade = Photo.GetComponent<dfPanel>();
		Photo.IsVisible = true;
		CyrptexBox.Fade(1);

		#if UNITY_WEBPLAYER
		kongregateWin();
		#endif //UNITY_WEBPLAYER
	}
	
	#if UNITY_WEBPLAYER
	private void kongregateWin()
	{
		//check if kongregate failed to load
		if (string.IsNullOrEmpty(State.Instance.KongregateUserInfo))
			return;

//		var kongregateParams = State.Instance.KongregateUserInfo.Split('|');
//		int userId;
//		int.TryParse(kongregateParams[0], out userId);
//		string username = kongregateParams[1];
//		string gameAuthToken = kongregateParams[2];

		// Begin the API loading process if it is available
		Application.ExternalCall("kongregate.stats.submit","GameComplete",100);
	}
	#endif //UNITY_WEBPLAYER
}