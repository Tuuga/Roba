using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour, IUsable {

	public GameObject bullet;
	public float force;
	public float fireRate;

	float lastShot;

	public Transform barrelEnd;

	public void Use () {
		if (lastShot < Time.time + fireRate) {
			GameObject bulletIns = (GameObject)Instantiate(bullet, barrelEnd.position, Quaternion.identity);
			Rigidbody rb = bulletIns.GetComponent<Rigidbody>();
			rb.velocity = barrelEnd.forward * force;
			lastShot = Time.time;
		}
	}
}
