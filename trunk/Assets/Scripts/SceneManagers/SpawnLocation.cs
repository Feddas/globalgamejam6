using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary> Spawn location is the coords for where the player started after entering this scene </summary>
public class SpawnLocation
{
	Dictionary<Tuple<Room, Room>, Vector2> spawnLocations = new Dictionary<Tuple<Room, Room>, Vector2>();

	private static volatile SpawnLocation _instance;
	private static object _lock = new object();
	
	public static SpawnLocation Instance {
		get {
			if (_instance == null) {
				lock(_lock) {
					if (_instance == null) 
						_instance = new SpawnLocation();
				}
			}
			return _instance;
		}
	}
	
	//Stops the lock being created ahead of time if it's not necessary
	static SpawnLocation() {
	}
	
	private SpawnLocation() {
		addSpawnLocation(Room.Hallway, Room.Attic, -2.5f, -3f);
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
}