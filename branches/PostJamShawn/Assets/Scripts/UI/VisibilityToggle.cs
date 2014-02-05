using UnityEngine;
using System.Collections;

//http://www.daikonforge.com/dfgui/forums/topic/simple-toggle-showhide/
public class VisibilityToggle : MonoBehaviour
{
	public dfControl targetControl;

	void Awake() { }

	public void OnClick()
	{
		targetControl.IsVisible = !targetControl.IsVisible;
	}
}