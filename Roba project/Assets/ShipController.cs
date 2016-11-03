using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour {

	public float pitchSpeed;
	public float jawSpeed;
	public float rollSpeed;

	public float thrustForce;
	public float hoverForce;

	bool inAir;

	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	void Update () {

		if (!RightStick.GetMenuPressed()) { return; }

		var pitchRoll = RightStick.GetPitchRoll();

		var pitch = -pitchRoll.y * pitchSpeed * Time.deltaTime;
		var roll = -pitchRoll.x * rollSpeed * Time.deltaTime;

		var jaw = RightStick.GetJaw() * jawSpeed * Time.deltaTime;

		var torque = Quaternion.Euler(pitch, jaw, roll);

		var thrust = new Vector3();
		if (RightStick.GetTriggerAxis().x >= 1) {
			thrust = transform.forward * (RightStick.GetTriggerAxis().x - LeftStick.GetTriggerAxis().x) * thrustForce * Time.deltaTime * 2;
		} else {
			thrust = transform.forward * (RightStick.GetTriggerAxis().x - LeftStick.GetTriggerAxis().x) * thrustForce * Time.deltaTime;
		}
		var hover = transform.up * LeftStick.GetHover() * hoverForce * Time.deltaTime;

		rb.AddForce(thrust, ForceMode.Force);
		rb.AddForce(hover, ForceMode.Force);

		transform.rotation *= torque;
		//transform.position += thrusth;
	}
}
