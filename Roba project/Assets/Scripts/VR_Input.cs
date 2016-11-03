using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Valve.VR;

public class VR_Input : MonoBehaviour {

	EVRButtonId trigger = EVRButtonId.k_EButton_SteamVR_Trigger;
	EVRButtonId grip = EVRButtonId.k_EButton_Grip;
	EVRButtonId menu = EVRButtonId.k_EButton_ApplicationMenu;
	EVRButtonId touchPad = EVRButtonId.k_EButton_SteamVR_Touchpad;

	SteamVR_TrackedObject trackedObj;
	SteamVR_Controller.Device controller;

	VR_Object currentInUse;
	List<VR_Object> couldUse = new List<VR_Object>();

	public Transform inventorySlot;

	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		controller = SteamVR_Controller.Input((int)trackedObj.index);
	}

	void Update () {

		var closestObj = ClosestCouldUse();

		// Grip input down
		// Sets currentlyInUse and holds it
		if (controller.GetPressDown(grip) && closestObj != null) {
			SetCurrentlyInUse(closestObj);
			closestObj.Hold(transform);
		}
		// Grip input up
		// Drops currentlyInUse
		if (controller.GetPressUp(grip) && currentInUse != null) {
			var vel = transform.parent.TransformDirection(controller.velocity);
			var angVel = transform.parent.TransformDirection(controller.angularVelocity);

			currentInUse.Drop(vel, angVel);
			SetCurrentlyInUse(null);
		}

		// Touch pad input down
		// Stores currentlyInUse
		if (controller.GetPressDown(touchPad) && currentInUse != null) {
			currentInUse.Drop(Vector3.zero, Vector3.zero);
			currentInUse.Store(inventorySlot);
			SetCurrentlyInUse(null);
		}
		// Touch pad input up
		if (controller.GetPressUp(touchPad)) {

		}

		// Trigger input down
		// Use currentlyInUse if usable
		if (controller.GetPressDown(trigger) && currentInUse != null) {
			var usable = currentInUse.GetComponent<IUsable>();
			if (usable != null) { usable.Use(); }
		}

		// Trigger input up
		if (controller.GetPressUp(trigger)) {

		}

		// Menu button input down
		if (controller.GetPressDown(menu)) {
			
		}
		// Menu button input up
		if (controller.GetPressUp(menu)) {

		}
	}

	// Finds the closest VR_Object in couldUse
	VR_Object ClosestCouldUse () {
		float closestDist = Mathf.Infinity;
		VR_Object closestObj = null;
		for (int i = 0; i < couldUse.Count; i++) {
			if ((transform.position - couldUse[i].transform.position).magnitude < closestDist) {
				closestObj = couldUse[i];
			}
		}
		return closestObj;
	}

	public void SetCurrentlyInUse (VR_Object obj) {
		currentInUse = obj;
	}


	// Finds VR_Object component from collider
	// If none found
	// Loops through every parent until one is found or ran out of parents
	VR_Object FindObjInHierarchy (Collider c) {
		var obj = c.GetComponent<VR_Object>();
		var objTrans = c.transform;

		while (obj == null && objTrans.parent != null) {
			objTrans = objTrans.parent;
			obj = objTrans.GetComponent<VR_Object>();
		}
		return obj;
	}

	// If on trigger area
	// Check if VR_Object
	// Add to couldUse
	void OnTriggerEnter (Collider c) {
		var obj = FindObjInHierarchy(c);

		if (obj != null && !couldUse.Contains(obj)) {
			couldUse.Add(obj);
		}
	}

	// If exiting VR_Object
	// Remove from couldUse
	void OnTriggerExit (Collider c) {
		var exitObj = FindObjInHierarchy(c);
		if (couldUse.Contains(exitObj)) {
			couldUse.Remove(exitObj);
		}
	}
}
