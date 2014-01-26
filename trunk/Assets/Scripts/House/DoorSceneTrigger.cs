using UnityEngine;
using System.Collections;

public class DoorSceneTrigger : MonoBehaviour {
	public string sceneToChangeTo = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D otherCollider) {
		//Debug.Log("Door change detected.");
		if(otherCollider.gameObject.GetComponent<Player>() != null) {
			if(!sceneToChangeTo.Equals(string.Empty)) {
				Application.LoadLevel(sceneToChangeTo);
			}
		}
	}
}