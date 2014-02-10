using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary> Spawn location is the coords for where the player started after entering this scene </summary>
public class GirlLocation
{
	Dictionary<Tuple<Room, Room>, Vector2> spawnLocations = new Dictionary<Tuple<Room, Room>, Vector2>();
	
	#region singleton
	private static volatile GirlLocation _instance;
	private static object _lock = new object();
	
	public static GirlLocation Instance {
		get {
			if (_instance == null) {
				lock(_lock) {
					if (_instance == null) 
						_instance = new GirlLocation();
				}
			}
			return _instance;
		}
	}
	
	//Stops the lock being created ahead of time if it's not necessary
	static GirlLocation() {
	}
	#endregion singleton
	
	private GirlLocation() {
		addSpawnLocation(Room.FrontHouse, Room.Foyer, 6.9f, -3f);
		addSpawnLocation(Room.Foyer, Room.FrontHouse, -6.9f, -3f);
		addSpawnLocation(Room.Foyer, Room.LivingRoom, 6.9f, -3f);
		addSpawnLocation(Room.LivingRoom, Room.Foyer, -6.9f, -3f);

		//Opened up after Completed.CryptexPieces1
		addSpawnLocation(Room.Foyer, Room.Hallway, 0f, -3.5f);
		addSpawnLocation(Room.Hallway, Room.Foyer, -4f, -3f);
		addSpawnLocation(Room.Hallway, Room.Bedroom, 6.9f, -3f);
		addSpawnLocation(Room.Bedroom, Room.Hallway, -6.9f, -2f);

		addSpawnLocation(Room.Hallway, Room.Masterbed, -6.9f, -3f);
		addSpawnLocation(Room.Masterbed, Room.Hallway, 6.9f, -2f);

		//Opened up after Completed.CryptexPieces4
		addSpawnLocation(Room.Hallway, Room.Attic, -1f, -3f);
		addSpawnLocation(Room.Attic, Room.Hallway, 4f, -2.5f);
	}

	public void SetPosition()
	{
		//Note: this return should only happen on the first scene, FrontHouse
		if (State.Instance.SceneLast == State.Instance.SceneCurrent)
			return;

		//set girl start location
		Vector2 position = getPositionInNewScene(State.Instance.SceneLast, State.Instance.SceneCurrent);
		State.Instance.Girl.transform.position = new Vector3(position.x, position.y, 0);
	}

	private void addSpawnLocation(Room fromRoom, Room toRoom, float x, float y)
	{
		var keyToAdd = new Tuple<Room, Room>(fromRoom, toRoom);
		spawnLocations.Add(keyToAdd, new Vector2(x, y));
	}

	private Vector2 getPositionInNewScene(Room fromRoom, Room toRoom)
	{
		var key = new Tuple<Room, Room>(fromRoom, toRoom);
		if (spawnLocations.ContainsKey(key))
			return spawnLocations[key];
		else
			return new Vector2(0f, -3.5f);
	}
}