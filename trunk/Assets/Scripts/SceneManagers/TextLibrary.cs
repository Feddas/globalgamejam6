using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextLibrary
{
	//TODO: singleton-ify this class

	/// <summary> Text for all house items is sorted first by the item type then by observing the player state to index the List<string> </summary>
	Dictionary<HouseItemType, List<string>> TextHouseItem;

	/// <summary> Various dialog for the flashback animations </summary>
	Dictionary<Room, List<string>> TextFlashback;

	/// <summary> Dialogs trigged by a zone, such as by the front door</summary>
	Dictionary<Room, List<string>> TextZone;

	public TextLibrary()
	{
		TextHouseItem = new Dictionary<HouseItemType, List<string>>();
		TextHouseItem.Add(HouseItemType.AtticBoxes, new List<string>{
			"What fancy boxes",
			"Are those from the 1800's?"});
	}

	public string GetTextFor(HouseItemType targetItem)
	{
		int itemState = 0;

		switch (targetItem)
		{
		case HouseItemType.AtticBoxes:
			itemState = 0;//TODO: observe player object to determine which itemState string to use
			break;

		default:
			break;
		}

		return TextHouseItem[targetItem][itemState];
	}
}