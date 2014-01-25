using UnityEngine;
using System.Collections;

public class HouseBaseObject : MonoBehaviour {
	public Player playerReference = null;

	// Use this for initialization
	public virtual void Start () {
		GameObject playerGameObject = GameObject.Find("Player");
		playerReference = playerGameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
	}
}
