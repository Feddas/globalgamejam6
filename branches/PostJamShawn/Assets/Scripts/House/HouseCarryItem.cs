using UnityEngine;
using System.Collections;

public class HouseCarryItem : HouseBaseObject {

	public HouseItemType type = HouseItemType.None;
	public CarriedItemsState state = CarriedItemsState.NotPickedUp;

	public override void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	public override void Start () {
		base.Start();
		switch(type) {
		case(HouseItemType.LivingroomFirePoker):
			if(Player.Instance.firePoker == CarriedItemsState.Carrying
			   || Player.Instance.firePoker == CarriedItemsState.Used) {
				Destroy(this.gameObject);
			}
			break;
		case(HouseItemType.MasterbedMirrorShard):
			if(Player.Instance.mirrorShard == CarriedItemsState.Carrying
			   || Player.Instance.mirrorShard == CarriedItemsState.Used) {
				Destroy(this.gameObject);
			}
			break;
		case(HouseItemType.MasterbedPillow1):
			if(Player.Instance.pillow1 == CarriedItemsState.Carrying
			   || Player.Instance.pillow1 == CarriedItemsState.Used) {
				Destroy(this.gameObject);
			}
			break;
		case(HouseItemType.MasterbedPillow2):
			if(Player.Instance.pillow2 == CarriedItemsState.Carrying
			   || Player.Instance.pillow2 == CarriedItemsState.Used) {
				Destroy(this.gameObject);
			}
			break;
		}
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public virtual void OnMouseDown() {
		//Debug.Log(this.gameObject.name + " was clicked.");
		
		Vector3 mousePosition = Input.mousePosition;
		Vector3 worldPosition = Camera.main.camera.ScreenToWorldPoint(mousePosition);
		
		//Debug.Log("calling player set target from house item");
		playerReference.SetTargetObjectAndTargetPosition(this.gameObject, worldPosition);
	}
}