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
				&& State.Instance.Completed == Completion.CryptexPieces1)
			State.Instance.Completed = Completion.HallwayFloorBoard;
		else if (SceneCurrent == Room.Hallway
				&& State.Instance.Completed == Completion.HallwayFloorBoard)
			Completer.FadeInChildren(this.transform); //Show CryptexPiece2
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