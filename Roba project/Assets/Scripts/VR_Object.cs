using UnityEngine;
using System.Collections;

public interface IUsable {
	void Use();
}

//offset = transform.position - inv.position;
//transform.position = inventory.position + offset;

[RequireComponent(typeof(Rigidbody))]
public class VR_Object : MonoBehaviour {

	Rigidbody rb;
	Transform contTransform;
	Transform inventory;

	Quaternion rotOnStore;

	bool holding;
	bool stored;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	public void Hold (Transform cont) {
		stored = false;
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

	public void Store (Transform inv) {
		stored = true;
		rb.isKinematic = true;
		inventory = inv;
		rotOnStore = inventory.rotation;
	}

	public Transform GetCont () {
		return contTransform;
	}

    void Update () {
		if (holding) {
			transform.position = contTransform.position;
			transform.rotation = contTransform.rotation;
		}

		if (stored) {
			transform.position = inventory.position;
			transform.rotation = Quaternion.Inverse(rotOnStore) * inventory.rotation;
		}
	}
}
