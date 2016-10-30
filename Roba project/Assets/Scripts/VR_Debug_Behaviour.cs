using UnityEngine;
using System.Collections;

public class VR_Debug_Behaviour : MonoBehaviour, IUsable {

	VR_Object obj;

	void Start () {
		obj = GetComponent<VR_Object>();
	}

	public void Use () {
		obj.GetCont().GetComponent<VR_Input>().SetCurrentlyInUse(null);
		obj.Drop(transform.forward * 10f, Vector3.zero);
	}
}
