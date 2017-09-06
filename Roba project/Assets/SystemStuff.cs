using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemStuff : MonoBehaviour {

	public Transform hourHand, minHand, secHand;

	void Update () {
		var current = System.DateTime.Now;
		var hour = current.Hour;
		var min = current.Minute;
		var second = current.Second;

		var hP = hour / 12f;
		var minP = min / 60f;
		var secP = second / 60f;

		hourHand.localRotation = Quaternion.Euler(0, 0, -360 * hP);
		minHand.localRotation = Quaternion.Euler(0, 0, -360 * minP);
		secHand.localRotation = Quaternion.Euler(0, 0, -360 * secP);
	}
}