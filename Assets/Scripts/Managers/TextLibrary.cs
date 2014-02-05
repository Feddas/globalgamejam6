using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextLibrary
{
	public const string CompletedDialog = "<CompletedDialog>";

	#region singleton
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

	//Stops the lock being created ahead of time if it's not necessary
	static TextLibrary() { }
	#endregion singleton

	/// <summary> Text for all house items is sorted first by the item type then by observing the player state to index the List<string> </summary>
	Dictionary<Tuple<HouseItemType, Completion>, List<string>> TextHouseItem;
	
	/// <summary> Dialogs triggered by a flashback or entering a room (such as by the front door)</summary>
	Dictionary<Tuple<Room, Completion>, List<string>> TextRoom;
		
	private TextLibrary() {
		loadTextHouseItem();
		loadTextRoom();
	}

	public void UpdateDialog(HouseItemType houseItem)
	{
		string dialogText = TextLibrary.Instance.GetTextFor(houseItem);
		if (string.IsNullOrEmpty(dialogText))
		{
			//Debug.Log(houseItem.ToString() + " was clicked.");
			return;
		}
		else if (dialogText != TextLibrary.CompletedDialog)
		{
			State.Instance.CurrentDialog = dialogText;
			State.Instance.GameDialog.IsVisible = true;
		}
	}

	public string GetTextFor(HouseItemType targetItem)
	{
		Completion completed = State.Instance.Completed;
		var textKey = Tuple.Create(targetItem, completed);

		//find closest text to current level of completion
		while (TextHouseItem.ContainsKey(textKey) == false)
		{
			if ((int)completed == 0)
				return null;
			
			completed = (Completion)((int)completed - 1);
			textKey.Item2 = completed;
		}
		int itemState = 0;

		//use random value
		switch (targetItem)
		{
		case HouseItemType.AtticBoxes:
			itemState = Random.Range(0, 2);
			break;
		default: break;
		}

		//check progress of item dialog to determine which itemState string to use
		if (itemState == 0)
		{
			itemState = State.Instance.GetItemState(textKey);
			State.Instance.IncrementItemState(textKey);
		}
		
		if (itemState > TextHouseItem[textKey].Count - 1)
		{
			State.Instance.ResetItemState(textKey);
			return CompletedDialog;
		}
		else
			return TextHouseItem[textKey][itemState];
	}
	
	public string GetTextFor(Room targetRoom)
	{
		Completion completed = State.Instance.Completed;
		var textKey = Tuple.Create(targetRoom, completed);

		//find closest text to current level of completion
		while (TextRoom.ContainsKey(textKey) == false)
		{
			if ((int)completed == 0)
				return null;

			completed = (Completion)((int)completed - 1);
			textKey.Item2 = completed;
		}
		
		return TextRoom[textKey][0];
	}
	
	private void loadTextHouseItem()
	{
		TextHouseItem = new Dictionary<Tuple<HouseItemType, Completion>, List<string>>();
		TextHouseItem.Add(Tuple.Create(HouseItemType.AtticBoxes, Completion.None), new List<string>{
			"These are filled with Liz’s old stuff…"});
		TextHouseItem.Add(Tuple.Create(HouseItemType.AtticRockingHorse, Completion.None), new List<string>{
			"The attic used to be empty. This must be where Dad put all of the old things after Mom died."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.BedroomBed, Completion.None), new List<string>{
			"My old bed… it’s just as I left it. I think I’ll lay down for a moment…"});
		TextHouseItem.Add(Tuple.Create(HouseItemType.BedroomPainting, Completion.None), new List<string>{//Poster
			"I can’t believe I listened to them…"});
		TextHouseItem.Add(Tuple.Create(HouseItemType.FrontCar, Completion.None), new List<string>{
			"I did what I came for. Time to put this place, and the past, in the mirror…"});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomCodex, Completion.None), new List<string>{//without all pieces
			"This looks like it would fit in the puzzle box. I guess these things are just lying about. I wonder how many more there are."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomFirePoker, Completion.None), new List<string>{
			"This place is even creepier than I remember. Maybe I’ll keep this with me…just in case."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomFireplaceMantle, Completion.None), new List<string>{
			"Here you go Dad. You have the place all to yourself, just like you wanted."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingFireplace, Completion.None), new List<string>{
			"I don’t remember this being here… it looks like there’s a note tucked behind the canvas."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.LivingroomPaintingsCreepy, Completion.None), new List<string>{
			"The original home owners. Mom said these paintings had charm, but I always felt like they were staring at me."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.FoyerChair, Completion.None), new List<string>{
			"I always hid behind this while playing with Liz…"});
		TextHouseItem.Add(Tuple.Create(HouseItemType.FoyerMirror, Completion.None), new List<string>{//without mirror shard
			"The mirror...",
			"I remember..."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.DoorAttic, Completion.None), new List<string>{//without FirePoker
			"None of us were ever able to open the attic once the chord broke. We always used something to reach up there…"});
		TextHouseItem.Add(Tuple.Create(HouseItemType.HallDoorLockedBathroom, Completion.None), new List<string>{
			"It's just the bathroom."});
		TextHouseItem.Add(Tuple.Create(HouseItemType.HallFloorboard, Completion.None), new List<string>{
			"Wasn’t Dad supposed to fix this? It looks loose…"});
		TextHouseItem.Add(Tuple.Create(HouseItemType.MasterbedMirrorShard, Completion.None), new List<string>{
			"This looks like it would fit somewhere…"});
	}
	
	private void loadTextRoom()
	{
		TextRoom = new Dictionary<Tuple<Room, Completion>, List<string>>();
		TextRoom.Add(Tuple.Create(Room.FrontHouse, Completion.Start), new List<string>{
@"It's been so long since I've been home. I hardly remember the place, but Dad wanted his ashes placed over the mantle. I'll just put them there and leave.
Strange to have an unfinished inscription on the urn though. All it says is, ""YOU ARE""."});
	}
}