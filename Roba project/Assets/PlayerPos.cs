using UnityEngine;
using System.Collections;

public class PlayerPos : MonoBehaviour {

	public Transform head;

	void Update () {
		transform.position = head.position;
		transform.rotation = Quaternion.Euler(0, head.transform.rotation.eulerAngles.y, 0);
	}
}
