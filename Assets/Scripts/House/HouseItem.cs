using UnityEngine;
using System.Collections;

public class HouseItem : HouseBaseObject {

	public HouseItemType type = HouseItemType.None;

	// Use this for initialization
	public override void Start () {
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
	}

	public void OnMouseDown() {
		//Debug.Log(this.gameObject.name + " was clicked.");

		Vector3 mousePosition = Input.mousePosition;
		Vector3 worldPosition = Camera.main.camera.ScreenToWorldPoint(mousePosition);

		//Debug.Log("calling player set target from house item");
		playerReference.SetTargetObjectAndTargetPosition(this.gameObject, worldPosition);
	}
}
