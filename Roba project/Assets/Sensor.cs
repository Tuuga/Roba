using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour {

	public bool entrance;
	Hub hub;

	void Start () {
		hub = FindObjectOfType<Hub>();
	}

	void OnTriggerEnter (Collider c) {
		hub.Trigger(entrance, this);
	}
}
