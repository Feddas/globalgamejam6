using UnityEngine;
using System.Collections;

public class WalkZone : MonoBehaviour
{
	public GameObject Girl;
	public Camera FollowingCamera;

	GirlController girlController;
	bool isWalking;
	Vector3 walkTarget, cameraTarget;
	
	//public delegate void Action<T>(T arg1);
	public delegate void Action(float arg1);
	public delegate void Action2(bool arg2);
	Action animateGrab;
	Action2 animateWalk;

	void Awake()
	{
		this.cameraTarget = this.FollowingCamera.transform.position; // reference the camera's y & z values

		this.girlController = this.Girl.GetComponent<GirlController>();
		this.animateGrab = this.girlController.AnimateGrab;
		this.animateWalk = this.girlController.AnimateWalk;
	}

	void Start()
	{
		//Move camera after Girls position is set from the Awake phase of GirlLocation.Instance.SetPosition().
		moveCamera();
	}

	void Update()
	{
		if (isWalking)
		{
			//move girl
			Girl.transform.position = Vector3.MoveTowards(Girl.transform.position, walkTarget, 2 * Time.deltaTime);

			moveCamera();
			
			//exit walking
			if (Girl.transform.position == walkTarget)
			{
				setWalking(false);
				if (State.Instance.ItemToInteract != HouseItemType.None)
				{
					grabbedItemDialog();
				}
			}
		}
		else if (State.Instance.ItemToInteract != HouseItemType.None)
		{
			walkToGrab();
		}
	}
	
	public void OnMouseDown()
	{
		walkStart(screenToPoint());
	}
	
	private void moveCamera()
	{
		//move camera at half speed
		this.cameraTarget.x = this.Girl.transform.position.x / 2;
		FollowingCamera.transform.position = cameraTarget;
	}

	/// <summary> moves only in the x direction, not y </summary>
	void walkToGrab()
	{
		float yDelta;
		Vector3 walkToPoint = screenToPoint(out yDelta);
		walkStart(walkToPoint);
		animateGrab(yDelta);
	}

	void grabbedItemDialog()
	{
		TextLibrary.Instance.UpdateDialog(State.Instance.ItemToInteract);
		State.Instance.ItemToInteract = HouseItemType.None;
	}
	
	void walkStart(Vector3 walkToPoint)
	{
		this.walkTarget = walkToPoint;
		setWalking(true);
		
		if (Mathf.Sign(Girl.transform.localScale.x) == Mathf.Sign(walkTarget.x - Girl.transform.position.x))
		{
			this.girlController.FlipHorizontally();
		}
	}
	
	void setWalking(bool isWalkingValue)
	{
		this.isWalking = isWalkingValue;
		if (animateWalk == null) //This should never happen, but I'm too lazy to figure out why it is occasionally happening.
		{
			animateWalk = this.girlController.AnimateWalk;
		}
		animateWalk(isWalkingValue);
	}
	
	/// <returns>The mouse postion.</returns>
	/// <param name="y">If a y coordinate is given, overwrite the mouse positions y coord.</param>
	Vector3 screenToPoint()
	{
		Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		result.z = 0;
		return result;
	}
	
	Vector3 screenToPoint(out float yDelta)
	{
		Vector3 result = screenToPoint();
		yDelta = result.y - Girl.transform.position.y;
		result.y = Girl.transform.position.y;
		return result;
	}
}