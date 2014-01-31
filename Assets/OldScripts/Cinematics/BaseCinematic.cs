using UnityEngine;
using System.Collections;

public class BaseCinematic : MonoBehaviour {
	public bool startedCinematic = false;
	public bool finishedCinematic = false;

	// Use this for initialization
	public virtual void Start () {
	}
	
	// Update is called once per frame
	public virtual void Update () {
		if(!startedCinematic) {
			startedCinematic = true;
			StartCoroutine("PerformCinematicLogic");
		}
		if(finishedCinematic) {
			Player.Instance.SetPlayerCurrentState(PlayerState.Standing);
		}
	}

	//Created so that all cinematics can have access to the co-routine
	public virtual IEnumerator PerformCinematicLogic() {
		yield return null;
	}
}
