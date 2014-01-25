using UnityEngine;
using System.Collections;

//TO DO: PUT ARRIVAL LOGIC SO YOU CAN DETECT THE SECOND YOU ARRIVE, THEN USE THAT TO HOOK IN LOGIC FOR EACH OBJECT
public class TargetPathingGameObject : MonoBehaviour {

	public GameObject targetObject = null;
	public Vector2 targetPosition = Vector2.zero;

	public bool useWorldBoundings = false;
	public float minWorldXBounding = 0;
	public float maxWorldXBounding = 0;
	public float minWorldYBounding = 0;
	public float maxWorldYBounding = 0;

	public float xMovementSpeed = 1;
	public float yMovementSpeed = 1;

	public float targetPositionXLockBuffer = 0;
	public float targetPositionYLockBuffer = 0;

	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
		//Debug.Log("Performing Movement Logic");
		PerformMovementLogic();
		//PerformTargetPositionLocking();
	}

	public void PerformMovementLogic() {
		Vector2 nextMoveStepSize = CalculateNextMovementStep();
		nextMoveStepSize = PerformTargetPositionLocking(nextMoveStepSize);
		Vector3 nextWorldPosition = this.gameObject.transform.position + (new Vector3(nextMoveStepSize.x, nextMoveStepSize.y, this.gameObject.transform.position.z));

		//Not super accurate but does the job
		float widthOffset = (this.gameObject.transform.localScale.x/2);
		float heightOffset = (this.gameObject.transform.localScale.y/2);
		//Debug.Log("Current Position: " + this.gameObject.transform.position);
		//Debug.Log("Next Position: " + nextWorldPosition);
		//Debug.Log("Next Position With X Bound: " + (nextWorldPosition.x + this.gameObject.transform.localScale.x/2);
		//Debug.Log(this.gameObject.transform.localScale.x/2);
		if(useWorldBoundings) {
			//Left X
			if((nextWorldPosition.x - widthOffset) < minWorldXBounding) {
				nextWorldPosition.x = (minWorldXBounding + widthOffset);
			}
			//Right X
			else if((nextWorldPosition.x + widthOffset) > maxWorldXBounding) {
				nextWorldPosition.x = (maxWorldXBounding -	 widthOffset);;
			}

			//Top Y
			if((nextWorldPosition.y + heightOffset) > minWorldYBounding) {
				nextWorldPosition.y = (minWorldYBounding - heightOffset);
				//Debug.Log("Min World Y bounding reset");
			}
			//Bottom Y
			else if((nextWorldPosition.y - heightOffset) < maxWorldYBounding) {
				nextWorldPosition.y = (maxWorldYBounding + heightOffset);
				//Debug.Log("Max World Y bounding reset");
			}
		}
		
		this.gameObject.transform.position = nextWorldPosition;
	}

	public Vector2 PerformTargetPositionLocking(Vector2 newPositionOffset) {
		//x locker
		//			Debug.Log("target x pos great: " + (targetPosition.x - targetPositionXLockBuffer));
		//			Debug.Log("target x pos less: " + (targetPosition.x + targetPositionXLockBuffer));
		//			Debug.Log("target x pos: " + targetPosition.x);
		//			Debug.Log("next x pos: " + (this.gameObject.transform.position.x + newPositionOffset.x));
		if((this.gameObject.transform.position.x + newPositionOffset.x) > (targetPosition.x - targetPositionXLockBuffer)
		   && (this.gameObject.transform.position.x + newPositionOffset.x) < (targetPosition.x + targetPositionXLockBuffer)) {
			//Debug.Log("setting x position to target");
			this.gameObject.transform.position = new Vector3(targetPosition.x,
			                                                 this.gameObject.transform.position.y,
			                                                 this.gameObject.transform.position.z);
			newPositionOffset.x = 0;
		}
		//			//y locker
		if((this.gameObject.transform.position.y + newPositionOffset.y) > (targetPosition.y - targetPositionYLockBuffer)
		   && (this.gameObject.transform.position.y + newPositionOffset.y) < (targetPosition.y + targetPositionYLockBuffer)) {
			//Debug.Log("setting y position to target");
			this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x,
			                                                 targetPosition.y,
			                                                 this.gameObject.transform.position.z);
			newPositionOffset.y = 0;
		} 
		
		return newPositionOffset;
	}

	public virtual void SetTargetObjectAndTargetPosition(GameObject newTargetObject, Vector2 newTargetPosition) {
		targetObject = newTargetObject;
		targetPosition = newTargetPosition;
	}

	public Vector2 CalculateNextMovementStep() {
		Vector2 movementDistance = Vector2.zero;

		if(this.gameObject.transform.position.x < targetPosition.x) {
			movementDistance.x += xMovementSpeed;
		}
		else if(this.gameObject.transform.position.x > targetPosition.x) {
			movementDistance.x -= xMovementSpeed;
		}
		if(this.gameObject.transform.position.y < targetPosition.y) {
			movementDistance.y += yMovementSpeed;
		}
		else if(this.gameObject.transform.position.y > targetPosition.y) {
			movementDistance.y -= yMovementSpeed;
		}

		return movementDistance;
	}

	public bool hasTargetObjectArrivedAtLocation() {
		if(this.gameObject.transform.position.x == targetPosition.x
		   && this.gameObject.transform.position.y == targetPosition.y) {
			return true;
		}
		else {
			return false;
		}
	}
}
