using UnityEngine;
using System.Collections;

public class DoorTaken : MonoBehaviour
{
	public Room SceneCurrent;
	public Room SceneToChangeTo;
	
	public void OnMouseDown()
	{
		if(audio)
			audio.Play();

		State.Instance.SceneLast = SceneCurrent;
		State.Instance.SceneCurrent = SceneToChangeTo;
		Application.LoadLevel(SceneToChangeTo.ToString());
	}
}