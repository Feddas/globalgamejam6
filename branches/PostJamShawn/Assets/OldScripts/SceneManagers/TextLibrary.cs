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
		TextHouseItem.Add(HouseItemType.AtticBoxes, new List<string>{
			"These are filled with Liz’s old stuff…"});
		TextHouseItem.Add(HouseItemType.AtticRockingHorse, new List<string>{
			"The attic used to be empty. This must be where Dad put all of the old things after Mom died."});
		TextHouseItem.Add(HouseItemType.BedroomBed, new List<string>{
			"My old bed… it’s just as I left it. I think I’ll lay down for a moment…"});
		TextHouseItem.Add(HouseItemType.BedroomPainting, new List<string>{//Poster
			"I can’t believe I listened to them…"});
		TextHouseItem.Add(HouseItemType.FrontCar, new List<string>{
			"I did what I came for. Time to put this place, and the past, in the mirror…"});
		TextHouseItem.Add(HouseItemType.LivingroomCodex, new List<string>{//without all pieces
			"This looks like it would fit in the puzzle box. I guess these things are just lying about. I wonder how many more there are."});
		TextHouseItem.Add(HouseItemType.LivingroomFirePoker, new List<string>{
			"This place is even creepier than I remember. Maybe I’ll keep this with me…just in case."});
		TextHouseItem.Add(HouseItemType.LivingroomFireplaceMantle, new List<string>{
			"Here you go Dad. You have the place all to yourself, just like you wanted."});
		TextHouseItem.Add(HouseItemType.LivingroomPaintingFireplace, new List<string>{
			"I don’t remember this being here… it looks like there’s a note tucked behind the canvas."});
		TextHouseItem.Add(HouseItemType.LivingroomPaintingsCreepy, new List<string>{
			"The original home owners. Mom said these paintings had charm, but I always felt like they were staring at me."});
		TextHouseItem.Add(HouseItemType.FoyerChair, new List<string>{
			"I always hid behind this while playing with Liz…"});
		TextHouseItem.Add(HouseItemType.FoyerMirror, new List<string>{//without mirror shard
			"The mirror… I remember…"});
		TextHouseItem.Add(HouseItemType.DoorAttic, new List<string>{//without FirePoker
			"None of us were ever able to open the attic once the chord broke. We always used something to reach up there…"});
		TextHouseItem.Add(HouseItemType.HallDoorLockedBathroom, new List<string>{
			"It's just the bathroom."});
		TextHouseItem.Add(HouseItemType.HallFloorboard, new List<string>{
			"Wasn’t Dad supposed to fix this? It looks loose…"});
		TextHouseItem.Add(HouseItemType.MasterbedMirrorShard, new List<string>{
			"This looks like it would fit somewhere…"});
	}
	
	private void loadTextRoom()
	{
		TextRoom = new Dictionary<Room, List<string>>();
		TextRoom.Add(Room.FrontHouse, new List<string>{
			"It’s been so long since I’ve been home.",
			"I hardly remember the place, but Dad wanted his ashes placed over the mantle. I’ll just put them there and leave.",
			"Strange to have an unfinished inscription on the urn though. All it says is, YOU ARE."});
		TextRoom.Add(Room.Attic, new List<string>{
		});
		TextRoom.Add(Room.Bedroom, new List<string>{
		});
		TextRoom.Add(Room.Foyer, new List<string>{
		});
		TextRoom.Add(Room.Hallway, new List<string>{
		});
		TextRoom.Add(Room.LivingRoom, new List<string>{
		});
		TextRoom.Add(Room.Masterbed, new List<string>{
		});
		
	}
}