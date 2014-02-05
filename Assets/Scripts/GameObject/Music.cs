using UnityEngine;
using System.Collections;

/// <summary> Persists the music between scenes using UnitySingletons DontDestroyOnLoad
/// Based off of http://answers.unity3d.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html </summary>
public class Music : MonoBehaviour
{
	private static Music instance = null;
	public static Music Instance
	{
		get { return instance; }
	}
	
	void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		
		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}

	void Start() { }
	void Update() { }
}