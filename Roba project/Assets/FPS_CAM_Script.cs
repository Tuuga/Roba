using UnityEngine;
using System.Collections;

public class FPS_CAM_Script : MonoBehaviour {

	public bool useSetHeight;
	public float setHeight;
	public float movSpeed;
	public float mouseSens;
	public float upDownRange;

	float verticalRotation;
	float horizontalRotation;

	// Android
	float twoTouchLastDist;

	bool mouseLock;

	public KeyCode upKey;
	public KeyCode downKey;

	GameObject mainCam;

	Quaternion baseRotation = Quaternion.identity;

	void Start () {

		MouseLock();
		mainCam = GameObject.FindGameObjectWithTag("MainCamera");
	}

	void Update () {

		if (Input.touchCount == 1) {
			TouchLook();
		}
		if (Input.touchCount == 2) {
			TouchMovement();
		}

		if (mouseLock)
			MouseLook();

		if (Input.GetKeyDown(KeyCode.LeftAlt))
			MouseLock();

		CamMovement();
	}

	void MouseLook () {
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;
		verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

		horizontalRotation += Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

		transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0) * baseRotation;

		mainCam.transform.position = transform.position;
		mainCam.transform.rotation = transform.rotation;
	}

	void TouchLook () {

		if (Input.GetTouch(0).phase != TouchPhase.Began || Input.GetTouch(0).phase != TouchPhase.Stationary) {
			if (Input.GetTouch(0).phase == TouchPhase.Moved) {

				verticalRotation -= Input.GetTouch(0).deltaPosition.y * mouseSens * Time.deltaTime;
				verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);

				horizontalRotation += Input.GetTouch(0).deltaPosition.x * mouseSens * Time.deltaTime;

				transform.localRotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0) * baseRotation;

				mainCam.transform.position = transform.position;
				mainCam.transform.rotation = transform.rotation;
			}
		}
	}

	void TouchMovement () {
		float currentDist = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
		if (currentDist > Screen.width / 3) {
			transform.position += transform.forward * movSpeed * Time.deltaTime;
		} else {
			transform.position += -transform.forward * movSpeed * Time.deltaTime;
		}
	}

	void MouseLock () {
		mouseLock = !mouseLock;
		if (mouseLock) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void CamMovement () {

		if (Input.GetKey(KeyCode.W))
			transform.position += transform.forward * movSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.S))
			transform.position += -transform.forward * movSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.A))
			transform.position += -transform.right * movSpeed * Time.deltaTime;

		if (Input.GetKey(KeyCode.D))
			transform.position += transform.right * movSpeed * Time.deltaTime;

		if (Input.GetKey(upKey))
			transform.position += transform.up * movSpeed * Time.deltaTime;

		if (Input.GetKey(downKey))
			transform.position += -transform.up * movSpeed * Time.deltaTime;

		if (useSetHeight)
			transform.position = new Vector3(transform.position.x, setHeight, transform.position.z);
	}
}
