using UnityEngine;
using System.Collections;

public interface IUsable {
	void Use();
}

[RequireComponent(typeof(Rigidbody))]
public class VR_Object : MonoBehaviour {

	Rigidbody rb;
	Transform contTransform;
	bool holding;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	public void Hold (Transform cont) {
		holding = true;
		rb.isKinematic = true;
		contTransform = cont;
	}

	public void Drop (Vector3 vel, Vector3 angVel) {
		holding = false;
		rb.isKinematic = false;
		rb.velocity = vel;
		rb.angularVelocity = angVel;
		contTransform = null;
	}

	public Transform GetCont () {
		return contTransform;
	}

    void Update () {
		if (holding) {
			transform.position = contTransform.position;
			transform.rotation = contTransform.rotation;
		}
	}
}
