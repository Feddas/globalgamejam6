using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextLibrary
{
	public const string CompletedDialog = "<CompletedDialog>";
	private static volatile TextLibrary _instance;
	private static object _lock = new object();

	public static TextLibrary Instance {
		get {
			if (_instance == null) {
				lock(_lock) {
					if (_instance == null) 
						_instance = new TextLibrary();
				}
			}
			return _instance;
		}
	}
	
	/// <summary> Text for all house items is sorted first by the item type then by observing the player state to index the List<string> </summary>
	Dictionary<HouseItemType, List<string>> TextHouseItem;
		
	/// <summary> Dialogs triggered by a flashback or entering a room (such as by the front door)</summary>
	Dictionary<Room, List<string>> TextRoom;
	
	//Stops the lock being created ahead of time if it's not necessary
	static TextLibrary() {
	}

	private TextLibrary() {
		loadTextHouseItem();
		loadTextRoom();
	}

	public string GetTextFor(HouseItemType targetItem)
	{
		int itemState = 0;

		if (TextHouseItem.ContainsKey(targetItem) == false)
			return null;

		switch (targetItem)
		{
		case HouseItemType.AtticRockingHorse:
			itemState = Player.Instance.GetItemState(targetItem); //TODO: observe player object to determine which itemState string to use
			Player.Instance.IncrementItemState(targetItem);
			break;
		case HouseItemType.AtticBoxes:
			itemState = Random.Range(0, 2);
			break;

		default:
			break;
		}

		if (itemState > TextHouseItem[targetItem].Count - 1)
			return CompletedDialog;
		else
			return TextHouseItem[targetItem][itemState];
	}

	public string GetTextFor(Room targetRoom)
	{
		if (TextRoom.ContainsKey(targetRoom) == false)
			return null;

		return TextRoom[targetRoom][0];
	}
	
	private void loadTextHouseItem()
	{
		TextHouseItem = new Dictionary<HouseItemType, List<string>>();
		TextHouseItem.Add(HouseItemType.AtticRockingHorse, new List<string>{
			"I wonder why this old toy was moving",
			"Opps, knocked the toy bunny over."});
		TextHouseItem.Add(HouseItemType.AtticBoxes, new List<string>{
			"What fancy boxes",
			"Are those from the 1800's?"});
	}
	
	private void loadTextRoom()
	{
		TextRoom = new Dictionary<Room, List<string>>();
		TextRoom.Add(Room.FrontHouse, new List<string>{
			"Here we are, my dad's place.",
			"He was a jerk, but I've got to prove it to myself that I'm better.",
			"At his house, with his ashes in this urn I should have his request to have them placed on the fireplace mantle in no time."});
	}
}