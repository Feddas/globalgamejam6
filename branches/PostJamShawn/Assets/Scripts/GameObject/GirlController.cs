using UnityEngine;
using System.Collections;

//NOTE: sprite sheet of Girl uses 372x372 grid cell size
public class GirlController : MonoBehaviour
{
	/// <summary> The exposing of State.CurrentDialog to a game object </summary>
	public string CurrentDialog { get; set;	}

	public dfControl GameDialog;

	private Animator anim;					// Reference to the player's animator component.
	
	void Awake()
	{
		this.anim = GetComponent<Animator>();
		State.Instance.Girl = this;
		
		//set dialog
		State.Instance.GameDialog = this.GameDialog;
		State.Instance.CurrentDialog = TextLibrary.Instance.GetTextFor(State.Instance.SceneCurrent);
		State.Instance.GameDialog.IsVisible = string.IsNullOrEmpty(State.Instance.CurrentDialog) == false;

		GirlLocation.Instance.SetPosition();
	}

	void Start() { }
	
	void Update()
	{
		#if UNITY_ANDROID
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
		#endif
	}
	
	/// <summary> Pulled from Unity2D 4.3 potato vs aliens demo </summary>
	public void FlipHorizontally()
	{
		// Multiply the player's x local scale by -1.
		Vector3 theScale = this.transform.localScale;
		theScale.x *= -1;
		this.transform.localScale = theScale;
	}

	/// <summary> Used to make the girl run her grab animations </summary>
	public void AnimateGrab(float yDelta)
	{
		if (yDelta > 4.8f)
			anim.SetTrigger("GrabUp");
		else if (yDelta < 1.0f)
			anim.SetTrigger("GrabDown");
		else
			anim.SetTrigger("GrabFront");
	}
	
	/// <summary> Used to make the girl run her walk animation </summary>
	public void AnimateWalk(bool isWalking)
	{
		anim.SetBool("IsWalking", isWalking);
	}
}