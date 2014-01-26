using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary> Spawn location is the coords for where the player started after entering this scene </summary>
public class PlayerLocation
{
	Dictionary<Tuple<Room, Room>, Vector2> spawnLocations = new Dictionary<Tuple<Room, Room>, Vector2>();
	Dictionary<Tuple<Room, Room>, Vector2> maxBounds = new Dictionary<Tuple<Room, Room>, Vector2>();

	private static volatile PlayerLocation _instance;
	private static object _lock = new object();
	
	public static PlayerLocation Instance {
		get {
			if (_instance == null) {
				lock(_lock) {
					if (_instance == null) 
						_instance = new PlayerLocation();
				}
			}
			return _instance;
		}
	}
	
	//Stops the lock being created ahead of time if it's not necessary
	static PlayerLocation() {
	}
	
	private PlayerLocation() {
		addSpawnLocation(Room.Hallway, Room.Attic, -2.5f, -3f);
		addSpawnLocation(Room.Hallway, Room.Masterbed, -4.4f, -3.6f);
		addSpawnLocation(Room.Hallway, Room.Bedroom, 6.2f, -3f);
	}

	private void addSpawnLocation(Room fromRoom, Room toRoom, float x, float y)
	{
		var keyToAdd = new Tuple<Room, Room>(fromRoom, toRoom);
		spawnLocations.Add(keyToAdd, new Vector2(x, y));
	}

	public Vector2 GetPositionInNewScene(Room fromRoom, Room toRoom)
	{
		var key = new Tuple<Room, Room>(fromRoom, toRoom);
		if (spawnLocations.ContainsKey(key))
			return spawnLocations[key];
		else
			return new Vector2(0f, -3.5f);
	}

	public Vector2 GetMaxBoundsInScene(Room targetRoom)
	{
		Vector2 result = new Vector2(3.6f, -4.9f);

		switch (targetRoom)
		{
		case Room.Attic:
		case Room.Foyer:
		case Room.Hallway:
		case Room.LivingRoom:
			result.x = 6.5f;
			break;
			
		case Room.Bedroom:
			result.x = 8.6f;
			break;
		case Room.Masterbed:
			result.x = 6.8f;
			break;
			
			//everything else
		default:
			result.x = 3.6f;
			break;
		}

		return result;
	}
}