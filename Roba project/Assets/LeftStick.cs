using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Valve.VR;

public class LeftStick : MonoBehaviour {

	EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
	EVRButtonId touchPad = EVRButtonId.k_EButton_SteamVR_Touchpad;
	EVRButtonId menu = EVRButtonId.k_EButton_ApplicationMenu;
	EVRButtonId grip = EVRButtonId.k_EButton_Grip;

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device controller;

	public Transform seat;
	public Transform head;

	public Image throttleImage;
	public Text throttleText;
	public AudioSource engine;

	public float yRotMin, yRotMax;
	public float xRotMin, xRotMax;
	public float thrustMult;


	static float hover;
	static float thrust;

	float lastZ;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
		transform.parent.position = seat.position + (transform.parent.position - head.position);
	}
	
	void Update () {
		hover = controller.GetAxis(touchPad).y;
		
		var yRot = transform.localEulerAngles.y;
		var xRot = transform.localEulerAngles.x;
		if (yRot > yRotMin && yRot < yRotMax && xRot > xRotMin && xRot < xRotMax) {
			var delta = transform.localPosition.z - lastZ;
			thrust += delta * thrustMult;
			thrust = Mathf.Clamp(thrust, 0f, 1f);
			engine.pitch = 2 * thrust;
			throttleImage.color = Color.green;
		} else {
			throttleImage.color = Color.white;
		}
		throttleText.text = "" + Mathf.Round(thrust * 10) / 10;


		if (controller.GetPressDown(menu)) {
			transform.parent.position = seat.position + (transform.parent.position - head.position);
			print("RESET");
		}

		lastZ = transform.localPosition.z;
	}
	
	public static float GetHover() {
		return hover;
	}

	public static float GetThrust() {
		return thrust;
	}
}
