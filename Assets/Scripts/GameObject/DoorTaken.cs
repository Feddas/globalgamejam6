using UnityEngine;
using System.Collections;

public class DoorTaken : MonoBehaviour
{
	public Room SceneCurrent;
	public Room SceneToChangeTo;
	public Completion ActivatesOn;
	
	void Awake()
	{
		if (State.Instance.Completed < this.ActivatesOn)
			this.gameObject.SetActive(false);

		//Handle bedroom and hallway floor boards completion step
		else if (SceneCurrent == Room.Bedroom
				&& State.Instance.Completed == Completion.Cryptex1Livingroom)
			State.Instance.Completed = Completion.HallwayFloorBoard;
	}

	public void OnMouseDown()
	{
		if(audio)
			audio.Play();

		State.Instance.SceneLast = SceneCurrent;
		State.Instance.SceneCurrent = SceneToChangeTo;
		Application.LoadLevel(SceneToChangeTo.ToString());
	}
}