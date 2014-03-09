using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour
{
	public GameObject FrontDoor;

	void Start()
	{
		if (State.Instance.Completed == Completion.None)
		{
			var startMenu = this.GetComponent<dfPanel>();
			startMenu.IsVisible = true;
			FrontDoor.SetActive(false);
		}
	}

	void Update() { }

	public void OnClick( dfControl control, dfMouseEventArgs mouseEvent )
	{
		if (control.name == "Start")
		{
			State.Instance.StartTime = System.DateTime.Now.Ticks;

			#if UNITY_WEBPLAYER
			kongregate();
			#endif //UNITY_WEBPLAYER

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

	#if UNITY_WEBPLAYER
	/// <summary>
	/// Copied from http://www.kongregate.com/developer_center/docs/en/using-the-api-with-unity3d
	/// with some help from https://www.youtube.com/watch?v=FW4QRsJx7Wg
	/// </summary>
	private void kongregate()
	{
		if (string.IsNullOrEmpty(State.Instance.KongregateUserInfo))
		{
			// Begin the API loading process if it is available
			Application.ExternalEval(
				"if(typeof(kongregateUnitySupport) != 'undefined'){" +
				" kongregateUnitySupport.initAPI('" + this.name + "', 'OnKongregateAPILoaded');" +
				"};"
			);
		}
	}

	private void OnKongregateAPILoaded(string userInfoString)
	{
		// We now know we're on Kongregate
		//isKongregate = true;

		State.Instance.KongregateUserInfo = userInfoString;
		Application.ExternalCall("kongregate.stats.submit", "Loaded", 1);
	}
	#endif //UNITY_WEBPLAYER
}