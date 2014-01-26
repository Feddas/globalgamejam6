﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : TargetPathingGameObject {
	public bool arrivedAtTargetPosition = false;
	public PlayerState currentState = PlayerState.None;
	
	private Dictionary<HouseItemType, int> itemInteractions = new Dictionary<HouseItemType, int>();
	#region Singleton Declaration
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
	#endregion //------------------------------------------------------

//	// Use this for initialization
//	public override void Start () {
//	}

	// Update is called once per frame
	public override void Update () {
		base.Update();
		PerformPlayerLogic();
	}

	public void UpdateSpawnLocation(Room fromRoom, Room toRoom)
	{
		this.targetPosition = SpawnLocation.Instance.GetPositionInNewScene(fromRoom, toRoom);

		switch (toRoom)
		{
		case Room.Attic:
		case Room.Foyer:
		case Room.Hallway:
		case Room.LivingRoom:
			this.maxWorldXBounding = 6.5f;
			break;

		//everything else
		default:
			this.maxWorldXBounding = 3.6f;
			break;
		}
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
				PerformPlayerArrivalLogic();
			break;
		}
	}
	public void PerformPlayerArrivalLogic() {
		if(this.gameObject.transform.position.x == targetPosition.x
		   && this.gameObject.transform.position.y == targetPosition.y) {
			//Debug.Log("Arrived in position");
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

	public int GetItemState(HouseItemType targetItem)
	{
		if (itemInteractions.ContainsKey(targetItem) == false)
			return 0;
		else
			return itemInteractions[targetItem];
	}

	public void IncrementItemState(HouseItemType targetItem)
	{
		if (itemInteractions.ContainsKey(targetItem) == false)
			itemInteractions.Add(targetItem, 1);
		else
			itemInteractions[targetItem]++;
	}
}