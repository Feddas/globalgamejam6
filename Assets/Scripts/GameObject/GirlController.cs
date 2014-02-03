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
		this.CurrentDialog = TextLibrary.Instance.GetTextFor(Room.FrontHouse);
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
				setWalking(false);
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
		if (control.name.StartsWith("Up"))
			walkToGrab("GrabUp");
		else if (control.name.StartsWith("Front"))
			walkToGrab("GrabFront");
		else if (control.name.StartsWith("Down"))
			walkToGrab("GrabDown");
		else if (control.name == "Walk")
			walkStart(screenToPoint(null));
	}

	//moves only in the x direction, not y
	void walkToGrab(string grabKey)
	{
		walkStart(screenToPoint(this.transform.position.y));
		anim.SetTrigger(grabKey);
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
	Vector3 screenToPoint(float? y)
	{
		Vector3 result = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (y.HasValue)
			result.y = y.Value;
		result.z = 0;
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
