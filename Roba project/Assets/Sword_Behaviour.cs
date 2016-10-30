using UnityEngine;
using System.Collections;

public class Sword_Behaviour : MonoBehaviour, IUsable {

	public GameObject blade;

	public void Use () {
		blade.SetActive(!blade.activeSelf);
	}
}
