using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
	public GameObject FrontDoor;

	void Start() { }
	void Update() { }

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		if (control.name == "Start")
		{
			State.Instance.Completed = Completion.Start;
			FrontDoor.SetActive(true);
			var startMenu = this.GetComponent<dfPanel>();
			startMenu.IsVisible = false;
		}
		else if (control.name == "Trailer")
		{
			//May want to try richtext hyperlink http://www.daikonforge.com/dfgui/forums/topic/rich-text-anchors-as-buttons/
			Application.OpenURL("https://drive.google.com/file/d/0B4RQwHQLBksVTlR6d0JOWVBZa2c/edit?usp=sharing");
		}
		else if (control.name == "Exit")
		{
			Application.Quit();
		}
	}
}