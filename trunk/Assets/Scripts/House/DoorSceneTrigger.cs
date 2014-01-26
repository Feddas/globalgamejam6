using UnityEngine;
using System.Collections;

public class DoorSceneTrigger : MonoBehaviour {
	public Room currentScene;
	public Room sceneToChangeTo;

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
				Player.Instance.UpdatePlayerLocation(currentScene, sceneToChangeTo);
				Player.Instance.currentState = PlayerState.Standing;
				//USE THIS TO CONFIGURE THE PLAYERS BOUNDING FOR EACH SCENE BEING ENTERED
				switch(sceneToChangeTo) {
					case(Room.None):
					break;
					case(Room.Attic):
					break;
					case(Room.Bedroom):
					break;
					case(Room.Foyer):
						//Player.Instance.minWorldXBounding = -0.9f;
						//Player.Instance.maxWorldXBounding = -0.9f;
						Player.Instance.minWorldYBounding = -2f;
						Player.Instance.maxWorldYBounding = -4.9f;
					break;
					case(Room.FrontHouse):
					break;
					case(Room.Hallway):
						//Player.Instance.minWorldXBounding = -0.9f;
						//Player.Instance.maxWorldXBounding = -0.9f;
						Player.Instance.minWorldYBounding = -0.9f;
						Player.Instance.maxWorldYBounding = -3.9f;
						if(currentScene == Room.Attic) {
							Player.Instance.gameObject.transform.position = new Vector3(-0.1140994f, -2.71987f, Player.Instance.gameObject.transform.position.z);
							Player.Instance.targetPosition = new Vector2(-0.1140994f, -2.71987f);
						}
						else if(currentScene == Room.Bedroom) {
							Player.Instance.gameObject.transform.position = new Vector3(-0.1140994f, -2.71987f, Player.Instance.gameObject.transform.position.z);
							Player.Instance.targetPosition = new Vector2(-0.1140994f, -2.71987f);
						}
						else if(currentScene == Room.Masterbed) {
							Player.Instance.gameObject.transform.position = new Vector3(-0.1140994f, -2.71987f, Player.Instance.gameObject.transform.position.z);
							Player.Instance.targetPosition = new Vector2(-0.1140994f, -2.71987f);
						}
						else if(currentScene == Room.Foyer) {
							Player.Instance.gameObject.transform.position = new Vector3(-0.1140994f, -2.71987f, Player.Instance.gameObject.transform.position.z);
							Player.Instance.targetPosition = new Vector2(-0.1140994f, -2.71987f);
						}
					break;
					case(Room.LivingRoom):
					break;
					case(Room.Masterbed):
					break;
				}
				if(audio)
					audio.Play();
				Application.LoadLevel(sceneToChangeTo.ToString());
			}
		}
	}
}