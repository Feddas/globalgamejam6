using UnityEngine;
using System.Collections;

//NOTE: sprite sheet uses 372x372
public class GirlController : MonoBehaviour
{
	/// <summary> The exposing of State.CurrentDialog to a game object </summary>
	public string CurrentDialog { get; set;	}

	public Camera FollowingCamera;

	bool isWalking;
	Vector3 walkTarget, cameraTarget;
	private Animator anim;					// Reference to the player's animator component.
	
	void Awake()
	{
		this.anim = GetComponent<Animator>();
		this.cameraTarget = FollowingCamera.transform.position; // reference the camera's y & z values
		State.Instance.Girl = this;
		
		State.Instance.Completed = Completion.PlacedUrn;
		this.CurrentDialog = TextLibrary.Instance.GetTextFor(Room.FrontHouse); //TODO: somehow move this line to GirlLocation.cs
	}

	void Start() { }
	
	void Update()
	{
		if (isWalking)
		{
			//move girl
			this.transform.position = Vector3.MoveTowards(this.transform.position, walkTarget, 2 * Time.deltaTime);

			//move camera at half speed
			cameraTarget.x = this.transform.position.x / 2;
			FollowingCamera.transform.position = cameraTarget;

			//exit walking
			if (this.transform.position == walkTarget)
			{
				setWalking(false);
				State.Instance.ItemToInteract = HouseItemType.None;
			}
		}
		else if (State.Instance.ItemToInteract != HouseItemType.None)
		{
			walkToGrab();
		}

		#if UNITY_ANDROID
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		#endif
	}

	#region Walk and grab
	public void OnClick(dfControl control, dfMouseEventArgs mouseEvent)
	{
		if (control.name == "Walk")
			walkStart(screenToPoint());
	}

	/// <summary> moves only in the x direction, not y </summary>
	void walkToGrab()
	{
		float yDelta;
		Vector3 walkToPoint = screenToPoint(out yDelta);
		walkStart(walkToPoint);

		if (yDelta > 4.8f)
			anim.SetTrigger("GrabUp");
		else if (yDelta < 1.0f)
			anim.SetTrigger("GrabDown");
		else
			anim.SetTrigger("GrabFront");
	}

	void walkStart(Vector3 walkToPoint)
	{
		this.walkTarget = walkToPoint;
		setWalking(true);
		
		if (Mathf.Sign(this.transform.localScale.x) == Mathf.Sign(walkTarget.x - this.transform.position.x))
		{
			Flip();
		}
	}

	void setWalking(bool isWalkingValue)
	{
		this.isWalking = isWalkingValue;
		anim.SetBool("IsWalking", isWalkingValue);
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
		yDelta = result.y - this.transform.position.y;
		result.y = this.transform.position.y;
		return result;
	}

	/// <summary> Pulled from Unity2D 4.3 potato vs aliens demo </summary>
	void Flip()
	{
		// Multiply the player's x local scale by -1.
		Vector3 theScale = this.transform.localScale;
		theScale.x *= -1;
		this.transform.localScale = theScale;
	}
	#endregion Walk and grab
}
