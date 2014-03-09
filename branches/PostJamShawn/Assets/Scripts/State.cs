using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class State
{
	public Room SceneCurrent;
	public Room SceneLast;
	public HouseItemType ItemToInteract;

	/// <summary> How much of the game the girl has completed so far. Used for keying text and item inventory </summary>
	public Completion Completed
	{
		get { return completed; }
		set
		{
			if (value == completed)
				return;

			completed = value;
			onCompletedChanged();
		}
	}
	private Completion completed;

	public string KongregateUserInfo { get; set; }

	/// <summary> the time when the player hit the start button, in Ticks
	public long StartTime { get; set; }

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
	public Dictionary<HouseItemType, Nullable<Color>> NewItemColor = new Dictionary<HouseItemType, Color?>();	
	public Dictionary<HouseItemType, Transform> NewItemTransform = new Dictionary<HouseItemType, Transform>();

	private Dictionary<Tuple<HouseItemType, Completion>, int> itemInteractions = new Dictionary<Tuple<HouseItemType, Completion>, int>();
	public int GetItemState(Tuple<HouseItemType, Completion> targetItem)
	{
		if (itemInteractions.ContainsKey(targetItem) == false)
			return 0;
		else
			return itemInteractions[targetItem];
	}
	
	public void IncrementItemState(Tuple<HouseItemType, Completion> targetItem)
	{
		if (itemInteractions.ContainsKey(targetItem) == false)
			itemInteractions.Add(targetItem, 1);
		else
			itemInteractions[targetItem]++;
	}

	public void ResetItemState(Tuple<HouseItemType, Completion> targetItem)
	{		
		if (itemInteractions.ContainsKey(targetItem) == false)
			itemInteractions.Add(targetItem, 0);
		else
			itemInteractions[targetItem] = 0;
	}
	#endregion item state

	private void onCompletedChanged()
	{
		if (string.IsNullOrEmpty(State.Instance.KongregateUserInfo) == false)
		{
			int progress = (100 * (int)completed) / CompletionStep.TotalSteps;
			Application.ExternalCall("kongregate.stats.submit", "GameComplete", progress);
		}
	}

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
