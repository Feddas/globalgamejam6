using UnityEngine;
using System.Collections;

public class Player : TargetPathingGameObject {
	public bool arrivedAtTargetPosition = false;

//	// Use this for initialization
//	public override void Start () {
//	}
//	// Update is called once per frame
//	public override void Update () {
//	}
//	public void OnCollisionEnter(Collision collisionInfo) {
//		Debug.Log("Collision detected");
//		HouseItem houseItem = collisionInfo.gameObject.GetComponent<HouseItem>();
//		if(houseItem != null) {
//		}
//	}
	public void PerformPlayerArrivalLogic() {
		if(this.gameObject.transform.position.x == targetPosition.x
		   && this.gameObject.transform.position.y == targetPosition.y) {
			if(!arrivedAtTargetPosition) {
				arrivedAtTargetPosition = true;
			}
		}
	}

	public override void SetTargetOjbectAndTargetPosition(GameObject newTargetObject, Vector2 newTargetPosition) {
		arrivedAtTargetPosition = false;
		targetObject = newTargetObject;
		targetPosition = newTargetPosition;
	}
}
