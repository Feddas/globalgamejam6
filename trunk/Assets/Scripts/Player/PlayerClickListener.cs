using UnityEngine;
using System.Collections;

/// <summary>
/// Click listener - This class assumes that it is attached to a game object that contains the script "Player"
/// 	if it's not then this will break. This is tightly coupled behavior for the player.
/// 
/// There either needs to be a way to detect the object being clicked or to communicate in the code what object on the screen is clicked
/// You can have it so each object manages their own on click logic, but if that's the case then the target object needs to be set maybe?
/// Talk to Shawn more about it. Reason being is that I'm concerned the player listener won't correctly path to objects if it doesn't know what object
/// is occurring on click. This is because this performs a general on click detection and translates that position to worldview. None of this performs
/// per object on mouse down detection /endtireddoc'ing
/// </summary>
public class PlayerClickListener : MonoBehaviour {
	
	public Player playerReference = null;

	// Use this for initialization
	void Start () {
		playerReference = this.gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		PerformClickListenerLogic();
	}

	void PerformClickListenerLogic() {
		if(Input.GetMouseButtonDown(0)) {
			Vector3 mousePosition = Input.mousePosition;
			Vector3 worldPosition = Camera.main.camera.ScreenToWorldPoint(mousePosition);
			//Debug.Log("Left Click Detected");
			//Debug.Log("Mouse Screen Position: " + mousePosition);
			//Debug.Log("Mouse World Position: " + worldPosition);

			//TO DO trigger call for separate logic branches here for better code management in case it needs to be refined
			//Maybe? Colliders should be attached to game objects, so maybe have it decide if it should path or do something special
			//who knows
			SetPlayerReferenceTargetObjectAndTargetPosition(null, new Vector2(worldPosition.x, worldPosition.y));
		}
	}

	void SetPlayerReferenceTargetObjectAndTargetPosition(GameObject newTargetObject, Vector3 newTargetPosition) {
		if(playerReference != null) {
			playerReference.SetTargetOjbectAndTargetPosition(newTargetObject, newTargetPosition);
		}
	}
}
