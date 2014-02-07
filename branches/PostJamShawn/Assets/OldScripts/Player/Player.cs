using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : TargetPathingGameObject {
	public bool arrivedAtTargetPosition = false;
	public PlayerState currentState = PlayerState.None;

	public Animator animatorReference = null;	
	private Dictionary<HouseItemType, int> itemInteractions = new Dictionary<HouseItemType, int>();

	public DirectionFacing facing = DirectionFacing.Left;

	public GameObject cinematicToPerform = null;

	//------------------------------------------------------
	//These are all the items the player can pick up
	//------------------------------------------------------
	public CarriedItemsState firePoker = CarriedItemsState.NotPickedUp;
	public CarriedItemsState mirrorShard = CarriedItemsState.NotPickedUp;
	public CarriedItemsState pillow1 = CarriedItemsState.NotPickedUp;
	public CarriedItemsState pillow2 = CarriedItemsState.NotPickedUp;
	public CarriedItemsState urn = CarriedItemsState.NotPickedUp;

	//------------------------------------------------------
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
				}
			}
			return _instance;
		}
	}
	
	private Player() {
	}
	//End of Singleton Declaration
	#endregion //------------------------------------------------------

	public override void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}

//	// Use this for initialization
	public override void Start () {
		animatorReference = this.gameObject.GetComponent<Animator>();

//		DontDestroyOnLoad(targetObject);
//		DontDestroyOnLoad(pillow1);
//		DontDestroyOnLoad(pillow2);
	}

	// Update is called once per frame
	public override void Update () {
		base.Update();
		PerformPlayerLogic();
		PerformAnimatorUpdate();
	}

	public void PerformAnimatorUpdate() {
		animatorReference.SetBool(PlayerState.None.ToString(), false);
		animatorReference.SetBool(PlayerState.BendingOver.ToString(), false);
		animatorReference.SetBool(PlayerState.Flashback.ToString(), false);
		animatorReference.SetBool(PlayerState.InCinematic.ToString(), false);
		animatorReference.SetBool(PlayerState.Moving.ToString(), false);
		animatorReference.SetBool(PlayerState.ReachingForward.ToString(), false);
		animatorReference.SetBool(PlayerState.ReachingUp.ToString(), false);
		animatorReference.SetBool(PlayerState.Standing.ToString(), false);
		//Debug.Log(currentState.ToString());
		animatorReference.SetBool(currentState.ToString(), true);
	}

	public void UpdatePlayerLocation(Room fromRoom, Room toRoom)
	{
		//this.targetPosition = PlayerLocation.Instance.GetPositionInNewScene(fromRoom, toRoom);
		this.transform.position = new Vector3(this.targetPosition.x, this.targetPosition.y, 0);

		//this.maxWorldXBounding = PlayerLocation.Instance.GetMaxBoundsInScene(toRoom).x;
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
			case(PlayerState.ReachingForward):
			break;
			case(PlayerState.ReachingUp):
			break;
			case(PlayerState.Standing):
				PerformPlayerArrivalLogic();
			break;
		}

		if(cinematicToPerform != null
		   && currentState!= PlayerState.InCinematic) {
			Destroy(cinematicToPerform);
		}
	}

	public void PerformCinematicLogic() {
	}

	public void PerformPlayerArrivalLogic() {
		if(this.gameObject.transform.position.x == targetPosition.x
		   && this.gameObject.transform.position.y == targetPosition.y) {
			//Debug.Log("Arrived in position");
			if(!arrivedAtTargetPosition) {
				arrivedAtTargetPosition = true;
				PerformPlayerArrivalHouseItemCheck();
				PerformPlayerArrivalCarryItemPickupCheck();
				currentState = PlayerState.Standing;
			}
		}
	}

	public void PerformPlayerArrivalHouseItemCheck() {
		Debug.Log("Performing Player Arrival House Item Check");
		
		if(targetObject != null) {
			Debug.Log("Target Object: " + targetObject.name);
			HouseItem houseItemReference = targetObject.GetComponent<HouseItem>();
			if(houseItemReference != null) {
				Debug.Log("House item detected");
				Debug.Log("House Type: " + houseItemReference.HouseItemOf);
				
				if(houseItemReference.HouseItemOf == HouseItemType.LivingroomFireplaceMantle) {
					if(Player.Instance.urn == CarriedItemsState.Carrying) {
						Instantiate(Resources.Load("Prefabs/Urn") as GameObject);
						Player.Instance.urn = CarriedItemsState.Used;
					}
				}
			}
		}
	}

	public void PerformPlayerArrivalCarryItemPickupCheck() {
		Debug.Log("Performing Player Arrival Carry Item Pickup Check");

		if(targetObject != null) {
			Debug.Log("Target Object: " + targetObject.name);
			HouseCarryItem houseCarryItemObjectReference = targetObject.GetComponent<HouseCarryItem>();
			if(houseCarryItemObjectReference != null) {
				Debug.Log("House carry item detected");
				Debug.Log("House Carry Type: " + houseCarryItemObjectReference.type);

				if(houseCarryItemObjectReference.type == HouseItemType.LivingroomFirePoker) {
					Debug.Log("Fire poker picked up.");
					targetObject.SetActive(false);
					Player.Instance.firePoker = CarriedItemsState.Carrying;
//					Player.Instance.firePoker.GetComponent<HouseCarryItem>().state = CarriedItemsState.Carrying;
//					DontDestroyOnLoad(Player.Instance.firePoker);
//					DontDestroyOnLoad(Player.Instance.targetObject);
				}
				if(houseCarryItemObjectReference.type == HouseItemType.MasterbedPillow1) {
					Debug.Log("Master bed pillow 1 picked up.");
					targetObject.SetActive(false);
					Player.Instance.pillow1 = CarriedItemsState.Carrying;
//					Player.Instance.pillow1.GetComponent<HouseCarryItem>().state = CarriedItemsState.Carrying;
//					DontDestroyOnLoad(Player.Instance.pillow1);
//					DontDestroyOnLoad(Player.Instance.targetObject);
				}
				if(houseCarryItemObjectReference.type == HouseItemType.MasterbedPillow2) {
					Debug.Log("Master bed pillow 2 picked up.");
					targetObject.SetActive(false);
					Player.Instance.pillow2 = CarriedItemsState.Carrying;
//					Player.Instance.pillow2.GetComponent<HouseCarryItem>().state = CarriedItemsState.Carrying;
//					DontDestroyOnLoad(Player.Instance.pillow2);
//					DontDestroyOnLoad(Player.Instance.targetObject);
				}
				if(houseCarryItemObjectReference.type == HouseItemType.MasterbedMirrorShard) {
					Debug.Log("Master bed mirror shard picked up.");
					targetObject.SetActive(false);
					Player.Instance.mirrorShard = CarriedItemsState.Carrying;
//					Player.Instance.mirrorShard.GetComponent<HouseCarryItem>().state = CarriedItemsState.Carrying;
//					DontDestroyOnLoad(Player.Instance.mirrorShard);
//					DontDestroyOnLoad(Player.Instance.targetObject);
				}
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

		OrientLocalScaleToMatchPlayerFacing();
	}

	public void OrientLocalScaleToMatchPlayerFacing() {
		//This configures the player's facing
		if(targetPosition.x < this.gameObject.transform.position.x) {
			facing = DirectionFacing.Left;
		}
		else if(targetPosition.x > this.gameObject.transform.position.x) {
			facing = DirectionFacing.Right;
		}

		//Debug.Log("Entered OrientLocalScaleToMatchPlayerFacing Method");
		//Debug.Log("Current Facing is: " + facing.ToString());
		if(facing == DirectionFacing.Left) {
			if(this.gameObject.transform.localScale.x < 0) {
				this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * -1,
				                                                   this.gameObject.transform.localScale.y,
				                                                   this.gameObject.transform.localScale.z);
			}
		}
		else if(facing == DirectionFacing.Right) {
			if(this.gameObject.transform.localScale.x > 0) {
				this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x * -1,
				                                                   this.gameObject.transform.localScale.y,
				                                                   this.gameObject.transform.localScale.z);
			}
		}
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