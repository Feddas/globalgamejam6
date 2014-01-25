using UnityEngine;
using System.Collections;

public class Player : TargetPathingGameObject {
	public bool arrivedAtTargetPosition = false;
	public PlayerState currentState = PlayerState.None;
	
	//------------------------------------------------------
	//Beginning of Singleton Declaration
	//------------------------------------------------------
	private static volatile Player _instance;

	private static object _lock = new object();
	
	//Stops the lock being created ahead of time if it's not necessary
	static Player() {
	}
	
	public static Player Instance {
		get {
			if (_instance == null) {
				lock(_lock) {
					if (_instance == null) 
						_instance = ((GameObject)GameObject.Instantiate(Resources.Load("Prefabs/Player") as GameObject)).GetComponent<Player>();
						DontDestroyOnLoad(Player.Instance.gameObject);
				}
			}
			return _instance;
		}
	}
	
	private Player() {
	}
	//End of Singleton Declaration
	//------------------------------------------------------

//	// Use this for initialization
//	public override void Start () {
//	}

	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public void PerformPlayerLogic() {
		switch(currentState) {
			case(PlayerState.None):
			break;
			case(PlayerState.BendingOver):
			break;
			case(PlayerState.InCinematic):
			break;
			case(PlayerState.Flashback):
			break;
			case(PlayerState.Moving):
				PerformPlayerArrivalLogic();
			break;
			case(PlayerState.ReachingOut):
			break;
			case(PlayerState.ReachingUp):
			break;
			case(PlayerState.Standing):
			break;
		}
	}
	public void PerformPlayerArrivalLogic() {
		if(this.gameObject.transform.position.x == targetPosition.x
		   && this.gameObject.transform.position.y == targetPosition.y) {
			if(!arrivedAtTargetPosition) {
				arrivedAtTargetPosition = true;
				currentState = PlayerState.Standing;
			}
		}
	}

	public override void SetTargetObjectAndTargetPosition(GameObject newTargetObject, Vector2 newTargetPosition) {
		if(useWorldBoundings) {
			//Left
			float widthOffset = this.gameObject.transform.localScale.x/2;
			float heightOffset = this.gameObject.transform.localScale.y/2;

			//Left
			if((newTargetPosition.x - widthOffset) < minWorldXBounding) {
				newTargetPosition.x = (minWorldXBounding + widthOffset);
			}
			//Right
			else if((newTargetPosition.x + widthOffset) > maxWorldXBounding) {
				newTargetPosition.x = (maxWorldXBounding - widthOffset);
			}

			//Top
			if((newTargetPosition.y + heightOffset) > minWorldYBounding) {
				newTargetPosition.y = (minWorldYBounding - heightOffset);
			}
			//Bottom
			else if((newTargetPosition.y - heightOffset) < maxWorldYBounding) {
				newTargetPosition.y = (maxWorldYBounding + heightOffset);
			}
	
		}

		currentState = PlayerState.Moving;
		arrivedAtTargetPosition = false;
		targetObject = newTargetObject;
		targetPosition = newTargetPosition;
	}

	public void SetPlayerCurrentState(PlayerState newPlayerState) {
		currentState = newPlayerState;
	}
}
