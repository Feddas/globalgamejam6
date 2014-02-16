using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class TextLibrary
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
		case HouseItemType.LockedDoor:
			itemState = Random.Range(0,3);
			break;
		default: break; //use default value of 0
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
		
		//only use text for exact match to current level of completion
		if (TextRoom.ContainsKey(textKey) == false)
		{
			return null;
		}
		
		return TextRoom[textKey][0];
	}
}