using UnityEngine;
using System.Collections;
using Valve.VR;

public class RightStick : MonoBehaviour {

	EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
	EVRButtonId touchPad = EVRButtonId.k_EButton_SteamVR_Touchpad;
	EVRButtonId menu = EVRButtonId.k_EButton_ApplicationMenu;

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device controller;

	public AudioSource music;

	static Vector3 stickVec;
	static Vector2 thrust;
	static float jaw;
	static bool menuPressed;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
	}

	void Update () {
		stickVec = transform.localRotation * Vector3.forward;
		stickVec = Quaternion.Euler(90, 0, 0) * stickVec;
		stickVec.z = 0;

		jaw = controller.GetAxis(touchPad).x;
		thrust = controller.GetAxis(trigger);

		if (controller.GetPressDown(menu) && !menuPressed) {
			menuPressed = true;
			music.Play();
		}
	}

	public static Vector3 GetPitchRoll () {
		return stickVec;
	}

	public static float GetJaw () {
		return jaw;
	}

	public static Vector2 GetTriggerAxis () {
		return thrust;
	}

	public static bool GetMenuPressed () {
		return menuPressed;
	}


}
