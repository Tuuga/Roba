using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour {

	public int entered;
	public int inKitchen;

	Sensor lastSensor;
	bool back;

	public void Trigger (bool entrance, Sensor s) {
		//UnityEditor.EditorGUIUtility.PingObject(s);

		if (entrance) {
			entered++;
		}

		if (!entrance) {

			if (lastSensor == s) {
				back = !back;
				if (back) {
					inKitchen--;
				} else {
					inKitchen++;
				}
			}

			if (lastSensor != s && !lastSensor.entrance) {
				inKitchen--;
			}

		}

		lastSensor = s;
	}
}
