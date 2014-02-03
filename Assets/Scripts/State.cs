using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class State
{
	/// <summary> How much of the game the girl has completed so far. Used for keying text and item inventory </summary>
	public Completion Completed { get; set; }

	#region display game dialog
	public dfControl GameDialog { get; set; }

	public string CurrentDialog {
		get { return Girl.CurrentDialog; }
		set {
			if (value == Girl.CurrentDialog)
				return;
			Girl.CurrentDialog = value;
		}
	}

	/// <summary> Only (so far R44) used to reference the current Girl.CurrentDialog </summary>
	public GirlController Girl {
		get { return girl; }
		set {
			if (value == girl)
				return;
			girl = value;
		}
	}
	private GirlController girl;
	#endregion display game dialog

	#region item state
	private Dictionary<HouseItemType, int> itemInteractions = new Dictionary<HouseItemType, int>();
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

	public void ResetItemState(HouseItemType targetItem)
	{		
		if (itemInteractions.ContainsKey(targetItem) == false)
			itemInteractions.Add(targetItem, 0);
		else
			itemInteractions[targetItem] = 0;
	}
	#endregion item state

	#region singleton
	private static volatile State _instance;
	private static object _lock = new object();
	
	public static State Instance {
		get {
			if (_instance == null) {
				lock(_lock) {
					if (_instance == null) 
						_instance = new State();
				}
			}
			return _instance;
		}
	}
	//Stops the lock being created ahead of time if it's not necessary
	static State() { }
	private State() { }
	#endregion singleton
}
