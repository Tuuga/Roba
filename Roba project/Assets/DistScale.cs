using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistScale : MonoBehaviour {

	public Transform distTo;

	void Update () {
		var dist = Vector3.Distance(transform.position, distTo.position);
		transform.localScale = Vector3.one * (dist / 2f);
		var pos = new Vector3(distTo.position.x, distTo.position.y, transform.position.z);
		transform.position = pos;
	}
}
