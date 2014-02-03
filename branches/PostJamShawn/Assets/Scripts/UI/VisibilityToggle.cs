using UnityEngine;
using System.Collections;

//http://www.daikonforge.com/dfgui/forums/topic/simple-toggle-showhide/
public class VisibilityToggle : MonoBehaviour
{
	public dfControl targetControl;
	
	public void OnClick()
	{
		targetControl.IsVisible = !targetControl.IsVisible;

		if (State.Instance.GameDialog == null)
			State.Instance.GameDialog = targetControl;
	}
}