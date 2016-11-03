using UnityEngine;
using System.Collections;
using Valve.VR;

public class LeftStick : MonoBehaviour {

	EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
	EVRButtonId touchPad = EVRButtonId.k_EButton_SteamVR_Touchpad;
	EVRButtonId menu = EVRButtonId.k_EButton_ApplicationMenu;

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device controller;

	public Transform seat;
	public Transform head;

	static float hover;
	static Vector2 thrust;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
		transform.parent.position = seat.position + (transform.parent.position - head.position);
	}
	
	void Update () {
		hover = controller.GetAxis(touchPad).y;
		thrust = controller.GetAxis(trigger);
		if (controller.GetPressDown(menu)) {
			transform.parent.position = seat.position + (transform.parent.position - head.position);
			print("RESET");
		}
	}
	
	public static float GetHover() {
		return hover;
	}

	public static Vector2 GetTriggerAxis() {
		return thrust;
	}
}
