using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Lookat : MonoBehaviour {

	public Transform target;
	public float yOffset;
	public float angle;

	void Update () {
		transform.LookAt(target);
		Debug.DrawLine(transform.position + transform.up * yOffset, target.position + target.up * yOffset, Color.red);
	}
}
