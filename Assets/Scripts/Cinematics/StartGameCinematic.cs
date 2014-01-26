using UnityEngine;
using System.Collections;

public class StartGameCinematic : BaseCinematic {
	public bool playerClickedNext = false;

	public bool displayTextAreaText = false;

	public int textboxWidth = 1024;
	public int textboxHeight = 100;
	public int screenWidth = 1024;
	public int screenHeight = 599;

	public string textBoxText = "The quick brown fox jumped over the log";

	// Use this for initialization
	public override void Start () {
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public void OnGUI() {
		if(displayTextAreaText) {
			//Debug.Log("GUI Being Drawn");
			if(GUI.Button(new Rect(735, (screenHeight - textboxHeight) - 64, 64, 64), "Next")) {
				playerClickedNext = true;
			}
			GUI.TextArea(new Rect(0, screenHeight - textboxHeight, textboxWidth, textboxHeight), textBoxText);
		}
	}

	//Created so that all cinematics can have access to the co-routine
	public override IEnumerator PerformCinematicLogic() {
		//yield return new WaitForSeconds(10);
		//yield finishedCinematic = true;

		//THIS WILL NEED TO BE THE FINAL CINEMATIC BEFORE THE CINEMATIC ENDS
		//THIS IS BECAUSE UNITY IS SO FREAKING STUPID WITH CO-ROUTINES, AND THE
		//ONLY WAY TO CHAIN THEM IS IN REVERSE WHILE YOU WAIT FOR EACH CALL TO
		//EXECUTE
		yield return StartCoroutine(ShowSecondParagraph());
//		if(playerClickedNext) {
//			yield return StartCoroutine(ShowFirstParagraph());
//		}
	}

	public IEnumerator ShowFirstParagraph() {
		//yield return new WaitForSeconds(10);
		textBoxText = "It’s been so long since I’ve been home, I hardly remember the place... But Dad wanted his ashes placed on the fireplace mantle. \n\nI’ll just put them there and leave. \n\nStrange to have an unfinished inscription on the urn. All it says is YOU ARE.";
		displayTextAreaText = true;
		//Debug.Log("Waiting");

		while(!playerClickedNext) {
			yield return null;
		}
		if(playerClickedNext) {
			//Debug.Log("Player clicked next");
		}
	}

	public IEnumerator ShowSecondParagraph() {
		yield return StartCoroutine(ShowFirstParagraph());

		textBoxText = "herp herp herp";
		displayTextAreaText = true;
		//Debug.Log("Second paragraph");

		finishedCinematic = true;
	}
//	IEnumerator Func2()
//	{
//		yield return StartCoroutine( Func1() );
//	}
}
